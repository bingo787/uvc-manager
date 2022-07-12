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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeViewDevice = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxDevInfo = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBoxImage1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBoxFilter = new System.Windows.Forms.ComboBox();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.textBoxCheckTime = new System.Windows.Forms.TextBox();
            this.buttonSetParameter = new System.Windows.Forms.Button();
            this.buttonModifySerial = new System.Windows.Forms.Button();
            this.textBoxSerial = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCurrent = new System.Windows.Forms.TextBox();
            this.textBoxKV = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonEnumDev = new System.Windows.Forms.Button();
            this.buttonGetDevState = new System.Windows.Forms.Button();
            this.buttonAbandon = new System.Windows.Forms.Button();
            this.buttonSynchronous = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonConnectServer = new System.Windows.Forms.Button();
            this.buttonAnalyse = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.labelXrayStatus = new System.Windows.Forms.Label();
            this.buttonXrayOnOff = new System.Windows.Forms.Button();
            this.labelHVPortLED = new System.Windows.Forms.Label();
            this.buttonConnectHVPort = new System.Windows.Forms.Button();
            this.comboBoxHVStopBit = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.comBoxHVCheckBit = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.comBoxHVDataBit = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.comboBoxHVBaudRate = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.comboBoxHVPort = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.labelPLCStatus = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxIpAddress = new System.Windows.Forms.TextBox();
            this.buttonConnectPLC = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonClearImage = new System.Windows.Forms.Button();
            this.buttonM1601 = new System.Windows.Forms.Button();
            this.buttonAsynchronous = new System.Windows.Forms.Button();
            this.groupBoxStatusBar = new System.Windows.Forms.GroupBox();
            this.labelStateInfo = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBoxStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeViewDevice);
            this.groupBox1.Location = new System.Drawing.Point(13, 14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(262, 125);
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
            this.treeViewDevice.Size = new System.Drawing.Size(250, 100);
            this.treeViewDevice.TabIndex = 0;
            this.treeViewDevice.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDevice_AfterCheck);
            this.treeViewDevice.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewDevice_NodeMouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxDevInfo);
            this.groupBox2.Location = new System.Drawing.Point(293, 14);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(262, 125);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设备信息";
            // 
            // textBoxDevInfo
            // 
            this.textBoxDevInfo.Location = new System.Drawing.Point(4, 16);
            this.textBoxDevInfo.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDevInfo.Multiline = true;
            this.textBoxDevInfo.Name = "textBoxDevInfo";
            this.textBoxDevInfo.ReadOnly = true;
            this.textBoxDevInfo.Size = new System.Drawing.Size(250, 100);
            this.textBoxDevInfo.TabIndex = 0;
            this.textBoxDevInfo.Text = "设备名称:\r\n设备版本:\r\n设备路径:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBoxImage1);
            this.groupBox3.Controls.Add(this.pictureBoxImage);
            this.groupBox3.Location = new System.Drawing.Point(559, 14);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(914, 660);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图像";
            // 
            // pictureBoxImage1
            // 
            this.pictureBoxImage1.Location = new System.Drawing.Point(452, 16);
            this.pictureBoxImage1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxImage1.Name = "pictureBoxImage1";
            this.pictureBoxImage1.Size = new System.Drawing.Size(421, 624);
            this.pictureBoxImage1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImage1.TabIndex = 1;
            this.pictureBoxImage1.TabStop = false;
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Location = new System.Drawing.Point(4, 16);
            this.pictureBoxImage.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(420, 624);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImage.TabIndex = 0;
            this.pictureBoxImage.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBoxFilter);
            this.groupBox4.Controls.Add(this.comboBoxModel);
            this.groupBox4.Controls.Add(this.textBoxCheckTime);
            this.groupBox4.Controls.Add(this.buttonSetParameter);
            this.groupBox4.Controls.Add(this.buttonModifySerial);
            this.groupBox4.Controls.Add(this.textBoxSerial);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(13, 145);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(319, 161);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "传感器参数设置";
            // 
            // comboBoxFilter
            // 
            this.comboBoxFilter.FormattingEnabled = true;
            this.comboBoxFilter.Location = new System.Drawing.Point(89, 90);
            this.comboBoxFilter.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxFilter.Name = "comboBoxFilter";
            this.comboBoxFilter.Size = new System.Drawing.Size(132, 20);
            this.comboBoxFilter.TabIndex = 41;
            // 
            // comboBoxModel
            // 
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Location = new System.Drawing.Point(89, 59);
            this.comboBoxModel.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(132, 20);
            this.comboBoxModel.TabIndex = 39;
            // 
            // textBoxCheckTime
            // 
            this.textBoxCheckTime.Location = new System.Drawing.Point(89, 118);
            this.textBoxCheckTime.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCheckTime.Name = "textBoxCheckTime";
            this.textBoxCheckTime.Size = new System.Drawing.Size(132, 21);
            this.textBoxCheckTime.TabIndex = 36;
            // 
            // buttonSetParameter
            // 
            this.buttonSetParameter.Location = new System.Drawing.Point(227, 118);
            this.buttonSetParameter.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSetParameter.Name = "buttonSetParameter";
            this.buttonSetParameter.Size = new System.Drawing.Size(75, 23);
            this.buttonSetParameter.TabIndex = 23;
            this.buttonSetParameter.Text = "设置";
            this.buttonSetParameter.UseVisualStyleBackColor = true;
            this.buttonSetParameter.Click += new System.EventHandler(this.buttonSetParameter_Click);
            // 
            // buttonModifySerial
            // 
            this.buttonModifySerial.Location = new System.Drawing.Point(227, 27);
            this.buttonModifySerial.Margin = new System.Windows.Forms.Padding(2);
            this.buttonModifySerial.Name = "buttonModifySerial";
            this.buttonModifySerial.Size = new System.Drawing.Size(75, 23);
            this.buttonModifySerial.TabIndex = 17;
            this.buttonModifySerial.Text = "修改序列号";
            this.buttonModifySerial.UseVisualStyleBackColor = true;
            this.buttonModifySerial.Click += new System.EventHandler(this.buttonModifySerial_Click);
            // 
            // textBoxSerial
            // 
            this.textBoxSerial.Location = new System.Drawing.Point(89, 29);
            this.textBoxSerial.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSerial.Name = "textBoxSerial";
            this.textBoxSerial.Size = new System.Drawing.Size(132, 21);
            this.textBoxSerial.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 59);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "出图模式:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 88);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "图像处理:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 118);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "曝光时间(ms):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备序列号:";
            // 
            // textBoxCurrent
            // 
            this.textBoxCurrent.Location = new System.Drawing.Point(224, 60);
            this.textBoxCurrent.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCurrent.Name = "textBoxCurrent";
            this.textBoxCurrent.Size = new System.Drawing.Size(61, 21);
            this.textBoxCurrent.TabIndex = 27;
            this.textBoxCurrent.Text = "0";
            this.textBoxCurrent.TextChanged += new System.EventHandler(this.textBoxCurrent_TextChanged);
            // 
            // textBoxKV
            // 
            this.textBoxKV.Location = new System.Drawing.Point(224, 32);
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
            this.label13.Location = new System.Drawing.Point(158, 64);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 12);
            this.label13.TabIndex = 12;
            this.label13.Text = "电流(mA):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "电压(kV):";
            // 
            // buttonEnumDev
            // 
            this.buttonEnumDev.Location = new System.Drawing.Point(60, 19);
            this.buttonEnumDev.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEnumDev.Name = "buttonEnumDev";
            this.buttonEnumDev.Size = new System.Drawing.Size(86, 23);
            this.buttonEnumDev.TabIndex = 4;
            this.buttonEnumDev.Text = "枚举设备";
            this.buttonEnumDev.UseVisualStyleBackColor = true;
            this.buttonEnumDev.Click += new System.EventHandler(this.buttonEnumDev_Click);
            // 
            // buttonGetDevState
            // 
            this.buttonGetDevState.Location = new System.Drawing.Point(60, 66);
            this.buttonGetDevState.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGetDevState.Name = "buttonGetDevState";
            this.buttonGetDevState.Size = new System.Drawing.Size(86, 23);
            this.buttonGetDevState.TabIndex = 18;
            this.buttonGetDevState.Text = "获取设备状态";
            this.buttonGetDevState.UseVisualStyleBackColor = true;
            this.buttonGetDevState.Click += new System.EventHandler(this.buttonGetDevState_Click);
            // 
            // buttonAbandon
            // 
            this.buttonAbandon.Location = new System.Drawing.Point(60, 254);
            this.buttonAbandon.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAbandon.Name = "buttonAbandon";
            this.buttonAbandon.Size = new System.Drawing.Size(86, 23);
            this.buttonAbandon.TabIndex = 19;
            this.buttonAbandon.Text = "中断图像获取";
            this.buttonAbandon.UseVisualStyleBackColor = true;
            this.buttonAbandon.Click += new System.EventHandler(this.buttonAbandon_Click);
            // 
            // buttonSynchronous
            // 
            this.buttonSynchronous.Location = new System.Drawing.Point(60, 160);
            this.buttonSynchronous.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSynchronous.Name = "buttonSynchronous";
            this.buttonSynchronous.Size = new System.Drawing.Size(86, 23);
            this.buttonSynchronous.TabIndex = 21;
            this.buttonSynchronous.Text = "同步获取图像";
            this.buttonSynchronous.UseVisualStyleBackColor = true;
            this.buttonSynchronous.Click += new System.EventHandler(this.buttonSynchronous_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(60, 438);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(86, 23);
            this.buttonExit.TabIndex = 22;
            this.buttonExit.Text = "退出软件";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonConnectServer
            // 
            this.buttonConnectServer.Location = new System.Drawing.Point(60, 113);
            this.buttonConnectServer.Name = "buttonConnectServer";
            this.buttonConnectServer.Size = new System.Drawing.Size(86, 23);
            this.buttonConnectServer.TabIndex = 23;
            this.buttonConnectServer.Text = "连接分析服务";
            this.buttonConnectServer.UseVisualStyleBackColor = true;
            this.buttonConnectServer.Click += new System.EventHandler(this.buttonConnectServer_Click);
            // 
            // buttonAnalyse
            // 
            this.buttonAnalyse.Location = new System.Drawing.Point(60, 301);
            this.buttonAnalyse.Name = "buttonAnalyse";
            this.buttonAnalyse.Size = new System.Drawing.Size(86, 23);
            this.buttonAnalyse.TabIndex = 24;
            this.buttonAnalyse.Text = "分析图像";
            this.buttonAnalyse.UseVisualStyleBackColor = true;
            this.buttonAnalyse.Click += new System.EventHandler(this.buttonAnalyse_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.labelXrayStatus);
            this.groupBox5.Controls.Add(this.buttonXrayOnOff);
            this.groupBox5.Controls.Add(this.labelHVPortLED);
            this.groupBox5.Controls.Add(this.buttonConnectHVPort);
            this.groupBox5.Controls.Add(this.comboBoxHVStopBit);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.comBoxHVCheckBit);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.comBoxHVDataBit);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.comboBoxHVBaudRate);
            this.groupBox5.Controls.Add(this.label20);
            this.groupBox5.Controls.Add(this.comboBoxHVPort);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.textBoxCurrent);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.textBoxKV);
            this.groupBox5.Location = new System.Drawing.Point(13, 321);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(319, 217);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "高压参数设置";
            // 
            // labelXrayStatus
            // 
            this.labelXrayStatus.AutoSize = true;
            this.labelXrayStatus.Location = new System.Drawing.Point(192, 94);
            this.labelXrayStatus.Name = "labelXrayStatus";
            this.labelXrayStatus.Size = new System.Drawing.Size(11, 12);
            this.labelXrayStatus.TabIndex = 48;
            this.labelXrayStatus.Text = "O";
            // 
            // buttonXrayOnOff
            // 
            this.buttonXrayOnOff.Location = new System.Drawing.Point(222, 89);
            this.buttonXrayOnOff.Name = "buttonXrayOnOff";
            this.buttonXrayOnOff.Size = new System.Drawing.Size(63, 23);
            this.buttonXrayOnOff.TabIndex = 47;
            this.buttonXrayOnOff.Text = "打开光源";
            this.buttonXrayOnOff.UseVisualStyleBackColor = true;
            this.buttonXrayOnOff.Click += new System.EventHandler(this.buttonXrayOnOff_Click);
            // 
            // labelHVPortLED
            // 
            this.labelHVPortLED.AutoSize = true;
            this.labelHVPortLED.Location = new System.Drawing.Point(29, 180);
            this.labelHVPortLED.Name = "labelHVPortLED";
            this.labelHVPortLED.Size = new System.Drawing.Size(11, 12);
            this.labelHVPortLED.TabIndex = 46;
            this.labelHVPortLED.Text = "O";
            // 
            // buttonConnectHVPort
            // 
            this.buttonConnectHVPort.Location = new System.Drawing.Point(64, 175);
            this.buttonConnectHVPort.Name = "buttonConnectHVPort";
            this.buttonConnectHVPort.Size = new System.Drawing.Size(63, 23);
            this.buttonConnectHVPort.TabIndex = 2;
            this.buttonConnectHVPort.Text = "打开串口";
            this.buttonConnectHVPort.UseVisualStyleBackColor = true;
            this.buttonConnectHVPort.Click += new System.EventHandler(this.buttonConnectHVPort_Click);
            // 
            // comboBoxHVStopBit
            // 
            this.comboBoxHVStopBit.FormattingEnabled = true;
            this.comboBoxHVStopBit.Location = new System.Drawing.Point(65, 145);
            this.comboBoxHVStopBit.Name = "comboBoxHVStopBit";
            this.comboBoxHVStopBit.Size = new System.Drawing.Size(62, 20);
            this.comboBoxHVStopBit.TabIndex = 1;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(12, 148);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(47, 12);
            this.label23.TabIndex = 0;
            this.label23.Text = "停止位:";
            // 
            // comBoxHVCheckBit
            // 
            this.comBoxHVCheckBit.FormattingEnabled = true;
            this.comBoxHVCheckBit.Location = new System.Drawing.Point(65, 119);
            this.comBoxHVCheckBit.Name = "comBoxHVCheckBit";
            this.comBoxHVCheckBit.Size = new System.Drawing.Size(62, 20);
            this.comBoxHVCheckBit.TabIndex = 1;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(12, 122);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(47, 12);
            this.label22.TabIndex = 0;
            this.label22.Text = "校验位:";
            // 
            // comBoxHVDataBit
            // 
            this.comBoxHVDataBit.FormattingEnabled = true;
            this.comBoxHVDataBit.Location = new System.Drawing.Point(65, 89);
            this.comBoxHVDataBit.Name = "comBoxHVDataBit";
            this.comBoxHVDataBit.Size = new System.Drawing.Size(62, 20);
            this.comBoxHVDataBit.TabIndex = 1;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(12, 92);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(47, 12);
            this.label21.TabIndex = 0;
            this.label21.Text = "数据位:";
            // 
            // comboBoxHVBaudRate
            // 
            this.comboBoxHVBaudRate.FormattingEnabled = true;
            this.comboBoxHVBaudRate.Location = new System.Drawing.Point(65, 61);
            this.comboBoxHVBaudRate.Name = "comboBoxHVBaudRate";
            this.comboBoxHVBaudRate.Size = new System.Drawing.Size(62, 20);
            this.comboBoxHVBaudRate.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(12, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(47, 12);
            this.label20.TabIndex = 0;
            this.label20.Text = "波特率:";
            // 
            // comboBoxHVPort
            // 
            this.comboBoxHVPort.FormattingEnabled = true;
            this.comboBoxHVPort.Location = new System.Drawing.Point(65, 33);
            this.comboBoxHVPort.Name = "comboBoxHVPort";
            this.comboBoxHVPort.Size = new System.Drawing.Size(62, 20);
            this.comboBoxHVPort.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(12, 36);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 12);
            this.label17.TabIndex = 0;
            this.label17.Text = "端口号:";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.labelPLCStatus);
            this.groupBox6.Controls.Add(this.textBoxPort);
            this.groupBox6.Controls.Add(this.textBoxIpAddress);
            this.groupBox6.Controls.Add(this.buttonConnectPLC);
            this.groupBox6.Controls.Add(this.label27);
            this.groupBox6.Controls.Add(this.label28);
            this.groupBox6.Location = new System.Drawing.Point(12, 544);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(320, 130);
            this.groupBox6.TabIndex = 26;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "PLC端口参数";
            // 
            // labelPLCStatus
            // 
            this.labelPLCStatus.AutoSize = true;
            this.labelPLCStatus.Location = new System.Drawing.Point(33, 93);
            this.labelPLCStatus.Name = "labelPLCStatus";
            this.labelPLCStatus.Size = new System.Drawing.Size(11, 12);
            this.labelPLCStatus.TabIndex = 49;
            this.labelPLCStatus.Text = "O";
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
            // buttonConnectPLC
            // 
            this.buttonConnectPLC.Location = new System.Drawing.Point(60, 88);
            this.buttonConnectPLC.Name = "buttonConnectPLC";
            this.buttonConnectPLC.Size = new System.Drawing.Size(100, 22);
            this.buttonConnectPLC.TabIndex = 2;
            this.buttonConnectPLC.Text = "连接";
            this.buttonConnectPLC.UseVisualStyleBackColor = true;
            this.buttonConnectPLC.Click += new System.EventHandler(this.buttonConnectPLC_Click);
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
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonClearImage);
            this.groupBox7.Controls.Add(this.buttonM1601);
            this.groupBox7.Controls.Add(this.buttonSynchronous);
            this.groupBox7.Controls.Add(this.buttonEnumDev);
            this.groupBox7.Controls.Add(this.buttonGetDevState);
            this.groupBox7.Controls.Add(this.buttonAbandon);
            this.groupBox7.Controls.Add(this.buttonAsynchronous);
            this.groupBox7.Controls.Add(this.buttonExit);
            this.groupBox7.Controls.Add(this.buttonAnalyse);
            this.groupBox7.Controls.Add(this.buttonConnectServer);
            this.groupBox7.Location = new System.Drawing.Point(363, 146);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(184, 528);
            this.groupBox7.TabIndex = 27;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "控制区域";
            // 
            // buttonClearImage
            // 
            this.buttonClearImage.Location = new System.Drawing.Point(61, 393);
            this.buttonClearImage.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClearImage.Name = "buttonClearImage";
            this.buttonClearImage.Size = new System.Drawing.Size(85, 21);
            this.buttonClearImage.TabIndex = 26;
            this.buttonClearImage.Text = "清空显示";
            this.buttonClearImage.UseVisualStyleBackColor = true;
            this.buttonClearImage.Click += new System.EventHandler(this.buttonD999_Click);
            // 
            // buttonM1601
            // 
            this.buttonM1601.Location = new System.Drawing.Point(61, 348);
            this.buttonM1601.Margin = new System.Windows.Forms.Padding(2);
            this.buttonM1601.Name = "buttonM1601";
            this.buttonM1601.Size = new System.Drawing.Size(85, 21);
            this.buttonM1601.TabIndex = 25;
            this.buttonM1601.Text = "触发采图";
            this.buttonM1601.UseVisualStyleBackColor = true;
            this.buttonM1601.Click += new System.EventHandler(this.buttonM1601_Click);
            // 
            // buttonAsynchronous
            // 
            this.buttonAsynchronous.Location = new System.Drawing.Point(60, 207);
            this.buttonAsynchronous.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAsynchronous.Name = "buttonAsynchronous";
            this.buttonAsynchronous.Size = new System.Drawing.Size(86, 23);
            this.buttonAsynchronous.TabIndex = 20;
            this.buttonAsynchronous.Text = "异步获取图像";
            this.buttonAsynchronous.UseVisualStyleBackColor = true;
            this.buttonAsynchronous.Click += new System.EventHandler(this.buttonAsynchronous_Click);
            // 
            // groupBoxStatusBar
            // 
            this.groupBoxStatusBar.Controls.Add(this.labelStateInfo);
            this.groupBoxStatusBar.Location = new System.Drawing.Point(559, 679);
            this.groupBoxStatusBar.Name = "groupBoxStatusBar";
            this.groupBoxStatusBar.Size = new System.Drawing.Size(914, 42);
            this.groupBoxStatusBar.TabIndex = 28;
            this.groupBoxStatusBar.TabStop = false;
            this.groupBoxStatusBar.Text = "系统状态";
            // 
            // labelStateInfo
            // 
            this.labelStateInfo.AutoSize = true;
            this.labelStateInfo.Location = new System.Drawing.Point(67, 17);
            this.labelStateInfo.Name = "labelStateInfo";
            this.labelStateInfo.Size = new System.Drawing.Size(65, 12);
            this.labelStateInfo.TabIndex = 0;
            this.labelStateInfo.Text = "设备未打开";
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1484, 761);
            this.Controls.Add(this.groupBoxStatusBar);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1500, 1000);
            this.MinimumSize = new System.Drawing.Size(1500, 705);
            this.Name = "Demo";
            this.Text = "睿奥自动化检测控制软件";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Demo_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBoxStatusBar.ResumeLayout(false);
            this.groupBoxStatusBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonEnumDev;
        private System.Windows.Forms.TreeView treeViewDevice;
        private System.Windows.Forms.TextBox textBoxDevInfo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox comboBoxModel;
        private System.Windows.Forms.TextBox textBoxCheckTime;
        private System.Windows.Forms.TextBox textBoxCurrent;
        private System.Windows.Forms.TextBox textBoxKV;
        private System.Windows.Forms.Button buttonSetParameter;
        private System.Windows.Forms.Button buttonModifySerial;
        private System.Windows.Forms.TextBox textBoxSerial;
        private System.Windows.Forms.Button buttonGetDevState;
        private System.Windows.Forms.Button buttonAbandon;
        private System.Windows.Forms.Button buttonSynchronous;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.Button buttonConnectServer;
        private System.Windows.Forms.Button buttonAnalyse;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox comboBoxHVStopBit;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox comBoxHVCheckBit;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox comBoxHVDataBit;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox comboBoxHVBaudRate;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox comboBoxHVPort;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button buttonConnectHVPort;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button buttonConnectPLC;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label labelHVPortLED;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label labelXrayStatus;
        private System.Windows.Forms.Button buttonXrayOnOff;
        private System.Windows.Forms.Button buttonAsynchronous;
        private System.Windows.Forms.Button buttonM1601;
        private System.Windows.Forms.Button buttonClearImage;
        private System.Windows.Forms.PictureBox pictureBoxImage1;
        private System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelPLCStatus;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxIpAddress;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.GroupBox groupBoxStatusBar;
        private System.Windows.Forms.Label labelStateInfo;
    }
}

