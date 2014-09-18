#include <Serial.h>
#include <elapsedMillis.h>

#include "config.h"

#define MILLIS_IN_SEC 1000

elapsedMillis psiReportElapsed;
elapsedMillis anemometerReportElapsed;

void setup()
{
    Serial.begin(SERIAL_RATE);
}

void loop()
{
    if (anemometerReportElapsed > MILLIS_IN_SEC) //Uptade every one second, this will be equal to reading frecuency (Hz).
    {
        anemometerReportElapsed = 0;
        Serial.print('A');
        Serial.print(20);
        Serial.print('\n');
    }


    if (psiReportElapsed > (MILLIS_IN_SEC / PSI_REPORTS_PER_SECOND))
    {
        psiReportElapsed = 0;
        Serial.print('P');
        Serial.print(22.5);
        Serial.print('\n');
    }

    if (Serial.available() > 0) {
        int cmd = Serial.read();
        switch (cmd) {
            case 'R': // Relase pressure for X milliseconds.
            {
                readMock();
            }
            break;
            case 'C': // Calibrate current rig pitch
            {
                readMock();
            }
            case 'S':
            {
                readMock();
            }
            break;
        }
    }
}

void readMock()
{
    while(Serial.available() > 0 && Serial.read() != '\n');
}
