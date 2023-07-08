namespace LionSDKDotDemo
{
    partial class Demo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Demo));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeViewDevice = new System.Windows.Forms.TreeView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBoxImage1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.textBoxActTime = new System.Windows.Forms.TextBox();
            this.buttonSetParameter = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCurrent = new System.Windows.Forms.TextBox();
            this.textBoxKV = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonEnumDev = new System.Windows.Forms.Button();
            this.buttonAbandon = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pictureBoxXrayOnOff = new System.Windows.Forms.PictureBox();
            this.comboBoxHVPort = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.buttonXrayOnOff = new System.Windows.Forms.Button();
            this.buttonConnectHVPort = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxIpAddress = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.buttonConnectPLC = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonClearImage = new System.Windows.Forms.Button();
            this.buttonAsynchronous = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel_PLCConn = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_HVConn = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_XrayOnOff = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_HV_KV = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_HV_Cur = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_HV_Temp = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_HVError = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel_Time = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel_ProgressInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerDateTime = new System.Windows.Forms.Timer(this.components);
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxXrayOnOff)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeViewDevice);
            this.groupBox1.Location = new System.Drawing.Point(13, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(324, 96);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备列表";
            // 
            // treeViewDevice
            // 
            this.treeViewDevice.CheckBoxes = true;
            this.treeViewDevice.FullRowSelect = true;
            this.treeViewDevice.Location = new System.Drawing.Point(4, 16);
            this.treeViewDevice.Margin = new System.Windows.Forms.Padding(2);
            this.treeViewDevice.Name = "treeViewDevice";
            this.treeViewDevice.Size = new System.Drawing.Size(310, 70);
            this.treeViewDevice.TabIndex = 0;
            this.treeViewDevice.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDevice_AfterCheck);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBoxImage1);
            this.groupBox3.Controls.Add(this.pictureBoxImage);
            this.groupBox3.Location = new System.Drawing.Point(338, 14);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(1135, 622);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图像";
            // 
            // pictureBoxImage1
            // 
            this.pictureBoxImage1.Location = new System.Drawing.Point(568, 16);
            this.pictureBoxImage1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxImage1.Name = "pictureBoxImage1";
            this.pictureBoxImage1.Size = new System.Drawing.Size(550, 600);
            this.pictureBoxImage1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImage1.TabIndex = 1;
            this.pictureBoxImage1.TabStop = false;
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Location = new System.Drawing.Point(4, 16);
            this.pictureBoxImage.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(550, 600);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImage.TabIndex = 0;
            this.pictureBoxImage.TabStop = false;
            // 
            // textBoxActTime
            // 
            this.textBoxActTime.Location = new System.Drawing.Point(98, 107);
            this.textBoxActTime.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxActTime.Name = "textBoxActTime";
            this.textBoxActTime.Size = new System.Drawing.Size(62, 21);
            this.textBoxActTime.TabIndex = 36;
            this.textBoxActTime.TextChanged += new System.EventHandler(this.textBoxActTime_TextChanged);
            // 
            // buttonSetParameter
            // 
            this.buttonSetParameter.Location = new System.Drawing.Point(115, 104);
            this.buttonSetParameter.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSetParameter.Name = "buttonSetParameter";
            this.buttonSetParameter.Size = new System.Drawing.Size(89, 21);
            this.buttonSetParameter.TabIndex = 23;
            this.buttonSetParameter.Text = "设置采集时间";
            this.buttonSetParameter.UseVisualStyleBackColor = true;
            this.buttonSetParameter.Click += new System.EventHandler(this.buttonSetParameter_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 111);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "采集时间(ms):";
            // 
            // textBoxCurrent
            // 
            this.textBoxCurrent.Location = new System.Drawing.Point(99, 81);
            this.textBoxCurrent.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCurrent.Name = "textBoxCurrent";
            this.textBoxCurrent.Size = new System.Drawing.Size(61, 21);
            this.textBoxCurrent.TabIndex = 27;
            this.textBoxCurrent.Text = "0";
            this.textBoxCurrent.TextChanged += new System.EventHandler(this.textBoxCurrent_TextChanged);
            // 
            // textBoxKV
            // 
            this.textBoxKV.Location = new System.Drawing.Point(99, 55);
            this.textBoxKV.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxKV.Name = "textBoxKV";
            this.textBoxKV.Size = new System.Drawing.Size(61, 21);
            this.textBoxKV.TabIndex = 26;
            this.textBoxKV.Text = "0";
            this.textBoxKV.TextChanged += new System.EventHandler(this.textBoxKV_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 85);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 12;
            this.label13.Text = "功率(W):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "电压(kV):";
            // 
            // buttonEnumDev
            // 
            this.buttonEnumDev.Location = new System.Drawing.Point(14, 30);
            this.buttonEnumDev.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEnumDev.Name = "buttonEnumDev";
            this.buttonEnumDev.Size = new System.Drawing.Size(86, 23);
            this.buttonEnumDev.TabIndex = 4;
            this.buttonEnumDev.Text = "枚举设备";
            this.buttonEnumDev.UseVisualStyleBackColor = true;
            this.buttonEnumDev.Click += new System.EventHandler(this.buttonEnumDev_Click);
            // 
            // buttonAbandon
            // 
            this.buttonAbandon.Location = new System.Drawing.Point(207, 31);
            this.buttonAbandon.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAbandon.Name = "buttonAbandon";
            this.buttonAbandon.Size = new System.Drawing.Size(86, 23);
            this.buttonAbandon.TabIndex = 19;
            this.buttonAbandon.Text = "中断图像获取";
            this.buttonAbandon.UseVisualStyleBackColor = true;
            this.buttonAbandon.Click += new System.EventHandler(this.buttonAbandon_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxActTime);
            this.groupBox5.Controls.Add(this.pictureBoxXrayOnOff);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.comboBoxHVPort);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.textBoxCurrent);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.textBoxKV);
            this.groupBox5.Location = new System.Drawing.Point(13, 115);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(324, 145);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "X光设备参数";
            // 
            // pictureBoxXrayOnOff
            // 
            this.pictureBoxXrayOnOff.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxXrayOnOff.InitialImage")));
            this.pictureBoxXrayOnOff.Location = new System.Drawing.Point(201, 25);
            this.pictureBoxXrayOnOff.Name = "pictureBoxXrayOnOff";
            this.pictureBoxXrayOnOff.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxXrayOnOff.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxXrayOnOff.TabIndex = 48;
            this.pictureBoxXrayOnOff.TabStop = false;
            // 
            // comboBoxHVPort
            // 
            this.comboBoxHVPort.FormattingEnabled = true;
            this.comboBoxHVPort.Location = new System.Drawing.Point(99, 30);
            this.comboBoxHVPort.Name = "comboBoxHVPort";
            this.comboBoxHVPort.Size = new System.Drawing.Size(62, 20);
            this.comboBoxHVPort.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 33);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 12);
            this.label17.TabIndex = 0;
            this.label17.Text = "端口号:";
            // 
            // buttonXrayOnOff
            // 
            this.buttonXrayOnOff.Location = new System.Drawing.Point(115, 67);
            this.buttonXrayOnOff.Name = "buttonXrayOnOff";
            this.buttonXrayOnOff.Size = new System.Drawing.Size(85, 23);
            this.buttonXrayOnOff.TabIndex = 47;
            this.buttonXrayOnOff.Text = "打开光源";
            this.buttonXrayOnOff.UseVisualStyleBackColor = true;
            this.buttonXrayOnOff.Click += new System.EventHandler(this.buttonXrayOnOff_Click);
            // 
            // buttonConnectHVPort
            // 
            this.buttonConnectHVPort.Location = new System.Drawing.Point(14, 67);
            this.buttonConnectHVPort.Name = "buttonConnectHVPort";
            this.buttonConnectHVPort.Size = new System.Drawing.Size(86, 23);
            this.buttonConnectHVPort.TabIndex = 2;
            this.buttonConnectHVPort.Text = "打开光源串口";
            this.buttonConnectHVPort.UseVisualStyleBackColor = true;
            this.buttonConnectHVPort.Click += new System.EventHandler(this.buttonConnectHVPort_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBoxPort);
            this.groupBox6.Controls.Add(this.textBoxIpAddress);
            this.groupBox6.Controls.Add(this.label27);
            this.groupBox6.Controls.Add(this.label28);
            this.groupBox6.Location = new System.Drawing.Point(17, 266);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(320, 91);
            this.groupBox6.TabIndex = 26;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "PLC端口参数";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(60, 56);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(100, 21);
            this.textBoxPort.TabIndex = 4;
            this.textBoxPort.Text = "502";
            // 
            // textBoxIpAddress
            // 
            this.textBoxIpAddress.Location = new System.Drawing.Point(60, 28);
            this.textBoxIpAddress.Name = "textBoxIpAddress";
            this.textBoxIpAddress.Size = new System.Drawing.Size(100, 21);
            this.textBoxIpAddress.TabIndex = 3;
            this.textBoxIpAddress.Text = "192.168.1.31";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 32);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(47, 12);
            this.label27.TabIndex = 0;
            this.label27.Text = "IP地址:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(18, 59);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(35, 12);
            this.label28.TabIndex = 0;
            this.label28.Text = "端口:";
            // 
            // buttonConnectPLC
            // 
            this.buttonConnectPLC.Location = new System.Drawing.Point(210, 67);
            this.buttonConnectPLC.Name = "buttonConnectPLC";
            this.buttonConnectPLC.Size = new System.Drawing.Size(83, 23);
            this.buttonConnectPLC.TabIndex = 2;
            this.buttonConnectPLC.Text = "连接PLC";
            this.buttonConnectPLC.UseVisualStyleBackColor = true;
            this.buttonConnectPLC.Click += new System.EventHandler(this.buttonConnectPLC_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonClearImage);
            this.groupBox7.Controls.Add(this.buttonSetParameter);
            this.groupBox7.Controls.Add(this.buttonXrayOnOff);
            this.groupBox7.Controls.Add(this.buttonConnectPLC);
            this.groupBox7.Controls.Add(this.buttonEnumDev);
            this.groupBox7.Controls.Add(this.buttonAbandon);
            this.groupBox7.Controls.Add(this.buttonConnectHVPort);
            this.groupBox7.Controls.Add(this.buttonAsynchronous);
            this.groupBox7.Location = new System.Drawing.Point(11, 363);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(326, 142);
            this.groupBox7.TabIndex = 27;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "调试区域";
            // 
            // buttonClearImage
            // 
            this.buttonClearImage.Location = new System.Drawing.Point(15, 104);
            this.buttonClearImage.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClearImage.Name = "buttonClearImage";
            this.buttonClearImage.Size = new System.Drawing.Size(85, 21);
            this.buttonClearImage.TabIndex = 26;
            this.buttonClearImage.Text = "清空显示";
            this.buttonClearImage.UseVisualStyleBackColor = true;
            this.buttonClearImage.Click += new System.EventHandler(this.buttonClearImage_Click);
            // 
            // buttonAsynchronous
            // 
            this.buttonAsynchronous.Location = new System.Drawing.Point(114, 31);
            this.buttonAsynchronous.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAsynchronous.Name = "buttonAsynchronous";
            this.buttonAsynchronous.Size = new System.Drawing.Size(86, 23);
            this.buttonAsynchronous.TabIndex = 20;
            this.buttonAsynchronous.Text = "获取图像";
            this.buttonAsynchronous.UseVisualStyleBackColor = true;
            this.buttonAsynchronous.Click += new System.EventHandler(this.buttonAsynchronous_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel_PLCConn,
            this.toolStripStatusLabel_HVConn,
            this.toolStripStatusLabel_XrayOnOff,
            this.toolStripStatusLabel_HV_KV,
            this.toolStripStatusLabel_HV_Cur,
            this.toolStripStatusLabel_HV_Temp,
            this.toolStripStatusLabel_HVError,
            this.toolStripProgressBar,
            this.toolStripStatusLabel_Time,
            this.toolStripStatusLabel_ProgressInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1484, 22);
            this.statusStrip1.TabIndex = 29;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_PLCConn
            // 
            this.toolStripStatusLabel_PLCConn.Name = "toolStripStatusLabel_PLCConn";
            this.toolStripStatusLabel_PLCConn.Size = new System.Drawing.Size(70, 17);
            this.toolStripStatusLabel_PLCConn.Text = "PLC-未连接";
            // 
            // toolStripStatusLabel_HVConn
            // 
            this.toolStripStatusLabel_HVConn.Name = "toolStripStatusLabel_HVConn";
            this.toolStripStatusLabel_HVConn.Size = new System.Drawing.Size(73, 17);
            this.toolStripStatusLabel_HVConn.Text = "高压-未连接";
            // 
            // toolStripStatusLabel_XrayOnOff
            // 
            this.toolStripStatusLabel_XrayOnOff.Name = "toolStripStatusLabel_XrayOnOff";
            this.toolStripStatusLabel_XrayOnOff.Size = new System.Drawing.Size(61, 17);
            this.toolStripStatusLabel_XrayOnOff.Text = "Xray-OFF";
            // 
            // toolStripStatusLabel_HV_KV
            // 
            this.toolStripStatusLabel_HV_KV.Name = "toolStripStatusLabel_HV_KV";
            this.toolStripStatusLabel_HV_KV.Size = new System.Drawing.Size(30, 17);
            this.toolStripStatusLabel_HV_KV.Text = "0kV";
            // 
            // toolStripStatusLabel_HV_Cur
            // 
            this.toolStripStatusLabel_HV_Cur.Name = "toolStripStatusLabel_HV_Cur";
            this.toolStripStatusLabel_HV_Cur.Size = new System.Drawing.Size(30, 17);
            this.toolStripStatusLabel_HV_Cur.Text = "0uA";
            // 
            // toolStripStatusLabel_HV_Temp
            // 
            this.toolStripStatusLabel_HV_Temp.Name = "toolStripStatusLabel_HV_Temp";
            this.toolStripStatusLabel_HV_Temp.Size = new System.Drawing.Size(23, 17);
            this.toolStripStatusLabel_HV_Temp.Text = "0C";
            // 
            // toolStripStatusLabel_HVError
            // 
            this.toolStripStatusLabel_HVError.Name = "toolStripStatusLabel_HVError";
            this.toolStripStatusLabel_HVError.Size = new System.Drawing.Size(18, 17);
            this.toolStripStatusLabel_HVError.Text = "--";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(200, 16);
            // 
            // toolStripStatusLabel_Time
            // 
            this.toolStripStatusLabel_Time.Name = "toolStripStatusLabel_Time";
            this.toolStripStatusLabel_Time.Size = new System.Drawing.Size(122, 17);
            this.toolStripStatusLabel_Time.Text = "2022.11.11 12:25:59";
            // 
            // toolStripStatusLabel_ProgressInfo
            // 
            this.toolStripStatusLabel_ProgressInfo.Name = "toolStripStatusLabel_ProgressInfo";
            this.toolStripStatusLabel_ProgressInfo.Size = new System.Drawing.Size(18, 17);
            this.toolStripStatusLabel_ProgressInfo.Text = "--";
            // 
            // timerDateTime
            // 
            this.timerDateTime.Enabled = true;
            this.timerDateTime.Interval = 1000;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.buttonStart);
            this.groupBox8.Location = new System.Drawing.Point(4, 511);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(333, 119);
            this.groupBox8.TabIndex = 30;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "控制区域";
            // 
            // buttonStart
            // 
            this.buttonStart.BackColor = System.Drawing.Color.Yellow;
            this.buttonStart.Font = new System.Drawing.Font("宋体", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonStart.Location = new System.Drawing.Point(9, 20);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(314, 93);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "一键启动";
            this.buttonStart.UseVisualStyleBackColor = false;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 661);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1500, 700);
            this.MinimumSize = new System.Drawing.Size(1500, 700);
            this.Name = "Demo";
            this.Text = "睿奥无损检测系统(v1.0.0.20230708)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Demo_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxXrayOnOff)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonEnumDev;
        private System.Windows.Forms.TreeView treeViewDevice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxActTime;
        private System.Windows.Forms.TextBox textBoxCurrent;
        private System.Windows.Forms.TextBox textBoxKV;
        private System.Windows.Forms.Button buttonSetParameter;
        private System.Windows.Forms.Button buttonAbandon;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox comboBoxHVPort;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button buttonConnectHVPort;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button buttonConnectPLC;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button buttonXrayOnOff;
        private System.Windows.Forms.Button buttonAsynchronous;
        private System.Windows.Forms.Button buttonClearImage;
        private System.Windows.Forms.PictureBox pictureBoxImage1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxIpAddress;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_PLCConn;
        private System.Windows.Forms.Timer timerDateTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_HVConn;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_HV_KV;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_HV_Cur;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_HV_Temp;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_XrayOnOff;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_Time;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_ProgressInfo;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel_HVError;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.PictureBox pictureBoxXrayOnOff;
        private System.Windows.Forms.Button buttonStart;
    }
}

