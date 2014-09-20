#include <elapsedMillis.h>

#include "config.h"

#define MILLIS_IN_SEC 1000

elapsedMillis psiReportElapsed;
elapsedMillis anemometerReportElapsed;

void setup()
{
    HWSERIAL.begin(SERIAL_RATE);
}

void loop()
{
    if (anemometerReportElapsed > MILLIS_IN_SEC) //Uptade every one second, this will be equal to reading frecuency (Hz).
    {
        anemometerReportElapsed = 0;
        HWSERIAL.print('A');
        HWSERIAL.print(20);
        HWSERIAL.print('\n');
    }


    if (psiReportElapsed > (MILLIS_IN_SEC / PSI_REPORTS_PER_SECOND))
    {
        psiReportElapsed = 0;
        HWSERIAL.print('P');
        HWSERIAL.print(22.5);
        HWSERIAL.print('\n');
    }

    if (HWSERIAL.available() > 0) {
        int cmd = HWSERIAL.read();
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
    while(HWSERIAL.available() > 0 && HWSERIAL.read() != '\n');
}
