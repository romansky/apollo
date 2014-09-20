import serial
import math
from flask import Flask
import threading
from flask import jsonify


ser = serial.Serial('/dev/ttyUSB0', 38400, timeout=10)

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
angle = 45
aim_distance = 200

def calcDistance(PsPsi, Or, rocket):
    Ps = psiToKilopascal(PsPsi)
    L = rocketsL[rocket]
    m = rocketsW[rocket]
    O = Or * 360 / (2 * math.pi)
    v0 = math.sqrt(2 * L * ((Ps - P0)*A /m - g ))
    R = 2 * L * math.sin(O) * (((Ps - P0) * A)/(m * g) -1)
    return R


def nextBufferPos(name) :
    if bufferIdx[name] is 99:
        bufferIdx[name] = 0
    else:
        bufferIdx[name] += 1
    return bufferIdx[name]

def curBufferPos(name):
    return bufferIdx[name]



def read_from_serial():
    while True:
        line = ser.readline()
        args = line.replace("\n","")
        action = args[0][0] 
        if action == 'A':
            aBuffer[nextBufferPos('A')] = float(args[1:])
        elif action == 'P':
            pBuffer[nextBufferPos('P')] = float(args[1:])

thread = threading.Thread(target=read_from_serial, args=())
thread.start()

app = Flask(__name__)

@app.route("/")
def index():
    with open ("index.html", "r") as myfile:
        return myfile.read().replace('\n', '')


@app.route("/data")
def data():

    return jsonify(
        pBuffer = pBuffer, 
        aBuffer = aBuffer, 
        cur_p_index = curBufferPos('P'), 
        cur_a_index = curBufferPos('A'),
        angle = angle,
        is_enough_pressure = aim_distance <= calcDistance(pBuffer[curBufferPos('P')], 45,'w'),
        cur_angle_distance = calcDistance(pBuffer[curBufferPos('P')], angle,'w'),
        cur_wind = calcPoly(aBuffer[curBufferPos('A')])
    )

if __name__ == "__main__":
    app.run()




