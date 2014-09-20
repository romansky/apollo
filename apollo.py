import serial
import math
from flask import Flask, jsonify, request
import threading


ser = serial.Serial('/dev/ttyACM1', 38400, timeout=10)

pBuffer = [None for x in range(1000)]
aBuffer = [None for x in range(1000)]

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
rocket_type = 'w'

def calcDistance(PsPsi, Or, rocket):
    Ps = psiToKilopascal(PsPsi)
    L = rocketsL[rocket]
    m = rocketsW[rocket]
    O = Or * (2 * math.pi) / 360
    v0 = math.sqrt(2 * L * ((Ps - P0)*A /m - g ))
    R = 2 * L * math.sin(2 * O) * (((Ps - P0) * A)/(m * g) -1)
    # print 'L',L,'m',m, 'A',A, 'Or',Or,'O',O,'PSI',PsPsi,'v0',v0,'R', R
    return R


def nextBufferPos(name) :
    if bufferIdx[name] is 99:
        bufferIdx[name] = 0
    else:
        bufferIdx[name] += 1
    return bufferIdx[name]

def curBufferPos(name):
    return bufferIdx[name]



def find_best_angle(pressure, distance, rocketType):
    if distance >= calcDistance(pressure, 45,rocketType):
        return False
    else:
        def _distance(angle):
            return (calcDistance(pressure, angle,rocketType),angle)
        angles = map(_distance,range(45,90))
        angles.reverse()
        def best_angle(a,b):
            if b[0] < distance:
                return b[1]
            else:
                return a
        return reduce(best_angle, angles,0)


def loop_on_serial():
    while True:
        line = ser.readline()
        read_from_serial(line)

def update_angle():
    global aim_distance
    global rocket_type
    global angle
    angle = find_best_angle(pBuffer[curBufferPos('P')],aim_distance,rocket_type)

def read_from_serial(line):
    global aim_distance
    global rocket_type
    args = line.replace("\n","")
    action = args[0][0] 
    if action == 'A':
        aBuffer[nextBufferPos('A')] = float(args[1:])
    elif action == 'P':
        prev_pos = curBufferPos('P')
        pBuffer[nextBufferPos('P')] = float(args[1:])
        if pBuffer[prev_pos] != pBuffer[curBufferPos('P')]:
            update_angle()



thread = threading.Thread(target=loop_on_serial, args=())
thread.start()

app = Flask(__name__)

@app.route("/")
def index():
    with open ("index.html", "r") as myfile:
        return myfile.read().replace('\n', '')


@app.route("/distance", methods = ['POST','GET'])
def set_distance():
    global aim_distance
    if request.method == 'POST':
        aim_distance = int(request.form['set_distance'])
        update_angle()
        return "ok"

@app.route("/launch", methods = ['POST','GET'])
def launch():
    global aim_distance
    if request.method == 'POST':
        print "launching!"
    return "ok"

@app.route("/data")
def data():
    global aim_distance
    global rocket_type
    global angle
    return jsonify(
        pBuffer = pBuffer, 
        aBuffer = aBuffer, 
        cur_p_index = curBufferPos('P'), 
        cur_a_index = curBufferPos('A'),
        angle = angle,
        is_enough_pressure = (aim_distance <= calcDistance(pBuffer[curBufferPos('P')], 45,rocket_type)),
        cur_angle_distance = calcDistance(pBuffer[curBufferPos('P')], angle,rocket_type),
        cur_wind = calcPoly(aBuffer[curBufferPos('A')])
    )

if __name__ == "__main__":
    app.run()





