#include <elapsedMillis.h>
#include <Wire.h>

#include "config.h"

#define MILLIS_IN_SEC 1000

elapsedMillis psiReportElapsed;
elapsedMillis anemometerReportElapsed;

int releaseStop = 0;

volatile int interruptsCount = 0;

float currentPitch = 0;

int pitchStepsToDo = 0;

long speedTriggerTimeA = 0;
long speedTriggerTimeB = 0;

//#define DEBUG_MOTOR
//#define DEBUG_RELEASE
//#define DEBUG_SPEED

int motorDelay = 16000;

void setup()
{
    analogReadResolution(16);

    HWSERIAL.begin(SERIAL_RATE);

    pinMode(ANEMOMETER_PIN, INPUT);
    attachInterrupt(ANEMOMETER_PIN, rpmInterrupt, CHANGE);

    pinMode(SPEED_PIN_A, INPUT);
    pinMode(SPEED_PIN_B, INPUT);
    attachInterrupt(SPEED_PIN_A, speedPinAInterrupt, RISING);
    attachInterrupt(SPEED_PIN_A, speedPinAInterrupt, FALLING);
    attachInterrupt(SPEED_PIN_B, speedPinBInterrupt, RISING);

    pinMode(RELEASE_PIN, OUTPUT);
    digitalWrite(RELEASE_PIN, HIGH);

    pinMode(PITCH_MOTOR_DIR, OUTPUT);
    pinMode(PITCH_MOTOR_STEP, OUTPUT);
    digitalWrite(PITCH_MOTOR_STEP, LOW);

    //Wire.begin();
}

 void loop()
 {
    if (pitchStepsToDo != 0)
    {
        int dir = pitchStepsToDo < 0 ? 1 : -1;

        digitalWrite(PITCH_MOTOR_DIR, pitchStepsToDo < 0 ? LOW : HIGH);

        digitalWrite(PITCH_MOTOR_STEP, pitchStepsToDo % 2 == 0 ? LOW : HIGH);

        pitchStepsToDo += dir;
#ifdef DEBUG_MOTOR
        HWSERIAL.print(pitchStepsToDo < 0 ? ".\n" : "+\n");
#endif
        //currentPitch += (1.0/PITCH_MOTOR_STEPS_PER_ANGLE)/2.0 * dir;
    }

    if (releaseStop > 0 && millis() > releaseStop)
    {
        releaseStop = -1;
        digitalWrite(RELEASE_PIN, HIGH);
#ifdef DEBUG_RELEASE
        HWSERIAL.println("Release done.");
#endif
    }

    if (anemometerReportElapsed > MILLIS_IN_SEC) //Uptade every one second, this will be equal to reading frecuency (Hz).
    {
        cli();
        int inter = interruptsCount;
        interruptsCount = 0;
        anemometerReportElapsed = 0;
        sei();
        HWSERIAL.print('A');
        HWSERIAL.print(inter);
        HWSERIAL.print('\n');
    }

    if (speedTriggerTimeB > 0)
    {
        cli();
        float speed = SPEED_DISTANCE / ((speedTriggerTimeB - speedTriggerTimeA) / 1000000.0);
        speedTriggerTimeA = 0;
        speedTriggerTimeB = 0;
        sei();
        HWSERIAL.print('S');
        HWSERIAL.print(speed);
        HWSERIAL.print('\n');
    }


    if (psiReportElapsed > (MILLIS_IN_SEC / PSI_REPORTS_PER_SECOND))
    {

        float psi = psiAnalog();
        psiReportElapsed = 0;
        HWSERIAL.print('P');
        HWSERIAL.print(psi);
        HWSERIAL.print('\n');
    }

    if (HWSERIAL.available() > 0)
    {
        int cmd = HWSERIAL.read();
        switch (cmd) {
            case 'R': // Relase pressure for X milliseconds.
            {
                int time = readInt();
                releaseStop = millis() + time;
                digitalWrite(RELEASE_PIN, LOW);
#ifdef DEBUG_RELEASE
                HWSERIAL.print("Initing release for ");
                HWSERIAL.print(time);
                HWSERIAL.println("ms.");
#endif
            }
            break;
            case 'C': // Calibrate current rig pitch
            {
                currentPitch = readFloat();
            }
            break;
            case 'S': // Set new rig pitch
            {
                float pitch = readFloat();

                float diff = currentPitch - pitch;

                int steps = diff*PITCH_MOTOR_STEPS_PER_ANGLE*2; // We double the steps since we need to output an edge for each step.

                pitchStepsToDo += steps;
                currentPitch = pitch;
#ifdef DEBUG_MOTOR
                HWSERIAL.print("Initing pitch: ");
                HWSERIAL.print(currentPitch);
                HWSERIAL.print(". Starting movment to ");
                HWSERIAL.print(pitch);
                HWSERIAL.print(" gonna do ");
                HWSERIAL.print(steps/2);
                HWSERIAL.println(" steps");
#endif
            }
            break;
            case 'D':
            {
                motorDelay = readInt();
            }
            break;
        }
    }

// We only need to delay if we don't use debug(which slows down the whole loop)
#ifndef DEBUG_MOTOR
    delayMicroseconds(motorDelay);
#endif
}

void rpmInterrupt()
{
    ++interruptsCount;
}

void speedPinAInterrupt()
{
    HWSERIAL.println("AH");
    // Only read once.
    if (speedTriggerTimeA == 0)
    {
        speedTriggerTimeA = micros();
    }
}

void speedPinAInterruptFalling()
{
    HWSERIAL.println("AL");
}


void speedPinBInterrupt()
{
#ifdef DEBUG_SPEED
    HWSERIAL.println("SPEED B");
#endif
    // Only read if A is available and only read once.
    if (speedTriggerTimeA > 0 && speedTriggerTimeB == 0)
    {
        speedTriggerTimeB = micros();
    }
}

float psiDigital()
{
    Wire.requestFrom(0x78, 2);
    if (Wire.available()) {
        byte x = Wire.read();
        byte y = Wire.read();
        Wire.endTransmission();
        int num = x;
        num = num << 8;
        num |= y;

        return (((num - 1638) * (100 - 0.36)) / (14746 - 1638)) + 0.36;
    }

    return 0;
}

float psiAnalog()
{
    int raw = analogRead(PRESSURE_METER_PIN);

    float voltage = 3.3 * raw/(65536-1);           //voltage present
    float percent = 100.0 * voltage/3.3;      //percetange of total voltage

    // handle out of scope range
    if (percent < 10) return -1;
    if (percent > 90) return -2;

    float maxPressure = 100.0;
    float minPressure= 0.0;

    float pressure = fmap(percent, 10, 90, minPressure, maxPressure); // we map 5% to minPressure and 95% to maxPressure
    return pressure;
}

float fmap(float x, float in_min, float in_max, float out_min, float out_max)
{
    return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
}

int readInt()
{
    char str[10];
    int len = HWSERIAL.readBytesUntil('\n', str, 10);
    int num;
    str[len] = '\0';
    sscanf(str, "%d", &num);

    return num;
}

int readFloat()
{
    char str[16];
    int len = HWSERIAL.readBytesUntil('\n', str, 16);
    float num;
    str[len] = '\0';
    sscanf(str, "%f", &num);

    return num;
}

