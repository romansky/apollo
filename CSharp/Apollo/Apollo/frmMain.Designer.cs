namespace Apollo
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnComReload = new System.Windows.Forms.Button();
            this.cmbBRate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbComPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.systemReadingGB = new System.Windows.Forms.GroupBox();
            this.lblRPMValue = new System.Windows.Forms.Label();
            this.lblPSIValue = new System.Windows.Forms.Label();
            this.lblRPM = new System.Windows.Forms.Label();
            this.lblPSI = new System.Windows.Forms.Label();
            this.systemReadingPB = new System.Windows.Forms.PictureBox();
            this.grpRocketType = new System.Windows.Forms.GroupBox();
            this.lblDrag = new System.Windows.Forms.Label();
            this.lblTubeLength = new System.Windows.Forms.Label();
            this.lblDiameter = new System.Windows.Forms.Label();
            this.lblMass = new System.Windows.Forms.Label();
            this.cmbRockets = new System.Windows.Forms.ComboBox();
            this.btnFire = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblOpenSolenoid = new System.Windows.Forms.Label();
            this.trackMSToOpen = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numDistanceThreshold = new System.Windows.Forms.NumericUpDown();
            this.radioFireDistance = new System.Windows.Forms.RadioButton();
            this.numPsiThreshold = new System.Windows.Forms.NumericUpDown();
            this.radioFirePSI = new System.Windows.Forms.RadioButton();
            this.radioFireManual = new System.Windows.Forms.RadioButton();
            this.groupBox3.SuspendLayout();
            this.systemReadingGB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemReadingPB)).BeginInit();
            this.grpRocketType.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMSToOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistanceThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPsiThreshold)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnConnect);
            this.groupBox3.Controls.Add(this.btnComReload);
            this.groupBox3.Controls.Add(this.cmbBRate);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.cmbComPort);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(13, 388);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(591, 56);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Arduino Connection:";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(510, 21);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 14;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnComReload
            // 
            this.btnComReload.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnComReload.BackgroundImage")));
            this.btnComReload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnComReload.Location = new System.Drawing.Point(108, 20);
            this.btnComReload.Name = "btnComReload";
            this.btnComReload.Size = new System.Drawing.Size(25, 23);
            this.btnComReload.TabIndex = 15;
            this.btnComReload.UseVisualStyleBackColor = true;
            this.btnComReload.Click += new System.EventHandler(this.btnComReload_Click);
            // 
            // cmbBRate
            // 
            this.cmbBRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBRate.FormattingEnabled = true;
            this.cmbBRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cmbBRate.Location = new System.Drawing.Point(206, 21);
            this.cmbBRate.Name = "cmbBRate";
            this.cmbBRate.Size = new System.Drawing.Size(76, 21);
            this.cmbBRate.TabIndex = 14;
            this.cmbBRate.SelectedIndexChanged += new System.EventHandler(this.cmbBRate_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Baud Rate:";
            // 
            // cmbComPort
            // 
            this.cmbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComPort.FormattingEnabled = true;
            this.cmbComPort.Location = new System.Drawing.Point(38, 21);
            this.cmbComPort.Name = "cmbComPort";
            this.cmbComPort.Size = new System.Drawing.Size(63, 21);
            this.cmbComPort.TabIndex = 10;
            this.cmbComPort.SelectedIndexChanged += new System.EventHandler(this.cmbComPort_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Port:";
            // 
            // systemReadingGB
            // 
            this.systemReadingGB.Controls.Add(this.lblRPMValue);
            this.systemReadingGB.Controls.Add(this.lblPSIValue);
            this.systemReadingGB.Controls.Add(this.lblRPM);
            this.systemReadingGB.Controls.Add(this.lblPSI);
            this.systemReadingGB.Controls.Add(this.systemReadingPB);
            this.systemReadingGB.Location = new System.Drawing.Point(12, 12);
            this.systemReadingGB.Name = "systemReadingGB";
            this.systemReadingGB.Size = new System.Drawing.Size(591, 205);
            this.systemReadingGB.TabIndex = 14;
            this.systemReadingGB.TabStop = false;
            this.systemReadingGB.Text = "System Readings:";
            // 
            // lblRPMValue
            // 
            this.lblRPMValue.AutoSize = true;
            this.lblRPMValue.Location = new System.Drawing.Point(339, 173);
            this.lblRPMValue.Name = "lblRPMValue";
            this.lblRPMValue.Size = new System.Drawing.Size(131, 13);
            this.lblRPMValue.TabIndex = 19;
            this.lblRPMValue.Text = "Current wind speed: 0 m/s";
            // 
            // lblPSIValue
            // 
            this.lblPSIValue.AutoSize = true;
            this.lblPSIValue.Location = new System.Drawing.Point(35, 173);
            this.lblPSIValue.Name = "lblPSIValue";
            this.lblPSIValue.Size = new System.Drawing.Size(73, 13);
            this.lblPSIValue.TabIndex = 18;
            this.lblPSIValue.Text = "Current PSI: 0";
            // 
            // lblRPM
            // 
            this.lblRPM.AutoSize = true;
            this.lblRPM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblRPM.Location = new System.Drawing.Point(397, 23);
            this.lblRPM.Name = "lblRPM";
            this.lblRPM.Size = new System.Drawing.Size(93, 16);
            this.lblRPM.TabIndex = 17;
            this.lblRPM.Text = "Wind Speed";
            // 
            // lblPSI
            // 
            this.lblPSI.AutoSize = true;
            this.lblPSI.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblPSI.Location = new System.Drawing.Point(93, 23);
            this.lblPSI.Name = "lblPSI";
            this.lblPSI.Size = new System.Drawing.Size(93, 16);
            this.lblPSI.TabIndex = 16;
            this.lblPSI.Text = "Air Pressure";
            // 
            // systemReadingPB
            // 
            this.systemReadingPB.Location = new System.Drawing.Point(9, 42);
            this.systemReadingPB.Name = "systemReadingPB";
            this.systemReadingPB.Size = new System.Drawing.Size(576, 128);
            this.systemReadingPB.TabIndex = 15;
            this.systemReadingPB.TabStop = false;
            this.systemReadingPB.Paint += new System.Windows.Forms.PaintEventHandler(this.systemReadingPB_Paint);
            // 
            // grpRocketType
            // 
            this.grpRocketType.Controls.Add(this.lblDrag);
            this.grpRocketType.Controls.Add(this.lblTubeLength);
            this.grpRocketType.Controls.Add(this.lblDiameter);
            this.grpRocketType.Controls.Add(this.lblMass);
            this.grpRocketType.Controls.Add(this.cmbRockets);
            this.grpRocketType.Location = new System.Drawing.Point(13, 223);
            this.grpRocketType.Name = "grpRocketType";
            this.grpRocketType.Size = new System.Drawing.Size(204, 159);
            this.grpRocketType.TabIndex = 17;
            this.grpRocketType.TabStop = false;
            this.grpRocketType.Text = "Rocket type:";
            // 
            // lblDrag
            // 
            this.lblDrag.AutoSize = true;
            this.lblDrag.Location = new System.Drawing.Point(7, 125);
            this.lblDrag.Name = "lblDrag";
            this.lblDrag.Size = new System.Drawing.Size(85, 13);
            this.lblDrag.TabIndex = 4;
            this.lblDrag.Text = "Drag coefficient:";
            // 
            // lblTubeLength
            // 
            this.lblTubeLength.AutoSize = true;
            this.lblTubeLength.Location = new System.Drawing.Point(7, 100);
            this.lblTubeLength.Name = "lblTubeLength";
            this.lblTubeLength.Size = new System.Drawing.Size(67, 13);
            this.lblTubeLength.TabIndex = 3;
            this.lblTubeLength.Text = "Tube length:";
            // 
            // lblDiameter
            // 
            this.lblDiameter.AutoSize = true;
            this.lblDiameter.Location = new System.Drawing.Point(7, 75);
            this.lblDiameter.Name = "lblDiameter";
            this.lblDiameter.Size = new System.Drawing.Size(52, 13);
            this.lblDiameter.TabIndex = 2;
            this.lblDiameter.Text = "Diameter:";
            // 
            // lblMass
            // 
            this.lblMass.AutoSize = true;
            this.lblMass.Location = new System.Drawing.Point(7, 50);
            this.lblMass.Name = "lblMass";
            this.lblMass.Size = new System.Drawing.Size(38, 13);
            this.lblMass.TabIndex = 1;
            this.lblMass.Text = "Mass: ";
            // 
            // cmbRockets
            // 
            this.cmbRockets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRockets.FormattingEnabled = true;
            this.cmbRockets.Location = new System.Drawing.Point(8, 19);
            this.cmbRockets.Name = "cmbRockets";
            this.cmbRockets.Size = new System.Drawing.Size(121, 21);
            this.cmbRockets.TabIndex = 0;
            this.cmbRockets.SelectedIndexChanged += new System.EventHandler(this.cmbRockets_SelectedIndexChanged);
            // 
            // btnFire
            // 
            this.btnFire.Location = new System.Drawing.Point(522, 359);
            this.btnFire.Name = "btnFire";
            this.btnFire.Size = new System.Drawing.Size(75, 23);
            this.btnFire.TabIndex = 18;
            this.btnFire.Text = "Fire!";
            this.btnFire.UseVisualStyleBackColor = true;
            this.btnFire.Click += new System.EventHandler(this.btnFire_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblOpenSolenoid);
            this.groupBox1.Controls.Add(this.trackMSToOpen);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numDistanceThreshold);
            this.groupBox1.Controls.Add(this.radioFireDistance);
            this.groupBox1.Controls.Add(this.numPsiThreshold);
            this.groupBox1.Controls.Add(this.radioFirePSI);
            this.groupBox1.Controls.Add(this.radioFireManual);
            this.groupBox1.Location = new System.Drawing.Point(223, 223);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 159);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Fire condition";
            // 
            // lblOpenSolenoid
            // 
            this.lblOpenSolenoid.AutoSize = true;
            this.lblOpenSolenoid.Location = new System.Drawing.Point(9, 99);
            this.lblOpenSolenoid.Name = "lblOpenSolenoid";
            this.lblOpenSolenoid.Size = new System.Drawing.Size(121, 13);
            this.lblOpenSolenoid.TabIndex = 8;
            this.lblOpenSolenoid.Text = "Open solenoid for 10 ms";
            // 
            // trackMSToOpen
            // 
            this.trackMSToOpen.LargeChange = 100;
            this.trackMSToOpen.Location = new System.Drawing.Point(6, 113);
            this.trackMSToOpen.Margin = new System.Windows.Forms.Padding(0);
            this.trackMSToOpen.Maximum = 1500;
            this.trackMSToOpen.Minimum = 10;
            this.trackMSToOpen.Name = "trackMSToOpen";
            this.trackMSToOpen.Size = new System.Drawing.Size(206, 45);
            this.trackMSToOpen.TabIndex = 7;
            this.trackMSToOpen.TickFrequency = 100;
            this.trackMSToOpen.Value = 500;
            this.trackMSToOpen.Scroll += new System.EventHandler(this.trackMSToOpen_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(174, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "meters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "psi";
            // 
            // numDistanceThreshold
            // 
            this.numDistanceThreshold.DecimalPlaces = 2;
            this.numDistanceThreshold.Enabled = false;
            this.numDistanceThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numDistanceThreshold.Location = new System.Drawing.Point(104, 73);
            this.numDistanceThreshold.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.numDistanceThreshold.Name = "numDistanceThreshold";
            this.numDistanceThreshold.Size = new System.Drawing.Size(64, 20);
            this.numDistanceThreshold.TabIndex = 4;
            this.numDistanceThreshold.Value = new decimal(new int[] {
            100,
            0,
            0,
            65536});
            // 
            // radioFireDistance
            // 
            this.radioFireDistance.AutoSize = true;
            this.radioFireDistance.Enabled = false;
            this.radioFireDistance.Location = new System.Drawing.Point(12, 74);
            this.radioFireDistance.Name = "radioFireDistance";
            this.radioFireDistance.Size = new System.Drawing.Size(86, 17);
            this.radioFireDistance.TabIndex = 3;
            this.radioFireDistance.Text = "Automatic @";
            this.radioFireDistance.UseVisualStyleBackColor = true;
            // 
            // numPsiThreshold
            // 
            this.numPsiThreshold.DecimalPlaces = 2;
            this.numPsiThreshold.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numPsiThreshold.Location = new System.Drawing.Point(104, 47);
            this.numPsiThreshold.Name = "numPsiThreshold";
            this.numPsiThreshold.Size = new System.Drawing.Size(64, 20);
            this.numPsiThreshold.TabIndex = 2;
            this.numPsiThreshold.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // radioFirePSI
            // 
            this.radioFirePSI.AutoSize = true;
            this.radioFirePSI.Location = new System.Drawing.Point(12, 47);
            this.radioFirePSI.Name = "radioFirePSI";
            this.radioFirePSI.Size = new System.Drawing.Size(86, 17);
            this.radioFirePSI.TabIndex = 1;
            this.radioFirePSI.Text = "Automatic @";
            this.radioFirePSI.UseVisualStyleBackColor = true;
            // 
            // radioFireManual
            // 
            this.radioFireManual.AutoSize = true;
            this.radioFireManual.Checked = true;
            this.radioFireManual.Location = new System.Drawing.Point(12, 20);
            this.radioFireManual.Name = "radioFireManual";
            this.radioFireManual.Size = new System.Drawing.Size(60, 17);
            this.radioFireManual.TabIndex = 0;
            this.radioFireManual.TabStop = true;
            this.radioFireManual.Text = "Manual";
            this.radioFireManual.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 456);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnFire);
            this.Controls.Add(this.grpRocketType);
            this.Controls.Add(this.systemReadingGB);
            this.Controls.Add(this.groupBox3);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Apollo";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.systemReadingGB.ResumeLayout(false);
            this.systemReadingGB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.systemReadingPB)).EndInit();
            this.grpRocketType.ResumeLayout(false);
            this.grpRocketType.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMSToOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDistanceThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPsiThreshold)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnComReload;
        private System.Windows.Forms.ComboBox cmbBRate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbComPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox systemReadingGB;
        private System.Windows.Forms.PictureBox systemReadingPB;
        private System.Windows.Forms.Label lblPSI;
        private System.Windows.Forms.Label lblRPM;
        private System.Windows.Forms.Label lblPSIValue;
        private System.Windows.Forms.Label lblRPMValue;
        private System.Windows.Forms.GroupBox grpRocketType;
        private System.Windows.Forms.ComboBox cmbRockets;
        private System.Windows.Forms.Label lblDrag;
        private System.Windows.Forms.Label lblTubeLength;
        private System.Windows.Forms.Label lblDiameter;
        private System.Windows.Forms.Label lblMass;
        private System.Windows.Forms.Button btnFire;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numPsiThreshold;
        private System.Windows.Forms.RadioButton radioFireManual;
        private System.Windows.Forms.Label lblOpenSolenoid;
        private System.Windows.Forms.TrackBar trackMSToOpen;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numDistanceThreshold;
        private System.Windows.Forms.RadioButton radioFireDistance;
        private System.Windows.Forms.RadioButton radioFirePSI;
    }
}

