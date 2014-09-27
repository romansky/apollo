using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Text;
using System.Threading;
//using System.Threading.Tasks;
using System.Windows.Forms;
using Apollo.Properties;
using System.IO;
using Newtonsoft.Json;

namespace Apollo
{
    struct Rocket
    {
        public float Mass;
        public float Diameter;
        public float Tube;
        public float Drag;
    }

    public partial class frmMain : Form
    {
        SerialPort serialConnection = new SerialPort(Settings.Default.com_port, Settings.Default.baud_rate);
        const int INIT_READINGS = 10;
        float   _psiRading = 0.0f;
        float   _initialPsiRading = 0.0f;
        int _initialPsiRadingCounts = INIT_READINGS;
        float   _windSpeed = 0.0f;
        bool _enableFireSystem = true;

        System.Timers.Timer _reactivateFireSystem = new System.Timers.Timer();

        string _rocketsDirectory = "";
        
        public frmMain()
        {

            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmMain_FormClosing);

            _rocketsDirectory = Directory.GetCurrentDirectory() + "/Rockets";

            _reactivateFireSystem.Interval = 1000;
            _reactivateFireSystem.Elapsed += reactivateFireSystem_Elapsed;
            _reactivateFireSystem.Enabled = false;
        }

        void reactivateFireSystem_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _enableFireSystem = true;
            _reactivateFireSystem.Enabled = false;
        }

        void safeExit()
        {
            if (serialConnection.IsOpen == true)
            {
                try
                {
                    serialConnection.Close();
                    serialConnection.Dispose();
                }
                catch { }
            }
        }

        void getComPorts()
        {
            // Get all the available COM ports and add them to the list
            cmbComPort.Items.Clear();
            foreach (string port in SerialPort.GetPortNames())
            {
                string portName = port;
                if (portName.IndexOf('w') > 0) // Small fix for bluetooth ports
                {
                    portName = portName.Substring(0, port.IndexOf('w'));
                }
                cmbComPort.Items.Add(portName);
            }
            cmbComPort.SelectedIndex = cmbComPort.FindString(Settings.Default.com_port);
            if (cmbComPort.SelectedIndex < 0)
            {
                cmbComPort.SelectedIndex = 0;
                Settings.Default.com_port = cmbComPort.Text;
                Settings.Default.Save();
            }
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            getComPorts();

            cmbBRate.SelectedIndex = cmbBRate.FindString(Settings.Default.baud_rate.ToString());
            serialConnection.DataReceived += new SerialDataReceivedEventHandler(serial_connection_DataReceived);

            string[] rockets = Directory.GetDirectories(_rocketsDirectory);
            foreach (string rocket in rockets)
            {
                cmbRockets.Items.Add(Path.GetFileName(rocket));
            }

            if (cmbRockets.Items.Count > 0)
            {
                cmbRockets.SelectedIndex = 0;
            }

            
        }

        void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            safeExit();
        }



        #region Arduino Connection Panel

        private void SetupSerialPort(ref SerialPort sPort, string ComPort, int BaudRate, int DataBits)
        {
            sPort.PortName = ComPort;
            sPort.BaudRate = BaudRate;
            sPort.DataBits = 8;
            sPort.DtrEnable = true;
        }

        private void cmbComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.com_port = cmbComPort.Text;
            Settings.Default.Save();
        }

        private void btnComReload_Click(object sender, EventArgs e)
        {
            getComPorts();
        }

        private void cmbBRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.baud_rate = Convert.ToInt32(cmbBRate.Text);
            Settings.Default.Save();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                // Use the button text as an indecator for conneciton
                if (serialConnection.IsOpen == false)
                {
                    // Load the information to the serial port for connection
                    SetupSerialPort(ref serialConnection, Settings.Default.com_port, Settings.Default.baud_rate, 8);
                    serialConnection.Open(); // Create a connection
                    btnConnect.Text = "Disconnect"; // Change the button caption
                }
                else
                {
                    serialConnection.DiscardInBuffer();
                    serialConnection.DiscardOutBuffer();
                    serialConnection.Close(); // Close the connection
                    serialConnection.Dispose();
                    btnConnect.Text = "Connect"; // Change the button caption
                }
            }
            catch
            {
                btnConnect.Text = "Connect"; // Faild to connect
            }
        }
        
        #endregion

        // This is a data callback that handles the data sent from the arduino
        void serial_connection_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialConnection.IsOpen == true)
            {
                try
                {
                    string receivedData = serialConnection.ReadLine();
                    handleReceivedData(receivedData);
                }
                catch { }
            }
        }

        void handleReceivedData(string receivedData)
        {
            if (receivedData.StartsWith("AH"))
            {

            }
            else if (receivedData.StartsWith("AL"))
            {

            }
            else if (receivedData[0] == 'P')
            {
                _psiRading = Convert.ToSingle(receivedData.Substring(1, receivedData.Length-1));
            }
            else if (receivedData[0] == 'A')
            {
                Console.WriteLine("------------------");
                Console.WriteLine(receivedData);
                Console.WriteLine("------------------");
                int interrupts = Convert.ToInt32(receivedData.Substring(1, receivedData.Length - 1));
                _windSpeed = windSpeedFromInterrupts(interrupts);
            }


            BeginInvoke(new Action(() =>
            {
                // Update clocks graphics
                systemReadingPB.Refresh();
                lblRPMValue.Text = "Current wind speed: " + _windSpeed + " m/s";
                lblPSIValue.Text = "Current PSI: " + _psiRading + " (" + ((_initialPsiRadingCounts >= 0) ? 0 : Math.Max(0, _psiRading - _initialPsiRading)) + ")";
                if (_initialPsiRadingCounts > 0 && _psiRading > 0.1f)
                {
                    _initialPsiRadingCounts--;
                    _initialPsiRading += _psiRading;
                }
                else  if (_initialPsiRadingCounts == 0)
                {
                    _initialPsiRadingCounts--;
                    _initialPsiRading = _initialPsiRading / INIT_READINGS;
                }

                if (radioFirePSI.Checked == true)
                {
                    if (_enableFireSystem && (_psiRading - _initialPsiRading) >= (float)numPsiThreshold.Value)
                    {
                        Console.WriteLine("Fired @ " + (_psiRading - _initialPsiRading));
                        _enableFireSystem = false;
                        fileTheCannon(trackMSToOpen.Value);
                        _reactivateFireSystem.Enabled = true;
                    }
                }

                //if (recordLine && _recordData)
                //{
                    
                //    string s = _lastWindSpeed.ToString() + "," + _windInMS;
                //    _dataFile.WriteLine(s);
                //    _dataFile.Flush();
                //    lblLastRecord.Text = s;
                //}

            }));
        }

        float scaleValue(float vlaue,  float inMin,  float inMax,  float outMin,  float outMax)
        {
            return (vlaue - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        }

        void drawClock(Graphics g, int x, int y, int size, float needleValue, float min, float max, float targetValue = -1)
        {
            // Make sure that the needle value is in range
            needleValue = Math.Max(needleValue, min);
            needleValue = Math.Min(needleValue, max);

            // Some definitions
            Pen blackPen = new Pen(Color.Black, 2);
            Pen redPen = new Pen(Color.Red, 2);
            Pen greenPen = new Pen(Color.Green, 2);
            greenPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush whiteBrush = new SolidBrush(Color.White);

            Rectangle clockRect = new Rectangle(x, y, size, size);
            g.FillPie(whiteBrush, clockRect, 180, 180);
            g.DrawArc(blackPen, clockRect, 180, 180);
            g.DrawLine(blackPen, new Point(clockRect.X, clockRect.Y + (clockRect.Height / 2)), new Point(clockRect.X + clockRect.Width, clockRect.Y + (clockRect.Height / 2)));

            // Calculate needle position
            int centerCircleSize = 20;
            float startX = clockRect.X + (clockRect.Width / 2);
            float startY = clockRect.Y + (clockRect.Height / 2);

            float value = scaleValue(needleValue, min, max, 0, 1);
            float needleRadians = Convert.ToSingle((Math.PI * value) - Math.PI);

            float targetX = Convert.ToSingle(startX + Math.Cos(needleRadians) * 90);
            float targetY = Convert.ToSingle(startY + Math.Sin(needleRadians) * 90);

            // Show the target value if it specified
            if (targetValue > -1)
            {
                float tValue = scaleValue(targetValue, min, max, 0, 1);
                float tRadians = Convert.ToSingle((Math.PI * tValue) - Math.PI);

                float tTargetX = Convert.ToSingle(startX + Math.Cos(tRadians) * 90);
                float tTargetY = Convert.ToSingle(startY + Math.Sin(tRadians) * 90);

                // Draw the target needle
                g.DrawLine(greenPen, new PointF(startX, startY), new PointF(tTargetX, tTargetY));
            }
            
            
            // Draw the needle
            g.DrawLine(redPen, new PointF(startX, startY), new PointF(targetX, targetY));
            
            // Center half circle to cover the needle
            g.FillPie(blackBrush, startX - (centerCircleSize / 2), startY - (centerCircleSize / 2), centerCircleSize, centerCircleSize, 180, 180);

            Font font = new Font("Arial", 7);

            // Draw small bars the indicate middle values
            int barCount = 8;
            float stepValue = max / barCount;
            float stringPositionFactor = (float)Math.Ceiling((barCount - 1.0f) / 2.0f);
            for (int i = 1; i < 8; i++)
            {
                string str = (stepValue*i).ToString("##.##");
                SizeF stringSize = g.MeasureString(str, font);

                float barRadians = Convert.ToSingle((Math.PI * 0.125 * i) - Math.PI);
                float barStartX = Convert.ToSingle(startX + Math.Cos(barRadians) * 90);
                float barStartY = Convert.ToSingle(startY + Math.Sin(barRadians) * 90);
                float barTargetX = Convert.ToSingle(barStartX + Math.Cos(barRadians) * 10);
                float barTargetY = Convert.ToSingle(barStartY + Math.Sin(barRadians) * 10);
                g.DrawLine(blackPen, new PointF(barStartX, barStartY), new PointF(barTargetX, barTargetY));
                g.DrawString(str, font, Brushes.Black, barTargetX - (stringSize.Width / 2) - (stringSize.Width / 2 + 5) * ((stringPositionFactor - i) / (stringPositionFactor - 1)), barTargetY - stringSize.Height - 3);
            }

            // Draw max and min values
            SizeF stringMinSize = g.MeasureString(min.ToString(), font);
            SizeF stringMaxSize = g.MeasureString(max.ToString(), font);

            g.DrawString(min.ToString(), font, Brushes.Black, clockRect.X - (stringMinSize.Width / 2) - (stringMinSize.Width / 2 + 5), clockRect.Y + (clockRect.Height / 2) - stringMinSize.Height);
            g.DrawString(max.ToString(), font, Brushes.Black, clockRect.X + clockRect.Width - (stringMaxSize.Width / 2) + (stringMaxSize.Width / 2 + 5), clockRect.Y + (clockRect.Height / 2) - stringMinSize.Height);
        }

        private void systemReadingPB_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // PSI Clock
            drawClock(e.Graphics, 30, 20, 200, _psiRading, 0, 100, (_initialPsiRadingCounts >= 0) ? 0 : Math.Max(0, _psiRading - _initialPsiRading));

            // Wind Speed Clock
            drawClock(e.Graphics, 330, 20, 200, _windSpeed, 0, 50);
        }

        private void trackTargetPSI_ValueChanged(object sender, EventArgs e)
        {
            // Update clocks graphics here only when there is no connection to avoid unnecessary updates
            if (serialConnection.IsOpen == false)
            {
                systemReadingPB.Refresh();
            }
        }

        float fmap(float x, float in_min, float in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        float windSpeedFromInterrupts(float x)
        {
            if (x < 12.5) return 0;

            float pP1 = -2.069e-006f;
            float pP2 = 0.000423f;
            float pP3 = -0.03118f;
            float pP4 = 1.202f;
            float pP5 = -10.69f;
            
            return (float)(pP1 * Math.Pow(x, 4) + pP2 * Math.Pow(x, 3) + pP3 * Math.Pow(x, 2) + pP4 * x + pP5);
        }

        float psi(int raw)
        {
            float voltage = 5.0f * raw / 1023;           //voltage present
            float percent = 100.0f * voltage / 5;      //percetange of total voltage

            // handle out of scope range
            if (percent < 10) return -1;
            if (percent > 90) return -2;

            float maxPressure = 100.0f;
            float minPressure = 0.0f;

            float pressure = fmap(percent, 10, 90, minPressure, maxPressure); // we map 5% to minPressure and 95% to maxPressure
            return pressure;
        }

        private void cmbRockets_SelectedIndexChanged(object sender, EventArgs e)
        {
            string infoFile = _rocketsDirectory + "/" + cmbRockets.Text + "/info.json";
            StreamReader file = new StreamReader(infoFile);
            string json = file.ReadToEnd();
            file.Close();
            Rocket r = JsonConvert.DeserializeObject<Rocket>(json);
            lblMass.Text = "Mass: " + r.Mass.ToString() + " kg";
            lblDiameter.Text = "Diameter: " + r.Diameter.ToString() + " m";
            lblTubeLength.Text = "Tube lenght: " + r.Tube.ToString() + " m";
            lblDrag.Text = "Drag coefficient: " + r.Drag.ToString();
        }

        private void btnFire_Click(object sender, EventArgs e)
        {
            fileTheCannon(trackMSToOpen.Value);
        }

        void fileTheCannon(int ms)
        {
            if (serialConnection.IsOpen == true)
            {
                try
                {
                    serialConnection.WriteLine("R" + ms);
                }
                catch { }
            }
        }

        private void trackMSToOpen_Scroll(object sender, EventArgs e)
        {
            lblOpenSolenoid.Text = "Open solenoid for " + trackMSToOpen.Value + " ms";
        }
    }
}
