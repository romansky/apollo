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

void setup()
{
    Serial.begin(SERIAL_RATE);

    pinMode(ANEMOMETER_PIN, INPUT);
    attachInterrupt(ANEMOMETER_PIN, rpmInterrupt, CHANGE);

    digitalWrite(RELEASE_PIN, LOW);

    digitalWrite(PITCH_MOTOR_STEP, LOW);

    Wire.begin();
}

 void loop()
 {
    if (pitchStepsToDo != 0)
    {
        int dir = pitchStepsToDo < 0 ? 1 : -1;

        digitalWrite(PITCH_MOTOR_DIR, pitchStepsToDo < 0 ? LOW : HIGH);

        digitalWrite(PITCH_MOTOR_STEP, pitchStepsToDo % 2 == 0 ? LOW : HIGH);

        pitchStepsToDo += dir;
        currentPitch += 1.0/PITCH_MOTOR_STEPS_PER_ANGLE/2.0 * dir;
    }

    if (releaseStop > 0 && millis() < releaseStop)
    {
        releaseStop = -1;
        digitalWrite(RELEASE_PIN, HIGH);
    }

    if (anemometerReportElapsed > MILLIS_IN_SEC) //Uptade every one second, this will be equal to reading frecuency (Hz).
    {
        cli();
        int inter = interruptsCount;
        interruptsCount = 0;
        anemometerReportElapsed = 0;
        sei();
        Serial.print('A');
        Serial.print(inter);
        Serial.print('\n');
    }


    if (psiReportElapsed > (MILLIS_IN_SEC / PSI_REPORTS_PER_SECOND))
    {

        float psi = psiDigital();
        psiReportElapsed = 0;
        Serial.print('P');
        Serial.print(psi);
        Serial.print('\n');
    }

    if (Serial.available() > 0)
    {
        int cmd = Serial.read();
        switch (cmd) {
            case 'R': // Relase pressure for X milliseconds.
            {
                int time = readInt();
                releaseStop = millis() + time;
                digitalWrite(RELEASE_PIN, LOW);
            }
            break;
            case 'C': // Calibrate current rig pitch
            {
                currentPitch = readFloat();
            }
            case 'S': // Set new rig pitch
            {
                float pitch = readFloat();

                float diff = currentPitch - pitch;

                int steps = diff*PITCH_MOTOR_STEPS_PER_ANGLE*2; // We double the steps since we need to output an edge for each step.

                pitchStepsToDo += steps;
            }
            break;
        }
    }
}

void rpmInterrupt()
{
    ++interruptsCount;
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

    float voltage = 3.3 * raw/4095;           //voltage present
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
    char str[5];
    int len = Serial.readBytesUntil('\n', str, 5);
    int num;
    str[len] = '\0';
    sscanf(str, "%d", &num);

    return num;
}

int readFloat()
{
    char str[16];
    int len = Serial.readBytesUntil('\n', str, 16);
    float num;
    str[len] = '\0';
    sscanf(str, "%f", &num);

    return num;
}

