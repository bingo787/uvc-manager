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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxDevInfo = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBoxImage1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBoxFilter = new System.Windows.Forms.ComboBox();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.textBoxActTime = new System.Windows.Forms.TextBox();
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
            this.buttonAbandon = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pictureBoxXrayOnOff = new System.Windows.Forms.PictureBox();
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
            this.buttonConnectServer = new System.Windows.Forms.Button();
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
            this.radioButtonAutoMode = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.radioButtonManualMode = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.groupBox4.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(26, 28);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(524, 250);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设备列表";
            // 
            // treeViewDevice
            // 
            this.treeViewDevice.CheckBoxes = true;
            this.treeViewDevice.FullRowSelect = true;
            this.treeViewDevice.Location = new System.Drawing.Point(8, 32);
            this.treeViewDevice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.treeViewDevice.Name = "treeViewDevice";
            this.treeViewDevice.Size = new System.Drawing.Size(496, 196);
            this.treeViewDevice.TabIndex = 0;
            this.treeViewDevice.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDevice_AfterCheck);
            this.treeViewDevice.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewDevice_NodeMouseClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxDevInfo);
            this.groupBox2.Location = new System.Drawing.Point(558, 28);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(524, 250);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设备信息";
            // 
            // textBoxDevInfo
            // 
            this.textBoxDevInfo.Location = new System.Drawing.Point(8, 32);
            this.textBoxDevInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxDevInfo.Multiline = true;
            this.textBoxDevInfo.Name = "textBoxDevInfo";
            this.textBoxDevInfo.ReadOnly = true;
            this.textBoxDevInfo.Size = new System.Drawing.Size(496, 196);
            this.textBoxDevInfo.TabIndex = 0;
            this.textBoxDevInfo.Text = "设备名称:\r\n设备版本:\r\n设备路径:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBoxImage1);
            this.groupBox3.Controls.Add(this.pictureBoxImage);
            this.groupBox3.Location = new System.Drawing.Point(1118, 28);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(1828, 1202);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图像";
            // 
            // pictureBoxImage1
            // 
            this.pictureBoxImage1.Location = new System.Drawing.Point(904, 32);
            this.pictureBoxImage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxImage1.Name = "pictureBoxImage1";
            this.pictureBoxImage1.Size = new System.Drawing.Size(830, 1140);
            this.pictureBoxImage1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImage1.TabIndex = 1;
            this.pictureBoxImage1.TabStop = false;
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Location = new System.Drawing.Point(8, 32);
            this.pictureBoxImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(830, 1140);
            this.pictureBoxImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxImage.TabIndex = 0;
            this.pictureBoxImage.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBoxFilter);
            this.groupBox4.Controls.Add(this.comboBoxModel);
            this.groupBox4.Controls.Add(this.textBoxActTime);
            this.groupBox4.Controls.Add(this.buttonSetParameter);
            this.groupBox4.Controls.Add(this.buttonModifySerial);
            this.groupBox4.Controls.Add(this.textBoxSerial);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(26, 290);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(638, 322);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "传感器参数设置";
            // 
            // comboBoxFilter
            // 
            this.comboBoxFilter.FormattingEnabled = true;
            this.comboBoxFilter.Location = new System.Drawing.Point(178, 180);
            this.comboBoxFilter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxFilter.Name = "comboBoxFilter";
            this.comboBoxFilter.Size = new System.Drawing.Size(260, 32);
            this.comboBoxFilter.TabIndex = 41;
            // 
            // comboBoxModel
            // 
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Location = new System.Drawing.Point(178, 118);
            this.comboBoxModel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(260, 32);
            this.comboBoxModel.TabIndex = 39;
            // 
            // textBoxActTime
            // 
            this.textBoxActTime.Location = new System.Drawing.Point(178, 236);
            this.textBoxActTime.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxActTime.Name = "textBoxActTime";
            this.textBoxActTime.Size = new System.Drawing.Size(260, 35);
            this.textBoxActTime.TabIndex = 36;
            this.textBoxActTime.TextChanged += new System.EventHandler(this.textBoxActTime_TextChanged);
            // 
            // buttonSetParameter
            // 
            this.buttonSetParameter.Location = new System.Drawing.Point(454, 236);
            this.buttonSetParameter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonSetParameter.Name = "buttonSetParameter";
            this.buttonSetParameter.Size = new System.Drawing.Size(150, 46);
            this.buttonSetParameter.TabIndex = 23;
            this.buttonSetParameter.Text = "设置";
            this.buttonSetParameter.UseVisualStyleBackColor = true;
            this.buttonSetParameter.Click += new System.EventHandler(this.buttonSetParameter_Click);
            // 
            // buttonModifySerial
            // 
            this.buttonModifySerial.Location = new System.Drawing.Point(454, 54);
            this.buttonModifySerial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonModifySerial.Name = "buttonModifySerial";
            this.buttonModifySerial.Size = new System.Drawing.Size(150, 46);
            this.buttonModifySerial.TabIndex = 17;
            this.buttonModifySerial.Text = "修改序列号";
            this.buttonModifySerial.UseVisualStyleBackColor = true;
            this.buttonModifySerial.Click += new System.EventHandler(this.buttonModifySerial_Click);
            // 
            // textBoxSerial
            // 
            this.textBoxSerial.Location = new System.Drawing.Point(178, 58);
            this.textBoxSerial.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxSerial.Name = "textBoxSerial";
            this.textBoxSerial.Size = new System.Drawing.Size(260, 35);
            this.textBoxSerial.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(52, 118);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 24);
            this.label8.TabIndex = 7;
            this.label8.Text = "出图模式:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(52, 176);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 24);
            this.label6.TabIndex = 5;
            this.label6.Text = "图像处理:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 236);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 24);
            this.label4.TabIndex = 3;
            this.label4.Text = "曝光时间(ms):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备序列号:";
            // 
            // textBoxCurrent
            // 
            this.textBoxCurrent.Location = new System.Drawing.Point(130, 474);
            this.textBoxCurrent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxCurrent.Name = "textBoxCurrent";
            this.textBoxCurrent.Size = new System.Drawing.Size(118, 35);
            this.textBoxCurrent.TabIndex = 27;
            this.textBoxCurrent.Text = "0";
            this.textBoxCurrent.TextChanged += new System.EventHandler(this.textBoxCurrent_TextChanged);
            // 
            // textBoxKV
            // 
            this.textBoxKV.Location = new System.Drawing.Point(130, 422);
            this.textBoxKV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxKV.Name = "textBoxKV";
            this.textBoxKV.Size = new System.Drawing.Size(118, 35);
            this.textBoxKV.TabIndex = 26;
            this.textBoxKV.Text = "0";
            this.textBoxKV.TextChanged += new System.EventHandler(this.textBoxKV_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 490);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(118, 24);
            this.label13.TabIndex = 12;
            this.label13.Text = "电流(mA):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 438);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "电压(kV):";
            // 
            // buttonEnumDev
            // 
            this.buttonEnumDev.Location = new System.Drawing.Point(98, 58);
            this.buttonEnumDev.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonEnumDev.Name = "buttonEnumDev";
            this.buttonEnumDev.Size = new System.Drawing.Size(172, 46);
            this.buttonEnumDev.TabIndex = 4;
            this.buttonEnumDev.Text = "枚举设备";
            this.buttonEnumDev.UseVisualStyleBackColor = true;
            this.buttonEnumDev.Click += new System.EventHandler(this.buttonEnumDev_Click);
            // 
            // buttonAbandon
            // 
            this.buttonAbandon.Location = new System.Drawing.Point(98, 300);
            this.buttonAbandon.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAbandon.Name = "buttonAbandon";
            this.buttonAbandon.Size = new System.Drawing.Size(172, 46);
            this.buttonAbandon.TabIndex = 19;
            this.buttonAbandon.Text = "中断图像获取";
            this.buttonAbandon.UseVisualStyleBackColor = true;
            this.buttonAbandon.Click += new System.EventHandler(this.buttonAbandon_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.pictureBoxXrayOnOff);
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
            this.groupBox5.Location = new System.Drawing.Point(26, 642);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox5.Size = new System.Drawing.Size(638, 588);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "高压参数设置";
            // 
            // pictureBoxXrayOnOff
            // 
            this.pictureBoxXrayOnOff.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxXrayOnOff.InitialImage")));
            this.pictureBoxXrayOnOff.Location = new System.Drawing.Point(404, 380);
            this.pictureBoxXrayOnOff.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.pictureBoxXrayOnOff.Name = "pictureBoxXrayOnOff";
            this.pictureBoxXrayOnOff.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxXrayOnOff.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxXrayOnOff.TabIndex = 48;
            this.pictureBoxXrayOnOff.TabStop = false;
            // 
            // buttonXrayOnOff
            // 
            this.buttonXrayOnOff.Location = new System.Drawing.Point(126, 534);
            this.buttonXrayOnOff.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.buttonXrayOnOff.Name = "buttonXrayOnOff";
            this.buttonXrayOnOff.Size = new System.Drawing.Size(126, 46);
            this.buttonXrayOnOff.TabIndex = 47;
            this.buttonXrayOnOff.Text = "打开光源";
            this.buttonXrayOnOff.UseVisualStyleBackColor = true;
            this.buttonXrayOnOff.Click += new System.EventHandler(this.buttonXrayOnOff_Click);
            // 
            // labelHVPortLED
            // 
            this.labelHVPortLED.AutoSize = true;
            this.labelHVPortLED.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelHVPortLED.Location = new System.Drawing.Point(50, 358);
            this.labelHVPortLED.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelHVPortLED.Name = "labelHVPortLED";
            this.labelHVPortLED.Size = new System.Drawing.Size(26, 27);
            this.labelHVPortLED.TabIndex = 46;
            this.labelHVPortLED.Text = "O";
            // 
            // buttonConnectHVPort
            // 
            this.buttonConnectHVPort.Location = new System.Drawing.Point(128, 350);
            this.buttonConnectHVPort.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.buttonConnectHVPort.Name = "buttonConnectHVPort";
            this.buttonConnectHVPort.Size = new System.Drawing.Size(126, 46);
            this.buttonConnectHVPort.TabIndex = 2;
            this.buttonConnectHVPort.Text = "打开串口";
            this.buttonConnectHVPort.UseVisualStyleBackColor = true;
            this.buttonConnectHVPort.Click += new System.EventHandler(this.buttonConnectHVPort_Click);
            // 
            // comboBoxHVStopBit
            // 
            this.comboBoxHVStopBit.FormattingEnabled = true;
            this.comboBoxHVStopBit.Location = new System.Drawing.Point(130, 290);
            this.comboBoxHVStopBit.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comboBoxHVStopBit.Name = "comboBoxHVStopBit";
            this.comboBoxHVStopBit.Size = new System.Drawing.Size(120, 32);
            this.comboBoxHVStopBit.TabIndex = 1;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(24, 296);
            this.label23.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(94, 24);
            this.label23.TabIndex = 0;
            this.label23.Text = "停止位:";
            // 
            // comBoxHVCheckBit
            // 
            this.comBoxHVCheckBit.FormattingEnabled = true;
            this.comBoxHVCheckBit.Location = new System.Drawing.Point(130, 238);
            this.comBoxHVCheckBit.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comBoxHVCheckBit.Name = "comBoxHVCheckBit";
            this.comBoxHVCheckBit.Size = new System.Drawing.Size(120, 32);
            this.comBoxHVCheckBit.TabIndex = 1;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(24, 244);
            this.label22.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(94, 24);
            this.label22.TabIndex = 0;
            this.label22.Text = "校验位:";
            // 
            // comBoxHVDataBit
            // 
            this.comBoxHVDataBit.FormattingEnabled = true;
            this.comBoxHVDataBit.Location = new System.Drawing.Point(130, 178);
            this.comBoxHVDataBit.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comBoxHVDataBit.Name = "comBoxHVDataBit";
            this.comBoxHVDataBit.Size = new System.Drawing.Size(120, 32);
            this.comBoxHVDataBit.TabIndex = 1;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(24, 184);
            this.label21.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(94, 24);
            this.label21.TabIndex = 0;
            this.label21.Text = "数据位:";
            // 
            // comboBoxHVBaudRate
            // 
            this.comboBoxHVBaudRate.FormattingEnabled = true;
            this.comboBoxHVBaudRate.Location = new System.Drawing.Point(130, 122);
            this.comboBoxHVBaudRate.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comboBoxHVBaudRate.Name = "comboBoxHVBaudRate";
            this.comboBoxHVBaudRate.Size = new System.Drawing.Size(120, 32);
            this.comboBoxHVBaudRate.TabIndex = 1;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(24, 128);
            this.label20.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(94, 24);
            this.label20.TabIndex = 0;
            this.label20.Text = "波特率:";
            // 
            // comboBoxHVPort
            // 
            this.comboBoxHVPort.FormattingEnabled = true;
            this.comboBoxHVPort.Location = new System.Drawing.Point(130, 66);
            this.comboBoxHVPort.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.comboBoxHVPort.Name = "comboBoxHVPort";
            this.comboBoxHVPort.Size = new System.Drawing.Size(120, 32);
            this.comboBoxHVPort.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(24, 72);
            this.label17.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(94, 24);
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
            this.groupBox6.Location = new System.Drawing.Point(714, 780);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox6.Size = new System.Drawing.Size(368, 260);
            this.groupBox6.TabIndex = 26;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "PLC端口参数";
            // 
            // labelPLCStatus
            // 
            this.labelPLCStatus.AutoSize = true;
            this.labelPLCStatus.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPLCStatus.Location = new System.Drawing.Point(52, 184);
            this.labelPLCStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelPLCStatus.Name = "labelPLCStatus";
            this.labelPLCStatus.Size = new System.Drawing.Size(26, 27);
            this.labelPLCStatus.TabIndex = 49;
            this.labelPLCStatus.Text = "O";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(120, 112);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(196, 35);
            this.textBoxPort.TabIndex = 4;
            this.textBoxPort.Text = "502";
            // 
            // textBoxIpAddress
            // 
            this.textBoxIpAddress.Location = new System.Drawing.Point(120, 56);
            this.textBoxIpAddress.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.textBoxIpAddress.Name = "textBoxIpAddress";
            this.textBoxIpAddress.Size = new System.Drawing.Size(196, 35);
            this.textBoxIpAddress.TabIndex = 3;
            this.textBoxIpAddress.Text = "192.168.1.31";
            // 
            // buttonConnectPLC
            // 
            this.buttonConnectPLC.Location = new System.Drawing.Point(120, 176);
            this.buttonConnectPLC.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.buttonConnectPLC.Name = "buttonConnectPLC";
            this.buttonConnectPLC.Size = new System.Drawing.Size(200, 44);
            this.buttonConnectPLC.TabIndex = 2;
            this.buttonConnectPLC.Text = "连接";
            this.buttonConnectPLC.UseVisualStyleBackColor = true;
            this.buttonConnectPLC.Click += new System.EventHandler(this.buttonConnectPLC_Click);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(12, 64);
            this.label27.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(94, 24);
            this.label27.TabIndex = 0;
            this.label27.Text = "IP地址:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(36, 118);
            this.label28.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(70, 24);
            this.label28.TabIndex = 0;
            this.label28.Text = "端口:";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.buttonClearImage);
            this.groupBox7.Controls.Add(this.buttonConnectServer);
            this.groupBox7.Controls.Add(this.buttonEnumDev);
            this.groupBox7.Controls.Add(this.buttonAbandon);
            this.groupBox7.Controls.Add(this.buttonAsynchronous);
            this.groupBox7.Location = new System.Drawing.Point(714, 292);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox7.Size = new System.Drawing.Size(368, 476);
            this.groupBox7.TabIndex = 27;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "控制区域";
            // 
            // buttonClearImage
            // 
            this.buttonClearImage.Location = new System.Drawing.Point(100, 390);
            this.buttonClearImage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonClearImage.Name = "buttonClearImage";
            this.buttonClearImage.Size = new System.Drawing.Size(170, 42);
            this.buttonClearImage.TabIndex = 26;
            this.buttonClearImage.Text = "清空显示";
            this.buttonClearImage.UseVisualStyleBackColor = true;
            this.buttonClearImage.Click += new System.EventHandler(this.buttonClearImage_Click);
            // 
            // buttonConnectServer
            // 
            this.buttonConnectServer.Location = new System.Drawing.Point(100, 216);
            this.buttonConnectServer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonConnectServer.Name = "buttonConnectServer";
            this.buttonConnectServer.Size = new System.Drawing.Size(170, 42);
            this.buttonConnectServer.TabIndex = 25;
            this.buttonConnectServer.Text = "连接智能分析";
            this.buttonConnectServer.UseVisualStyleBackColor = true;
            this.buttonConnectServer.Click += new System.EventHandler(this.buttonConnectServer_Click);
            // 
            // buttonAsynchronous
            // 
            this.buttonAsynchronous.Location = new System.Drawing.Point(98, 134);
            this.buttonAsynchronous.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAsynchronous.Name = "buttonAsynchronous";
            this.buttonAsynchronous.Size = new System.Drawing.Size(172, 46);
            this.buttonAsynchronous.TabIndex = 20;
            this.buttonAsynchronous.Text = "异步获取图像";
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 1284);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 28, 0);
            this.statusStrip1.Size = new System.Drawing.Size(2564, 38);
            this.statusStrip1.TabIndex = 29;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel_PLCConn
            // 
            this.toolStripStatusLabel_PLCConn.Name = "toolStripStatusLabel_PLCConn";
            this.toolStripStatusLabel_PLCConn.Size = new System.Drawing.Size(139, 33);
            this.toolStripStatusLabel_PLCConn.Text = "PLC-未连接";
            // 
            // toolStripStatusLabel_HVConn
            // 
            this.toolStripStatusLabel_HVConn.Name = "toolStripStatusLabel_HVConn";
            this.toolStripStatusLabel_HVConn.Size = new System.Drawing.Size(144, 33);
            this.toolStripStatusLabel_HVConn.Text = "高压-未连接";
            // 
            // toolStripStatusLabel_XrayOnOff
            // 
            this.toolStripStatusLabel_XrayOnOff.Name = "toolStripStatusLabel_XrayOnOff";
            this.toolStripStatusLabel_XrayOnOff.Size = new System.Drawing.Size(120, 33);
            this.toolStripStatusLabel_XrayOnOff.Text = "Xray-OFF";
            // 
            // toolStripStatusLabel_HV_KV
            // 
            this.toolStripStatusLabel_HV_KV.Name = "toolStripStatusLabel_HV_KV";
            this.toolStripStatusLabel_HV_KV.Size = new System.Drawing.Size(57, 33);
            this.toolStripStatusLabel_HV_KV.Text = "0kV";
            // 
            // toolStripStatusLabel_HV_Cur
            // 
            this.toolStripStatusLabel_HV_Cur.Name = "toolStripStatusLabel_HV_Cur";
            this.toolStripStatusLabel_HV_Cur.Size = new System.Drawing.Size(60, 33);
            this.toolStripStatusLabel_HV_Cur.Text = "0uA";
            // 
            // toolStripStatusLabel_HV_Temp
            // 
            this.toolStripStatusLabel_HV_Temp.Name = "toolStripStatusLabel_HV_Temp";
            this.toolStripStatusLabel_HV_Temp.Size = new System.Drawing.Size(44, 33);
            this.toolStripStatusLabel_HV_Temp.Text = "0C";
            // 
            // toolStripStatusLabel_HVError
            // 
            this.toolStripStatusLabel_HVError.Name = "toolStripStatusLabel_HVError";
            this.toolStripStatusLabel_HVError.Size = new System.Drawing.Size(34, 33);
            this.toolStripStatusLabel_HVError.Text = "--";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(400, 32);
            // 
            // toolStripStatusLabel_Time
            // 
            this.toolStripStatusLabel_Time.Name = "toolStripStatusLabel_Time";
            this.toolStripStatusLabel_Time.Size = new System.Drawing.Size(241, 33);
            this.toolStripStatusLabel_Time.Text = "2022.11.11 12:25:59";
            // 
            // toolStripStatusLabel_ProgressInfo
            // 
            this.toolStripStatusLabel_ProgressInfo.Name = "toolStripStatusLabel_ProgressInfo";
            this.toolStripStatusLabel_ProgressInfo.Size = new System.Drawing.Size(34, 33);
            this.toolStripStatusLabel_ProgressInfo.Text = "--";
            // 
            // timerDateTime
            // 
            this.timerDateTime.Enabled = true;
            this.timerDateTime.Interval = 1000;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
            // 
            // radioButtonAutoMode
            // 
            this.radioButtonAutoMode.AutoSize = true;
            this.radioButtonAutoMode.Location = new System.Drawing.Point(98, 58);
            this.radioButtonAutoMode.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.radioButtonAutoMode.Name = "radioButtonAutoMode";
            this.radioButtonAutoMode.Size = new System.Drawing.Size(137, 28);
            this.radioButtonAutoMode.TabIndex = 27;
            this.radioButtonAutoMode.TabStop = true;
            this.radioButtonAutoMode.Text = "自动模式";
            this.radioButtonAutoMode.UseVisualStyleBackColor = true;
            this.radioButtonAutoMode.CheckedChanged += new System.EventHandler(this.radioButtonLock_CheckedChanged);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.radioButtonManualMode);
            this.groupBox8.Controls.Add(this.radioButtonAutoMode);
            this.groupBox8.Location = new System.Drawing.Point(716, 1052);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.groupBox8.Size = new System.Drawing.Size(366, 178);
            this.groupBox8.TabIndex = 30;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "模式选择";
            // 
            // radioButtonManualMode
            // 
            this.radioButtonManualMode.AutoSize = true;
            this.radioButtonManualMode.Location = new System.Drawing.Point(98, 124);
            this.radioButtonManualMode.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.radioButtonManualMode.Name = "radioButtonManualMode";
            this.radioButtonManualMode.Size = new System.Drawing.Size(137, 28);
            this.radioButtonManualMode.TabIndex = 27;
            this.radioButtonManualMode.TabStop = true;
            this.radioButtonManualMode.Text = "手动模式";
            this.radioButtonManualMode.UseVisualStyleBackColor = true;
            this.radioButtonManualMode.CheckedChanged += new System.EventHandler(this.radioButtonLock_CheckedChanged);
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2564, 1322);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(2966, 1905);
            this.MinimumSize = new System.Drawing.Size(2538, 1305);
            this.Name = "Demo";
            this.Text = "睿奥自动化检测控制软件(v20220820)";
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxXrayOnOff)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox textBoxActTime;
        private System.Windows.Forms.TextBox textBoxCurrent;
        private System.Windows.Forms.TextBox textBoxKV;
        private System.Windows.Forms.Button buttonSetParameter;
        private System.Windows.Forms.Button buttonModifySerial;
        private System.Windows.Forms.TextBox textBoxSerial;
        private System.Windows.Forms.Button buttonAbandon;
        private System.Windows.Forms.PictureBox pictureBoxImage;
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
        private System.Windows.Forms.Button buttonXrayOnOff;
        private System.Windows.Forms.Button buttonAsynchronous;
        private System.Windows.Forms.Button buttonClearImage;
        private System.Windows.Forms.PictureBox pictureBoxImage1;
        private System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelPLCStatus;
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
        private System.Windows.Forms.RadioButton radioButtonAutoMode;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.RadioButton radioButtonManualMode;
        private System.Windows.Forms.PictureBox pictureBoxXrayOnOff;
        private System.Windows.Forms.Button buttonConnectServer;
    }
}

