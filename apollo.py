import serial
import math

ser = serial.Serial('/dev/rfcomm0', 9600, timeout=10)

pBuffer = [None for x in range(100)]
aBuffer = [None for x in range(100)]

bufferIdx = { 'A' : -1, 'P' : -1}

pP1 = -2.069e-006
pP2 = 0.000423
pP3 = -0.03118
pP4 = 1.202
pP5 = -10.69

def calcPoly(x):
    return pP1*x**4 + pP2*x**3 + pP3*x**2 + pP4*x + pP5

def psiToKilopascal(psi):
    return psi * 6894.75729

rocketsW = {
    'w' : 59.0 / 1000,
    'gray' : 71.0 / 1000,
    'green' : 56.0 / 1000
}

rocketsL = {
    'w' : 22.0 / 100, #cms / 100
    'gray' : 21.0 / 100,
    'green' : 22.0 / 100   
}


Ar = 0.011 # rocket radius
A = math.pi * Ar ** 2 # rocket area
P0 = 101352.932
g = 9.8 # m/s^2

def calcDistance(PsPsi, O, rocket):
    Ps = psiToKilopascal(PsPsi)
    L = rocketsL[rocket]
    m = rocketsW[rocket]
    v0 = math.sqrt(2 * L * ((Ps - P0)*A /m - g ))
    print 'Ps',Ps, 'L',L,'m',m,'v0',v0, 'A',A
    R = 2 * L * math.sin(O) * (((Ps - P0) * A)/(m * g) -1)
    print 'R', R


def nextBufferPos(name) :
    if bufferIdx[name] is 99:
        bufferIdx[name] = 0
    else:
        bufferIdx[name] += 1
    return bufferIdx[name]

def curBufferPos(name):
    return bufferIdx[name]

while True:
    line = ser.readline()
    args = line.replace("\n","")
    action = args[0][0] 
    if action == 'A':
        aBuffer[nextBufferPos('A')] = calcPoly(float(args[1:]))
        print aBuffer[curBufferPos('A')]
    elif action == 'P':
        pBuffer[nextBufferPos('P')] = calcDistance(float(args[1:]), 45,'w')
    print pBuffer





