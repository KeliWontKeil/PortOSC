
using _NumberTextBox;

namespace PortOSC
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            PortNameBox = new ComboBox();
            BaudRateBox = new ComboBox();
            DataBitsBox = new ComboBox();
            StopBitsBox = new ComboBox();
            ParityBox = new ComboBox();
            RecText = new TextBox();
            StrSendText = new TextBox();
            HexSendText = new TextBox();
            OpenSerialPort = new Button();
            CloseSerialPort = new Button();
            EnableOsc = new CheckBox();
            HexSendButton = new Button();
            StrSendButton = new Button();
            ClearRec = new Button();
            HeadTextBox = new TextBox();
            EndTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            formPlot1 = new ScottPlot.FormsPlot();
            SavePlot = new Button();
            ClearData = new Button();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            FileTypeText = new ComboBox();
            FileNameText = new TextBox();
            ChannelLength = new NumericUpDown();
            label9 = new Label();
            label14 = new Label();
            flowLayoutPanel3 = new FlowLayoutPanel();
            StateMassageTextBox = new RichTextBox();
            StopRec = new CheckBox();
            RecNewLine = new CheckBox();
            ShowHex = new RadioButton();
            ShowStr = new RadioButton();
            panel1 = new Panel();
            SerialPortPanel = new Panel();
            ServerTCP_Panel = new Panel();
            label19 = new Label();
            ServerPortBox = new NumberTextBox();
            ServerIPBox3 = new NumberTextBox();
            ServerIPBox4 = new NumberTextBox();
            ServerIPBox2 = new NumberTextBox();
            ServerIPBox1 = new NumberTextBox();
            ServerStateLight = new TextBox();
            ServerCloseButton = new Button();
            ServerStartButton = new Button();
            panel2 = new Panel();
            label21 = new Label();
            StopReceiveCheckBox = new CheckBox();
            StopSendCheckBox = new CheckBox();
            TransmitServerStateLight = new TextBox();
            TransmitServerPortBox = new NumberTextBox();
            TransmitServerCloseButton = new Button();
            TransmitServerStartButton = new Button();
            TransmitServerIPBox3 = new NumberTextBox();
            TransmitServerIPBox4 = new NumberTextBox();
            TransmitServerIPBox2 = new NumberTextBox();
            TransmitServerIPBox1 = new NumberTextBox();
            ClientTCP_Panel = new Panel();
            label20 = new Label();
            ClientPortBox = new NumberTextBox();
            ClientIPBox3 = new NumberTextBox();
            ClientIPBox4 = new NumberTextBox();
            ClientIPBox2 = new NumberTextBox();
            ClientIPBox1 = new NumberTextBox();
            ClientDisconnectButton = new Button();
            ClientConnectButton = new Button();
            UDP_Panel = new Panel();
            label17 = new Label();
            label16 = new Label();
            label15 = new Label();
            UDPPortBox = new NumberTextBox();
            UdpTargetIPBox3 = new NumberTextBox();
            UdpTargetIPBox4 = new NumberTextBox();
            UdpTargetIPBox2 = new NumberTextBox();
            UdpTargetIPBox1 = new NumberTextBox();
            UdpLocalIPBox3 = new NumberTextBox();
            UdpLocalIPBox4 = new NumberTextBox();
            UdpLocalIPBox2 = new NumberTextBox();
            UdpLocalIPBox1 = new NumberTextBox();
            UDPUnbindButton = new Button();
            UDPBindButton = new Button();
            menuStrip1 = new MenuStrip();
            ChooseDataInput = new ToolStripMenuItem();
            SerialPortToolStripMenuItem = new ToolStripMenuItem();
            TCPserverToolStripMenuItem = new ToolStripMenuItem();
            TCPClientToolStripMenuItem = new ToolStripMenuItem();
            UDPToolStripMenuItem = new ToolStripMenuItem();
            ToolsToolStripMenuItem = new ToolStripMenuItem();
            OpenHexToCharTool = new ToolStripMenuItem();
            ReadConfig = new ToolStripMenuItem();
            SaveConfigToAnother = new ToolStripMenuItem();
            OpenHelp = new ToolStripMenuItem();
            label8 = new Label();
            ReceiveModeSelectText = new Label();
            label18 = new Label();
            AdditionalBufferLengthBox = new NumericUpDown();
            SaveFileDialog = new SaveFileDialog();
            OpenFileDialog = new OpenFileDialog();
            XStep = new NumberTextBox();
            YStep = new NumberTextBox();
            RightLimit = new NumberTextBox();
            LeftLimit = new NumberTextBox();
            ButtomLimit = new NumberTextBox();
            TopLimit = new NumberTextBox();
            ((System.ComponentModel.ISupportInitialize)ChannelLength).BeginInit();
            flowLayoutPanel3.SuspendLayout();
            panel1.SuspendLayout();
            SerialPortPanel.SuspendLayout();
            ServerTCP_Panel.SuspendLayout();
            panel2.SuspendLayout();
            ClientTCP_Panel.SuspendLayout();
            UDP_Panel.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AdditionalBufferLengthBox).BeginInit();
            SuspendLayout();
            // 
            // PortNameBox
            // 
            PortNameBox.DropDownStyle = ComboBoxStyle.DropDownList;
            PortNameBox.FormattingEnabled = true;
            PortNameBox.Location = new Point(56, 63);
            PortNameBox.Margin = new Padding(3, 4, 3, 4);
            PortNameBox.Name = "PortNameBox";
            PortNameBox.Size = new Size(84, 25);
            PortNameBox.TabIndex = 0;
            // 
            // BaudRateBox
            // 
            BaudRateBox.DropDownStyle = ComboBoxStyle.DropDownList;
            BaudRateBox.FormattingEnabled = true;
            BaudRateBox.Items.AddRange(new object[] { "1200", "2400", "4800", "9600", "14400", "19200", "38400", "43000", "57600", "78600", "115200", "128000", "230400", "256000", "460800", "921600", "1382400" });
            BaudRateBox.Location = new Point(56, 95);
            BaudRateBox.Margin = new Padding(3, 4, 3, 4);
            BaudRateBox.Name = "BaudRateBox";
            BaudRateBox.Size = new Size(84, 25);
            BaudRateBox.TabIndex = 1;
            // 
            // DataBitsBox
            // 
            DataBitsBox.DropDownStyle = ComboBoxStyle.DropDownList;
            DataBitsBox.FormattingEnabled = true;
            DataBitsBox.Items.AddRange(new object[] { "5", "6", "7", "8" });
            DataBitsBox.Location = new Point(56, 128);
            DataBitsBox.Margin = new Padding(3, 4, 3, 4);
            DataBitsBox.Name = "DataBitsBox";
            DataBitsBox.Size = new Size(84, 25);
            DataBitsBox.TabIndex = 2;
            // 
            // StopBitsBox
            // 
            StopBitsBox.DropDownStyle = ComboBoxStyle.DropDownList;
            StopBitsBox.FormattingEnabled = true;
            StopBitsBox.Items.AddRange(new object[] { "1", "1.5", "2" });
            StopBitsBox.Location = new Point(56, 160);
            StopBitsBox.Margin = new Padding(3, 4, 3, 4);
            StopBitsBox.Name = "StopBitsBox";
            StopBitsBox.Size = new Size(84, 25);
            StopBitsBox.TabIndex = 3;
            // 
            // ParityBox
            // 
            ParityBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ParityBox.FormattingEnabled = true;
            ParityBox.Items.AddRange(new object[] { "无", "奇校验", "偶校验" });
            ParityBox.Location = new Point(56, 192);
            ParityBox.Margin = new Padding(3, 4, 3, 4);
            ParityBox.Name = "ParityBox";
            ParityBox.Size = new Size(84, 25);
            ParityBox.TabIndex = 4;
            // 
            // RecText
            // 
            RecText.BackColor = SystemColors.ButtonHighlight;
            RecText.BorderStyle = BorderStyle.FixedSingle;
            RecText.Location = new Point(230, 31);
            RecText.Margin = new Padding(3, 4, 3, 4);
            RecText.Multiline = true;
            RecText.Name = "RecText";
            RecText.ReadOnly = true;
            RecText.Size = new Size(435, 421);
            RecText.TabIndex = 5;
            // 
            // StrSendText
            // 
            StrSendText.Location = new Point(306, 556);
            StrSendText.Margin = new Padding(3, 4, 3, 4);
            StrSendText.Multiline = true;
            StrSendText.Name = "StrSendText";
            StrSendText.Size = new Size(359, 83);
            StrSendText.TabIndex = 6;
            // 
            // HexSendText
            // 
            HexSendText.Location = new Point(306, 459);
            HexSendText.Margin = new Padding(3, 4, 3, 4);
            HexSendText.Multiline = true;
            HexSendText.Name = "HexSendText";
            HexSendText.Size = new Size(359, 91);
            HexSendText.TabIndex = 7;
            // 
            // OpenSerialPort
            // 
            OpenSerialPort.ImeMode = ImeMode.NoControl;
            OpenSerialPort.Location = new Point(136, 15);
            OpenSerialPort.Margin = new Padding(3, 4, 3, 4);
            OpenSerialPort.Name = "OpenSerialPort";
            OpenSerialPort.Size = new Size(72, 63);
            OpenSerialPort.TabIndex = 8;
            OpenSerialPort.Text = "打开串口";
            OpenSerialPort.UseVisualStyleBackColor = true;
            OpenSerialPort.Click += OpenSerialPort_Click;
            // 
            // CloseSerialPort
            // 
            CloseSerialPort.Enabled = false;
            CloseSerialPort.ImeMode = ImeMode.NoControl;
            CloseSerialPort.Location = new Point(136, 90);
            CloseSerialPort.Margin = new Padding(3, 4, 3, 4);
            CloseSerialPort.Name = "CloseSerialPort";
            CloseSerialPort.Size = new Size(72, 63);
            CloseSerialPort.TabIndex = 9;
            CloseSerialPort.Text = "关闭串口";
            CloseSerialPort.UseVisualStyleBackColor = true;
            CloseSerialPort.Click += CloseSerialPort_Click;
            // 
            // EnableOsc
            // 
            EnableOsc.AutoSize = true;
            EnableOsc.ImeMode = ImeMode.NoControl;
            EnableOsc.Location = new Point(3, 4);
            EnableOsc.Margin = new Padding(3, 4, 3, 4);
            EnableOsc.Name = "EnableOsc";
            EnableOsc.Size = new Size(87, 21);
            EnableOsc.TabIndex = 11;
            EnableOsc.Text = "启用示波器";
            EnableOsc.UseVisualStyleBackColor = true;
            EnableOsc.CheckedChanged += EnableOsc_CheckedChanged;
            // 
            // HexSendButton
            // 
            HexSendButton.ImeMode = ImeMode.NoControl;
            HexSendButton.Location = new Point(227, 522);
            HexSendButton.Margin = new Padding(3, 4, 3, 4);
            HexSendButton.Name = "HexSendButton";
            HexSendButton.Size = new Size(76, 27);
            HexSendButton.TabIndex = 15;
            HexSendButton.Text = "HEX发送";
            HexSendButton.UseVisualStyleBackColor = true;
            HexSendButton.Click += HexSendButton_Click;
            // 
            // StrSendButton
            // 
            StrSendButton.Font = new Font("微软雅黑", 9F, FontStyle.Regular, GraphicsUnit.Point, 134);
            StrSendButton.ImeMode = ImeMode.NoControl;
            StrSendButton.Location = new Point(227, 612);
            StrSendButton.Margin = new Padding(3, 4, 3, 4);
            StrSendButton.Name = "StrSendButton";
            StrSendButton.Size = new Size(76, 27);
            StrSendButton.TabIndex = 16;
            StrSendButton.Text = "字符串发送";
            StrSendButton.UseVisualStyleBackColor = true;
            StrSendButton.Click += StrSendButton_Click;
            // 
            // ClearRec
            // 
            ClearRec.ImeMode = ImeMode.NoControl;
            ClearRec.Location = new Point(128, 258);
            ClearRec.Margin = new Padding(3, 4, 3, 4);
            ClearRec.Name = "ClearRec";
            ClearRec.Size = new Size(84, 48);
            ClearRec.TabIndex = 17;
            ClearRec.Text = "清接收区";
            ClearRec.UseVisualStyleBackColor = true;
            ClearRec.Click += ClearRec_Click;
            // 
            // HeadTextBox
            // 
            HeadTextBox.Location = new Point(752, 554);
            HeadTextBox.Margin = new Padding(3, 4, 3, 4);
            HeadTextBox.Name = "HeadTextBox";
            HeadTextBox.Size = new Size(251, 23);
            HeadTextBox.TabIndex = 18;
            // 
            // EndTextBox
            // 
            EndTextBox.Location = new Point(752, 585);
            EndTextBox.Margin = new Padding(3, 4, 3, 4);
            EndTextBox.Name = "EndTextBox";
            EndTextBox.Size = new Size(251, 23);
            EndTextBox.TabIndex = 19;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(12, 66);
            label1.Name = "label1";
            label1.Size = new Size(44, 17);
            label1.TabIndex = 20;
            label1.Text = "端口号";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ImeMode = ImeMode.NoControl;
            label2.Location = new Point(12, 99);
            label2.Name = "label2";
            label2.Size = new Size(44, 17);
            label2.TabIndex = 21;
            label2.Text = "波特率";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ImeMode = ImeMode.NoControl;
            label3.Location = new Point(12, 131);
            label3.Name = "label3";
            label3.Size = new Size(44, 17);
            label3.TabIndex = 22;
            label3.Text = "数据位";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ImeMode = ImeMode.NoControl;
            label4.Location = new Point(12, 163);
            label4.Name = "label4";
            label4.Size = new Size(44, 17);
            label4.TabIndex = 23;
            label4.Text = "停止位";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ImeMode = ImeMode.NoControl;
            label5.Location = new Point(12, 196);
            label5.Name = "label5";
            label5.Size = new Size(44, 17);
            label5.TabIndex = 24;
            label5.Text = "校验位";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ImeMode = ImeMode.NoControl;
            label6.Location = new Point(684, 557);
            label6.Name = "label6";
            label6.Size = new Size(64, 17);
            label6.TabIndex = 25;
            label6.Text = "帧头(HEX)";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ImeMode = ImeMode.NoControl;
            label7.Location = new Point(684, 588);
            label7.Name = "label7";
            label7.Size = new Size(64, 17);
            label7.TabIndex = 26;
            label7.Text = "帧尾(HEX)";
            // 
            // formPlot1
            // 
            formPlot1.Location = new Point(661, 14);
            formPlot1.Margin = new Padding(4, 3, 4, 3);
            formPlot1.Name = "formPlot1";
            formPlot1.Size = new Size(701, 506);
            formPlot1.TabIndex = 40;
            formPlot1.AxesChanged += FormsPlot1_AxesChanged;
            formPlot1.Load += FormsPlot1_Load;
            // 
            // SavePlot
            // 
            SavePlot.Location = new Point(1259, 612);
            SavePlot.Margin = new Padding(2, 3, 2, 3);
            SavePlot.Name = "SavePlot";
            SavePlot.Size = new Size(87, 28);
            SavePlot.TabIndex = 45;
            SavePlot.Text = "保存数据";
            SavePlot.UseVisualStyleBackColor = true;
            SavePlot.Click += SavePlot_Click;
            // 
            // ClearData
            // 
            ClearData.Location = new Point(827, 512);
            ClearData.Margin = new Padding(2, 3, 2, 3);
            ClearData.Name = "ClearData";
            ClearData.Size = new Size(153, 35);
            ClearData.TabIndex = 46;
            ClearData.Text = "复位显示";
            ClearData.UseVisualStyleBackColor = true;
            ClearData.Click += ClearData_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(1007, 558);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(76, 17);
            label10.TabIndex = 58;
            label10.Text = "X轴显示范围";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(1009, 588);
            label11.Margin = new Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new Size(75, 17);
            label11.TabIndex = 59;
            label11.Text = "Y轴显示范围";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(1213, 587);
            label12.Margin = new Padding(2, 0, 2, 0);
            label12.Name = "label12";
            label12.Size = new Size(75, 17);
            label12.TabIndex = 60;
            label12.Text = "Y轴刻度间隔";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(1212, 557);
            label13.Margin = new Padding(2, 0, 2, 0);
            label13.Name = "label13";
            label13.Size = new Size(76, 17);
            label13.TabIndex = 61;
            label13.Text = "X轴刻度间隔";
            // 
            // FileTypeText
            // 
            FileTypeText.DropDownStyle = ComboBoxStyle.DropDownList;
            FileTypeText.FormattingEnabled = true;
            FileTypeText.Items.AddRange(new object[] { ".png", ".jpg", ".bmp", ".tiff", ".csv" });
            FileTypeText.Location = new Point(1167, 614);
            FileTypeText.Margin = new Padding(2, 3, 2, 3);
            FileTypeText.Name = "FileTypeText";
            FileTypeText.Size = new Size(88, 25);
            FileTypeText.TabIndex = 64;
            // 
            // FileNameText
            // 
            FileNameText.Location = new Point(752, 615);
            FileNameText.Margin = new Padding(2, 3, 2, 3);
            FileNameText.Name = "FileNameText";
            FileNameText.Size = new Size(412, 23);
            FileNameText.TabIndex = 65;
            FileNameText.Text = "SaveData1";
            // 
            // ChannelLength
            // 
            ChannelLength.BackColor = SystemColors.ButtonHighlight;
            ChannelLength.Location = new Point(1237, 517);
            ChannelLength.Margin = new Padding(3, 4, 3, 4);
            ChannelLength.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            ChannelLength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ChannelLength.Name = "ChannelLength";
            ChannelLength.ReadOnly = true;
            ChannelLength.Size = new Size(66, 23);
            ChannelLength.TabIndex = 41;
            ChannelLength.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ImeMode = ImeMode.NoControl;
            label9.Location = new Point(1189, 519);
            label9.Name = "label9";
            label9.Size = new Size(44, 17);
            label9.TabIndex = 42;
            label9.Text = "通道数";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(682, 618);
            label14.Margin = new Padding(2, 0, 2, 0);
            label14.Name = "label14";
            label14.Size = new Size(68, 17);
            label14.TabIndex = 66;
            label14.Text = "保存文件名";
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.BorderStyle = BorderStyle.Fixed3D;
            flowLayoutPanel3.Controls.Add(EnableOsc);
            flowLayoutPanel3.Location = new Point(701, 512);
            flowLayoutPanel3.Margin = new Padding(3, 4, 3, 4);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(96, 34);
            flowLayoutPanel3.TabIndex = 67;
            // 
            // StateMassageTextBox
            // 
            StateMassageTextBox.BackColor = SystemColors.ButtonHighlight;
            StateMassageTextBox.Location = new Point(6, 459);
            StateMassageTextBox.Margin = new Padding(2, 3, 2, 3);
            StateMassageTextBox.Name = "StateMassageTextBox";
            StateMassageTextBox.ReadOnly = true;
            StateMassageTextBox.Size = new Size(217, 180);
            StateMassageTextBox.TabIndex = 68;
            StateMassageTextBox.Text = "";
            // 
            // StopRec
            // 
            StopRec.AutoSize = true;
            StopRec.ImeMode = ImeMode.NoControl;
            StopRec.Location = new Point(136, 237);
            StopRec.Margin = new Padding(3, 4, 3, 4);
            StopRec.Name = "StopRec";
            StopRec.Size = new Size(75, 21);
            StopRec.TabIndex = 10;
            StopRec.Text = "暂停显示";
            StopRec.UseVisualStyleBackColor = true;
            // 
            // RecNewLine
            // 
            RecNewLine.AutoSize = true;
            RecNewLine.ImeMode = ImeMode.NoControl;
            RecNewLine.Location = new Point(3, 44);
            RecNewLine.Margin = new Padding(3, 4, 3, 4);
            RecNewLine.Name = "RecNewLine";
            RecNewLine.Size = new Size(75, 21);
            RecNewLine.TabIndex = 14;
            RecNewLine.Text = "接收换行";
            RecNewLine.UseVisualStyleBackColor = true;
            // 
            // ShowHex
            // 
            ShowHex.AutoSize = true;
            ShowHex.ImeMode = ImeMode.NoControl;
            ShowHex.Location = new Point(3, 23);
            ShowHex.Margin = new Padding(3, 4, 3, 4);
            ShowHex.Name = "ShowHex";
            ShowHex.Size = new Size(74, 21);
            ShowHex.TabIndex = 12;
            ShowHex.Text = "HEX显示";
            ShowHex.UseVisualStyleBackColor = true;
            // 
            // ShowStr
            // 
            ShowStr.AutoSize = true;
            ShowStr.Checked = true;
            ShowStr.ImeMode = ImeMode.NoControl;
            ShowStr.Location = new Point(3, 3);
            ShowStr.Margin = new Padding(3, 4, 3, 4);
            ShowStr.Name = "ShowStr";
            ShowStr.Size = new Size(86, 21);
            ShowStr.TabIndex = 13;
            ShowStr.TabStop = true;
            ShowStr.Text = "字符串显示";
            ShowStr.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(RecNewLine);
            panel1.Controls.Add(ShowStr);
            panel1.Controls.Add(ShowHex);
            panel1.Location = new Point(20, 236);
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(104, 69);
            panel1.TabIndex = 0;
            // 
            // SerialPortPanel
            // 
            SerialPortPanel.BorderStyle = BorderStyle.Fixed3D;
            SerialPortPanel.Controls.Add(CloseSerialPort);
            SerialPortPanel.Controls.Add(OpenSerialPort);
            SerialPortPanel.Location = new Point(7, 54);
            SerialPortPanel.Margin = new Padding(2, 3, 2, 3);
            SerialPortPanel.Name = "SerialPortPanel";
            SerialPortPanel.Size = new Size(216, 171);
            SerialPortPanel.TabIndex = 72;
            // 
            // ServerTCP_Panel
            // 
            ServerTCP_Panel.BorderStyle = BorderStyle.Fixed3D;
            ServerTCP_Panel.Controls.Add(label19);
            ServerTCP_Panel.Controls.Add(ServerPortBox);
            ServerTCP_Panel.Controls.Add(ServerIPBox3);
            ServerTCP_Panel.Controls.Add(ServerIPBox4);
            ServerTCP_Panel.Controls.Add(ServerIPBox2);
            ServerTCP_Panel.Controls.Add(ServerIPBox1);
            ServerTCP_Panel.Controls.Add(ServerStateLight);
            ServerTCP_Panel.Controls.Add(ServerCloseButton);
            ServerTCP_Panel.Controls.Add(ServerStartButton);
            ServerTCP_Panel.Location = new Point(235, 45);
            ServerTCP_Panel.Margin = new Padding(2, 3, 2, 3);
            ServerTCP_Panel.Name = "ServerTCP_Panel";
            ServerTCP_Panel.Size = new Size(216, 171);
            ServerTCP_Panel.TabIndex = 73;
            ServerTCP_Panel.Visible = false;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(11, 6);
            label19.Margin = new Padding(2, 0, 2, 0);
            label19.Name = "label19";
            label19.Size = new Size(104, 17);
            label19.TabIndex = 88;
            label19.Text = "本地服务器地址：";
            // 
            // ServerPortBox
            // 
            ServerPortBox.DecimalPointNumber = 0;
            ServerPortBox.DoubleValue = 0D;
            ServerPortBox.FloatValue = 0F;
            ServerPortBox.IntValue = 60721;
            ServerPortBox.Location = new Point(13, 62);
            ServerPortBox.LongValue = 0L;
            ServerPortBox.Margin = new Padding(2, 3, 2, 3);
            ServerPortBox.MaxValue = "65535";
            ServerPortBox.MinValue = "0";
            ServerPortBox.Name = "ServerPortBox";
            ServerPortBox.NumberType = NumberType.Integer;
            ServerPortBox.Size = new Size(94, 23);
            ServerPortBox.TabIndex = 81;
            ServerPortBox.Text = "60721";
            // 
            // ServerIPBox3
            // 
            ServerIPBox3.DecimalPointNumber = 0;
            ServerIPBox3.DoubleValue = 0D;
            ServerIPBox3.FloatValue = 0F;
            ServerIPBox3.Location = new Point(111, 27);
            ServerIPBox3.LongValue = 0L;
            ServerIPBox3.Margin = new Padding(2, 3, 2, 3);
            ServerIPBox3.MaxValue = "255";
            ServerIPBox3.MinValue = "0";
            ServerIPBox3.Name = "ServerIPBox3";
            ServerIPBox3.NumberType = NumberType.Integer;
            ServerIPBox3.Size = new Size(43, 23);
            ServerIPBox3.TabIndex = 80;
            ServerIPBox3.Text = "0";
            // 
            // ServerIPBox4
            // 
            ServerIPBox4.DecimalPointNumber = 0;
            ServerIPBox4.DoubleValue = 0D;
            ServerIPBox4.FloatValue = 0F;
            ServerIPBox4.IntValue = 1;
            ServerIPBox4.Location = new Point(160, 27);
            ServerIPBox4.LongValue = 0L;
            ServerIPBox4.Margin = new Padding(2, 3, 2, 3);
            ServerIPBox4.MaxValue = "255";
            ServerIPBox4.MinValue = "0";
            ServerIPBox4.Name = "ServerIPBox4";
            ServerIPBox4.NumberType = NumberType.Integer;
            ServerIPBox4.Size = new Size(43, 23);
            ServerIPBox4.TabIndex = 79;
            ServerIPBox4.Text = "1";
            // 
            // ServerIPBox2
            // 
            ServerIPBox2.DecimalPointNumber = 0;
            ServerIPBox2.DoubleValue = 0D;
            ServerIPBox2.FloatValue = 0F;
            ServerIPBox2.Location = new Point(62, 27);
            ServerIPBox2.LongValue = 0L;
            ServerIPBox2.Margin = new Padding(2, 3, 2, 3);
            ServerIPBox2.MaxValue = "255";
            ServerIPBox2.MinValue = "0";
            ServerIPBox2.Name = "ServerIPBox2";
            ServerIPBox2.NumberType = NumberType.Integer;
            ServerIPBox2.Size = new Size(43, 23);
            ServerIPBox2.TabIndex = 78;
            ServerIPBox2.Text = "0";
            // 
            // ServerIPBox1
            // 
            ServerIPBox1.DecimalPointNumber = 0;
            ServerIPBox1.DoubleValue = 0D;
            ServerIPBox1.FloatValue = 0F;
            ServerIPBox1.IntValue = 127;
            ServerIPBox1.Location = new Point(13, 27);
            ServerIPBox1.LongValue = 0L;
            ServerIPBox1.Margin = new Padding(2, 3, 2, 3);
            ServerIPBox1.MaxValue = "255";
            ServerIPBox1.MinValue = "0";
            ServerIPBox1.Name = "ServerIPBox1";
            ServerIPBox1.NumberType = NumberType.Integer;
            ServerIPBox1.Size = new Size(43, 23);
            ServerIPBox1.TabIndex = 77;
            ServerIPBox1.Text = "127";
            // 
            // ServerStateLight
            // 
            ServerStateLight.BackColor = Color.Red;
            ServerStateLight.BorderStyle = BorderStyle.FixedSingle;
            ServerStateLight.Enabled = false;
            ServerStateLight.Location = new Point(149, 64);
            ServerStateLight.Margin = new Padding(2, 3, 2, 3);
            ServerStateLight.Name = "ServerStateLight";
            ServerStateLight.ReadOnly = true;
            ServerStateLight.Size = new Size(24, 23);
            ServerStateLight.TabIndex = 76;
            // 
            // ServerCloseButton
            // 
            ServerCloseButton.Enabled = false;
            ServerCloseButton.Location = new Point(110, 101);
            ServerCloseButton.Margin = new Padding(2, 3, 2, 3);
            ServerCloseButton.Name = "ServerCloseButton";
            ServerCloseButton.Size = new Size(93, 60);
            ServerCloseButton.TabIndex = 11;
            ServerCloseButton.Text = "关闭服务器";
            ServerCloseButton.UseVisualStyleBackColor = true;
            ServerCloseButton.Click += ServerCloseButton_Click;
            // 
            // ServerStartButton
            // 
            ServerStartButton.Location = new Point(13, 101);
            ServerStartButton.Margin = new Padding(2, 3, 2, 3);
            ServerStartButton.Name = "ServerStartButton";
            ServerStartButton.Size = new Size(93, 60);
            ServerStartButton.TabIndex = 10;
            ServerStartButton.Text = "启动服务器";
            ServerStartButton.UseVisualStyleBackColor = true;
            ServerStartButton.Click += ServerStartButton_Click;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(label21);
            panel2.Controls.Add(StopReceiveCheckBox);
            panel2.Controls.Add(StopSendCheckBox);
            panel2.Controls.Add(TransmitServerStateLight);
            panel2.Controls.Add(TransmitServerPortBox);
            panel2.Controls.Add(TransmitServerCloseButton);
            panel2.Controls.Add(TransmitServerStartButton);
            panel2.Controls.Add(TransmitServerIPBox3);
            panel2.Controls.Add(TransmitServerIPBox4);
            panel2.Controls.Add(TransmitServerIPBox2);
            panel2.Controls.Add(TransmitServerIPBox1);
            panel2.Location = new Point(7, 315);
            panel2.Margin = new Padding(2, 3, 2, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(216, 136);
            panel2.TabIndex = 74;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(9, 2);
            label21.Margin = new Padding(2, 0, 2, 0);
            label21.Name = "label21";
            label21.Size = new Size(104, 17);
            label21.TabIndex = 86;
            label21.Text = "转发服务器选项：";
            // 
            // StopReceiveCheckBox
            // 
            StopReceiveCheckBox.AutoSize = true;
            StopReceiveCheckBox.Location = new Point(97, 20);
            StopReceiveCheckBox.Margin = new Padding(2, 3, 2, 3);
            StopReceiveCheckBox.Name = "StopReceiveCheckBox";
            StopReceiveCheckBox.Size = new Size(99, 21);
            StopReceiveCheckBox.TabIndex = 85;
            StopReceiveCheckBox.Text = "暂停接收转发";
            StopReceiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // StopSendCheckBox
            // 
            StopSendCheckBox.AutoSize = true;
            StopSendCheckBox.Location = new Point(14, 20);
            StopSendCheckBox.Margin = new Padding(2, 3, 2, 3);
            StopSendCheckBox.Name = "StopSendCheckBox";
            StopSendCheckBox.Size = new Size(75, 21);
            StopSendCheckBox.TabIndex = 84;
            StopSendCheckBox.Text = "暂停发送";
            StopSendCheckBox.UseVisualStyleBackColor = true;
            // 
            // TransmitServerStateLight
            // 
            TransmitServerStateLight.BackColor = Color.Red;
            TransmitServerStateLight.BorderStyle = BorderStyle.FixedSingle;
            TransmitServerStateLight.Enabled = false;
            TransmitServerStateLight.Location = new Point(142, 70);
            TransmitServerStateLight.Margin = new Padding(2, 3, 2, 3);
            TransmitServerStateLight.MaxLength = 1;
            TransmitServerStateLight.Name = "TransmitServerStateLight";
            TransmitServerStateLight.ReadOnly = true;
            TransmitServerStateLight.ShortcutsEnabled = false;
            TransmitServerStateLight.Size = new Size(24, 23);
            TransmitServerStateLight.TabIndex = 75;
            // 
            // TransmitServerPortBox
            // 
            TransmitServerPortBox.DecimalPointNumber = 0;
            TransmitServerPortBox.DoubleValue = 0D;
            TransmitServerPortBox.FloatValue = 0F;
            TransmitServerPortBox.IntValue = 60721;
            TransmitServerPortBox.Location = new Point(12, 68);
            TransmitServerPortBox.LongValue = 0L;
            TransmitServerPortBox.Margin = new Padding(2, 3, 2, 3);
            TransmitServerPortBox.MaxValue = "65535";
            TransmitServerPortBox.MinValue = "0";
            TransmitServerPortBox.Name = "TransmitServerPortBox";
            TransmitServerPortBox.NumberType = NumberType.Integer;
            TransmitServerPortBox.Size = new Size(90, 23);
            TransmitServerPortBox.TabIndex = 6;
            TransmitServerPortBox.Text = "60721";
            // 
            // TransmitServerCloseButton
            // 
            TransmitServerCloseButton.Enabled = false;
            TransmitServerCloseButton.Location = new Point(109, 96);
            TransmitServerCloseButton.Margin = new Padding(2, 3, 2, 3);
            TransmitServerCloseButton.Name = "TransmitServerCloseButton";
            TransmitServerCloseButton.Size = new Size(92, 31);
            TransmitServerCloseButton.TabIndex = 5;
            TransmitServerCloseButton.Text = "关闭服务器";
            TransmitServerCloseButton.UseVisualStyleBackColor = true;
            TransmitServerCloseButton.Click += TransmitServerCloseButton_Click;
            // 
            // TransmitServerStartButton
            // 
            TransmitServerStartButton.Location = new Point(9, 95);
            TransmitServerStartButton.Margin = new Padding(2, 3, 2, 3);
            TransmitServerStartButton.Name = "TransmitServerStartButton";
            TransmitServerStartButton.Size = new Size(92, 31);
            TransmitServerStartButton.TabIndex = 4;
            TransmitServerStartButton.Text = "启动服务器";
            TransmitServerStartButton.UseVisualStyleBackColor = true;
            TransmitServerStartButton.Click += TransmitServerStartButton_Click;
            // 
            // TransmitServerIPBox3
            // 
            TransmitServerIPBox3.DecimalPointNumber = 0;
            TransmitServerIPBox3.DoubleValue = 0D;
            TransmitServerIPBox3.FloatValue = 0F;
            TransmitServerIPBox3.Location = new Point(107, 42);
            TransmitServerIPBox3.LongValue = 0L;
            TransmitServerIPBox3.Margin = new Padding(2, 3, 2, 3);
            TransmitServerIPBox3.MaxValue = "255";
            TransmitServerIPBox3.MinValue = "0";
            TransmitServerIPBox3.Name = "TransmitServerIPBox3";
            TransmitServerIPBox3.NumberType = NumberType.Integer;
            TransmitServerIPBox3.Size = new Size(43, 23);
            TransmitServerIPBox3.TabIndex = 3;
            TransmitServerIPBox3.Text = "0";
            // 
            // TransmitServerIPBox4
            // 
            TransmitServerIPBox4.DecimalPointNumber = 0;
            TransmitServerIPBox4.DoubleValue = 0D;
            TransmitServerIPBox4.FloatValue = 0F;
            TransmitServerIPBox4.IntValue = 1;
            TransmitServerIPBox4.Location = new Point(156, 42);
            TransmitServerIPBox4.LongValue = 0L;
            TransmitServerIPBox4.Margin = new Padding(2, 3, 2, 3);
            TransmitServerIPBox4.MaxValue = "255";
            TransmitServerIPBox4.MinValue = "0";
            TransmitServerIPBox4.Name = "TransmitServerIPBox4";
            TransmitServerIPBox4.NumberType = NumberType.Integer;
            TransmitServerIPBox4.Size = new Size(43, 23);
            TransmitServerIPBox4.TabIndex = 2;
            TransmitServerIPBox4.Text = "1";
            // 
            // TransmitServerIPBox2
            // 
            TransmitServerIPBox2.DecimalPointNumber = 0;
            TransmitServerIPBox2.DoubleValue = 0D;
            TransmitServerIPBox2.FloatValue = 0F;
            TransmitServerIPBox2.Location = new Point(60, 42);
            TransmitServerIPBox2.LongValue = 0L;
            TransmitServerIPBox2.Margin = new Padding(2, 3, 2, 3);
            TransmitServerIPBox2.MaxValue = "255";
            TransmitServerIPBox2.MinValue = "0";
            TransmitServerIPBox2.Name = "TransmitServerIPBox2";
            TransmitServerIPBox2.NumberType = NumberType.Integer;
            TransmitServerIPBox2.Size = new Size(43, 23);
            TransmitServerIPBox2.TabIndex = 1;
            TransmitServerIPBox2.Text = "0";
            // 
            // TransmitServerIPBox1
            // 
            TransmitServerIPBox1.DecimalPointNumber = 0;
            TransmitServerIPBox1.DoubleValue = 0D;
            TransmitServerIPBox1.FloatValue = 0F;
            TransmitServerIPBox1.IntValue = 127;
            TransmitServerIPBox1.Location = new Point(12, 42);
            TransmitServerIPBox1.LongValue = 0L;
            TransmitServerIPBox1.Margin = new Padding(2, 3, 2, 3);
            TransmitServerIPBox1.MaxValue = "255";
            TransmitServerIPBox1.MinValue = "0";
            TransmitServerIPBox1.Name = "TransmitServerIPBox1";
            TransmitServerIPBox1.NumberType = NumberType.Integer;
            TransmitServerIPBox1.Size = new Size(43, 23);
            TransmitServerIPBox1.TabIndex = 0;
            TransmitServerIPBox1.Text = "127";
            // 
            // ClientTCP_Panel
            // 
            ClientTCP_Panel.BorderStyle = BorderStyle.Fixed3D;
            ClientTCP_Panel.Controls.Add(label20);
            ClientTCP_Panel.Controls.Add(ClientPortBox);
            ClientTCP_Panel.Controls.Add(ClientIPBox3);
            ClientTCP_Panel.Controls.Add(ClientIPBox4);
            ClientTCP_Panel.Controls.Add(ClientIPBox2);
            ClientTCP_Panel.Controls.Add(ClientIPBox1);
            ClientTCP_Panel.Controls.Add(ClientDisconnectButton);
            ClientTCP_Panel.Controls.Add(ClientConnectButton);
            ClientTCP_Panel.Location = new Point(235, 223);
            ClientTCP_Panel.Margin = new Padding(2, 3, 2, 3);
            ClientTCP_Panel.Name = "ClientTCP_Panel";
            ClientTCP_Panel.Size = new Size(216, 171);
            ClientTCP_Panel.TabIndex = 75;
            ClientTCP_Panel.Visible = false;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(11, 7);
            label20.Margin = new Padding(2, 0, 2, 0);
            label20.Name = "label20";
            label20.Size = new Size(104, 17);
            label20.TabIndex = 85;
            label20.Text = "目标服务器地址：";
            // 
            // ClientPortBox
            // 
            ClientPortBox.DecimalPointNumber = 0;
            ClientPortBox.DoubleValue = 0D;
            ClientPortBox.FloatValue = 0F;
            ClientPortBox.IntValue = 60721;
            ClientPortBox.Location = new Point(12, 60);
            ClientPortBox.LongValue = 0L;
            ClientPortBox.Margin = new Padding(2, 3, 2, 3);
            ClientPortBox.MaxValue = "65535";
            ClientPortBox.MinValue = "0";
            ClientPortBox.Name = "ClientPortBox";
            ClientPortBox.NumberType = NumberType.Integer;
            ClientPortBox.Size = new Size(94, 23);
            ClientPortBox.TabIndex = 81;
            ClientPortBox.Text = "60721";
            // 
            // ClientIPBox3
            // 
            ClientIPBox3.DecimalPointNumber = 0;
            ClientIPBox3.DoubleValue = 0D;
            ClientIPBox3.FloatValue = 0F;
            ClientIPBox3.Location = new Point(104, 27);
            ClientIPBox3.LongValue = 0L;
            ClientIPBox3.Margin = new Padding(2, 3, 2, 3);
            ClientIPBox3.MaxValue = "255";
            ClientIPBox3.MinValue = "0";
            ClientIPBox3.Name = "ClientIPBox3";
            ClientIPBox3.NumberType = NumberType.Integer;
            ClientIPBox3.Size = new Size(43, 23);
            ClientIPBox3.TabIndex = 80;
            ClientIPBox3.Text = "0";
            // 
            // ClientIPBox4
            // 
            ClientIPBox4.DecimalPointNumber = 0;
            ClientIPBox4.DoubleValue = 0D;
            ClientIPBox4.FloatValue = 0F;
            ClientIPBox4.IntValue = 1;
            ClientIPBox4.Location = new Point(152, 27);
            ClientIPBox4.LongValue = 0L;
            ClientIPBox4.Margin = new Padding(2, 3, 2, 3);
            ClientIPBox4.MaxValue = "255";
            ClientIPBox4.MinValue = "0";
            ClientIPBox4.Name = "ClientIPBox4";
            ClientIPBox4.NumberType = NumberType.Integer;
            ClientIPBox4.Size = new Size(43, 23);
            ClientIPBox4.TabIndex = 79;
            ClientIPBox4.Text = "1";
            // 
            // ClientIPBox2
            // 
            ClientIPBox2.DecimalPointNumber = 0;
            ClientIPBox2.DoubleValue = 0D;
            ClientIPBox2.FloatValue = 0F;
            ClientIPBox2.Location = new Point(58, 27);
            ClientIPBox2.LongValue = 0L;
            ClientIPBox2.Margin = new Padding(2, 3, 2, 3);
            ClientIPBox2.MaxValue = "255";
            ClientIPBox2.MinValue = "0";
            ClientIPBox2.Name = "ClientIPBox2";
            ClientIPBox2.NumberType = NumberType.Integer;
            ClientIPBox2.Size = new Size(43, 23);
            ClientIPBox2.TabIndex = 78;
            ClientIPBox2.Text = "0";
            // 
            // ClientIPBox1
            // 
            ClientIPBox1.DecimalPointNumber = 0;
            ClientIPBox1.DoubleValue = 0D;
            ClientIPBox1.FloatValue = 0F;
            ClientIPBox1.IntValue = 127;
            ClientIPBox1.Location = new Point(11, 27);
            ClientIPBox1.LongValue = 0L;
            ClientIPBox1.Margin = new Padding(2, 3, 2, 3);
            ClientIPBox1.MaxValue = "255";
            ClientIPBox1.MinValue = "0";
            ClientIPBox1.Name = "ClientIPBox1";
            ClientIPBox1.NumberType = NumberType.Integer;
            ClientIPBox1.Size = new Size(43, 23);
            ClientIPBox1.TabIndex = 77;
            ClientIPBox1.Text = "127";
            // 
            // ClientDisconnectButton
            // 
            ClientDisconnectButton.Enabled = false;
            ClientDisconnectButton.Location = new Point(107, 92);
            ClientDisconnectButton.Margin = new Padding(2, 3, 2, 3);
            ClientDisconnectButton.Name = "ClientDisconnectButton";
            ClientDisconnectButton.Size = new Size(93, 60);
            ClientDisconnectButton.TabIndex = 11;
            ClientDisconnectButton.Text = "断开";
            ClientDisconnectButton.UseVisualStyleBackColor = true;
            ClientDisconnectButton.Click += ClientDisconnectButton_Click;
            // 
            // ClientConnectButton
            // 
            ClientConnectButton.Location = new Point(8, 92);
            ClientConnectButton.Margin = new Padding(2, 3, 2, 3);
            ClientConnectButton.Name = "ClientConnectButton";
            ClientConnectButton.Size = new Size(93, 60);
            ClientConnectButton.TabIndex = 10;
            ClientConnectButton.Text = "连接";
            ClientConnectButton.UseVisualStyleBackColor = true;
            ClientConnectButton.Click += ClientConnectButton_Click;
            // 
            // UDP_Panel
            // 
            UDP_Panel.BorderStyle = BorderStyle.Fixed3D;
            UDP_Panel.Controls.Add(label17);
            UDP_Panel.Controls.Add(label16);
            UDP_Panel.Controls.Add(label15);
            UDP_Panel.Controls.Add(UDPPortBox);
            UDP_Panel.Controls.Add(UdpTargetIPBox3);
            UDP_Panel.Controls.Add(UdpTargetIPBox4);
            UDP_Panel.Controls.Add(UdpTargetIPBox2);
            UDP_Panel.Controls.Add(UdpTargetIPBox1);
            UDP_Panel.Controls.Add(UdpLocalIPBox3);
            UDP_Panel.Controls.Add(UdpLocalIPBox4);
            UDP_Panel.Controls.Add(UdpLocalIPBox2);
            UDP_Panel.Controls.Add(UdpLocalIPBox1);
            UDP_Panel.Controls.Add(UDPUnbindButton);
            UDP_Panel.Controls.Add(UDPBindButton);
            UDP_Panel.Location = new Point(455, 45);
            UDP_Panel.Margin = new Padding(2, 3, 2, 3);
            UDP_Panel.Name = "UDP_Panel";
            UDP_Panel.Size = new Size(216, 171);
            UDP_Panel.TabIndex = 82;
            UDP_Panel.Visible = false;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(11, 92);
            label17.Margin = new Padding(2, 0, 2, 0);
            label17.Name = "label17";
            label17.Size = new Size(32, 17);
            label17.TabIndex = 89;
            label17.Text = "端口";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(3, 60);
            label16.Margin = new Padding(2, 0, 2, 0);
            label16.Name = "label16";
            label16.Size = new Size(43, 17);
            label16.TabIndex = 88;
            label16.Text = "目标IP";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(3, 21);
            label15.Margin = new Padding(2, 0, 2, 0);
            label15.Name = "label15";
            label15.Size = new Size(43, 17);
            label15.TabIndex = 87;
            label15.Text = "本地IP";
            // 
            // UDPPortBox
            // 
            UDPPortBox.DecimalPointNumber = 0;
            UDPPortBox.DoubleValue = 0D;
            UDPPortBox.FloatValue = 0F;
            UDPPortBox.IntValue = 60721;
            UDPPortBox.Location = new Point(53, 88);
            UDPPortBox.LongValue = 0L;
            UDPPortBox.Margin = new Padding(2, 3, 2, 3);
            UDPPortBox.MaxValue = "65535";
            UDPPortBox.MinValue = "0";
            UDPPortBox.Name = "UDPPortBox";
            UDPPortBox.NumberType = NumberType.Integer;
            UDPPortBox.Size = new Size(72, 23);
            UDPPortBox.TabIndex = 86;
            UDPPortBox.Text = "60721";
            // 
            // UdpTargetIPBox3
            // 
            UdpTargetIPBox3.DecimalPointNumber = 0;
            UdpTargetIPBox3.DoubleValue = 0D;
            UdpTargetIPBox3.FloatValue = 0F;
            UdpTargetIPBox3.Location = new Point(129, 54);
            UdpTargetIPBox3.LongValue = 0L;
            UdpTargetIPBox3.Margin = new Padding(2, 3, 2, 3);
            UdpTargetIPBox3.MaxValue = "255";
            UdpTargetIPBox3.MinValue = "0";
            UdpTargetIPBox3.Name = "UdpTargetIPBox3";
            UdpTargetIPBox3.NumberType = NumberType.Integer;
            UdpTargetIPBox3.Size = new Size(34, 23);
            UdpTargetIPBox3.TabIndex = 85;
            UdpTargetIPBox3.Text = "0";
            // 
            // UdpTargetIPBox4
            // 
            UdpTargetIPBox4.DecimalPointNumber = 0;
            UdpTargetIPBox4.DoubleValue = 0D;
            UdpTargetIPBox4.FloatValue = 0F;
            UdpTargetIPBox4.IntValue = 1;
            UdpTargetIPBox4.Location = new Point(168, 54);
            UdpTargetIPBox4.LongValue = 0L;
            UdpTargetIPBox4.Margin = new Padding(2, 3, 2, 3);
            UdpTargetIPBox4.MaxValue = "255";
            UdpTargetIPBox4.MinValue = "0";
            UdpTargetIPBox4.Name = "UdpTargetIPBox4";
            UdpTargetIPBox4.NumberType = NumberType.Integer;
            UdpTargetIPBox4.Size = new Size(34, 23);
            UdpTargetIPBox4.TabIndex = 84;
            UdpTargetIPBox4.Text = "1";
            // 
            // UdpTargetIPBox2
            // 
            UdpTargetIPBox2.DecimalPointNumber = 0;
            UdpTargetIPBox2.DoubleValue = 0D;
            UdpTargetIPBox2.FloatValue = 0F;
            UdpTargetIPBox2.Location = new Point(91, 54);
            UdpTargetIPBox2.LongValue = 0L;
            UdpTargetIPBox2.Margin = new Padding(2, 3, 2, 3);
            UdpTargetIPBox2.MaxValue = "255";
            UdpTargetIPBox2.MinValue = "0";
            UdpTargetIPBox2.Name = "UdpTargetIPBox2";
            UdpTargetIPBox2.NumberType = NumberType.Integer;
            UdpTargetIPBox2.Size = new Size(34, 23);
            UdpTargetIPBox2.TabIndex = 83;
            UdpTargetIPBox2.Text = "0";
            // 
            // UdpTargetIPBox1
            // 
            UdpTargetIPBox1.DecimalPointNumber = 0;
            UdpTargetIPBox1.DoubleValue = 0D;
            UdpTargetIPBox1.FloatValue = 0F;
            UdpTargetIPBox1.IntValue = 127;
            UdpTargetIPBox1.Location = new Point(53, 54);
            UdpTargetIPBox1.LongValue = 0L;
            UdpTargetIPBox1.Margin = new Padding(2, 3, 2, 3);
            UdpTargetIPBox1.MaxValue = "255";
            UdpTargetIPBox1.MinValue = "0";
            UdpTargetIPBox1.Name = "UdpTargetIPBox1";
            UdpTargetIPBox1.NumberType = NumberType.Integer;
            UdpTargetIPBox1.Size = new Size(34, 23);
            UdpTargetIPBox1.TabIndex = 82;
            UdpTargetIPBox1.Text = "127";
            // 
            // UdpLocalIPBox3
            // 
            UdpLocalIPBox3.DecimalPointNumber = 0;
            UdpLocalIPBox3.DoubleValue = 0D;
            UdpLocalIPBox3.FloatValue = 0F;
            UdpLocalIPBox3.Location = new Point(129, 18);
            UdpLocalIPBox3.LongValue = 0L;
            UdpLocalIPBox3.Margin = new Padding(2, 3, 2, 3);
            UdpLocalIPBox3.MaxValue = "255";
            UdpLocalIPBox3.MinValue = "0";
            UdpLocalIPBox3.Name = "UdpLocalIPBox3";
            UdpLocalIPBox3.NumberType = NumberType.Integer;
            UdpLocalIPBox3.Size = new Size(34, 23);
            UdpLocalIPBox3.TabIndex = 80;
            UdpLocalIPBox3.Text = "0";
            // 
            // UdpLocalIPBox4
            // 
            UdpLocalIPBox4.DecimalPointNumber = 0;
            UdpLocalIPBox4.DoubleValue = 0D;
            UdpLocalIPBox4.FloatValue = 0F;
            UdpLocalIPBox4.IntValue = 1;
            UdpLocalIPBox4.Location = new Point(168, 18);
            UdpLocalIPBox4.LongValue = 0L;
            UdpLocalIPBox4.Margin = new Padding(2, 3, 2, 3);
            UdpLocalIPBox4.MaxValue = "255";
            UdpLocalIPBox4.MinValue = "0";
            UdpLocalIPBox4.Name = "UdpLocalIPBox4";
            UdpLocalIPBox4.NumberType = NumberType.Integer;
            UdpLocalIPBox4.Size = new Size(34, 23);
            UdpLocalIPBox4.TabIndex = 79;
            UdpLocalIPBox4.Text = "1";
            // 
            // UdpLocalIPBox2
            // 
            UdpLocalIPBox2.DecimalPointNumber = 0;
            UdpLocalIPBox2.DoubleValue = 0D;
            UdpLocalIPBox2.FloatValue = 0F;
            UdpLocalIPBox2.Location = new Point(91, 18);
            UdpLocalIPBox2.LongValue = 0L;
            UdpLocalIPBox2.Margin = new Padding(2, 3, 2, 3);
            UdpLocalIPBox2.MaxValue = "255";
            UdpLocalIPBox2.MinValue = "0";
            UdpLocalIPBox2.Name = "UdpLocalIPBox2";
            UdpLocalIPBox2.NumberType = NumberType.Integer;
            UdpLocalIPBox2.Size = new Size(34, 23);
            UdpLocalIPBox2.TabIndex = 78;
            UdpLocalIPBox2.Text = "0";
            // 
            // UdpLocalIPBox1
            // 
            UdpLocalIPBox1.DecimalPointNumber = 0;
            UdpLocalIPBox1.DoubleValue = 0D;
            UdpLocalIPBox1.FloatValue = 0F;
            UdpLocalIPBox1.IntValue = 127;
            UdpLocalIPBox1.Location = new Point(53, 18);
            UdpLocalIPBox1.LongValue = 0L;
            UdpLocalIPBox1.Margin = new Padding(2, 3, 2, 3);
            UdpLocalIPBox1.MaxValue = "255";
            UdpLocalIPBox1.MinValue = "0";
            UdpLocalIPBox1.Name = "UdpLocalIPBox1";
            UdpLocalIPBox1.NumberType = NumberType.Integer;
            UdpLocalIPBox1.Size = new Size(34, 23);
            UdpLocalIPBox1.TabIndex = 77;
            UdpLocalIPBox1.Text = "127";
            // 
            // UDPUnbindButton
            // 
            UDPUnbindButton.Enabled = false;
            UDPUnbindButton.Location = new Point(113, 122);
            UDPUnbindButton.Margin = new Padding(2, 3, 2, 3);
            UDPUnbindButton.Name = "UDPUnbindButton";
            UDPUnbindButton.Size = new Size(86, 35);
            UDPUnbindButton.TabIndex = 11;
            UDPUnbindButton.Text = "解绑";
            UDPUnbindButton.UseVisualStyleBackColor = true;
            UDPUnbindButton.Click += UDPUnbindButton_Click;
            // 
            // UDPBindButton
            // 
            UDPBindButton.Location = new Point(19, 122);
            UDPBindButton.Margin = new Padding(2, 3, 2, 3);
            UDPBindButton.Name = "UDPBindButton";
            UDPBindButton.Size = new Size(86, 35);
            UDPBindButton.TabIndex = 10;
            UDPBindButton.Text = "绑定";
            UDPBindButton.UseVisualStyleBackColor = true;
            UDPBindButton.Click += UDPBindButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { ChooseDataInput, ToolsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 3, 0, 3);
            menuStrip1.Size = new Size(1362, 27);
            menuStrip1.TabIndex = 83;
            menuStrip1.Text = "menuStrip1";
            // 
            // ChooseDataInput
            // 
            ChooseDataInput.DropDownItems.AddRange(new ToolStripItem[] { SerialPortToolStripMenuItem, TCPserverToolStripMenuItem, TCPClientToolStripMenuItem, UDPToolStripMenuItem });
            ChooseDataInput.Name = "ChooseDataInput";
            ChooseDataInput.Size = new Size(56, 21);
            ChooseDataInput.Text = "数据源";
            // 
            // SerialPortToolStripMenuItem
            // 
            SerialPortToolStripMenuItem.Checked = true;
            SerialPortToolStripMenuItem.CheckState = CheckState.Checked;
            SerialPortToolStripMenuItem.Name = "SerialPortToolStripMenuItem";
            SerialPortToolStripMenuItem.Size = new Size(143, 22);
            SerialPortToolStripMenuItem.Text = "SerialPort";
            SerialPortToolStripMenuItem.Click += SerialPortToolStripMenuItem_Click;
            // 
            // TCPserverToolStripMenuItem
            // 
            TCPserverToolStripMenuItem.Name = "TCPserverToolStripMenuItem";
            TCPserverToolStripMenuItem.Size = new Size(143, 22);
            TCPserverToolStripMenuItem.Text = "TCP(Server)";
            TCPserverToolStripMenuItem.Click += TCPserverToolStripMenuItem_Click;
            // 
            // TCPClientToolStripMenuItem
            // 
            TCPClientToolStripMenuItem.Name = "TCPClientToolStripMenuItem";
            TCPClientToolStripMenuItem.Size = new Size(143, 22);
            TCPClientToolStripMenuItem.Text = "TCP(Client)";
            TCPClientToolStripMenuItem.Click += TCPClientToolStripMenuItem_Click;
            // 
            // UDPToolStripMenuItem
            // 
            UDPToolStripMenuItem.Name = "UDPToolStripMenuItem";
            UDPToolStripMenuItem.Size = new Size(143, 22);
            UDPToolStripMenuItem.Text = "UDP";
            UDPToolStripMenuItem.Click += UDPToolStripMenuItem_Click;
            // 
            // ToolsToolStripMenuItem
            // 
            ToolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { OpenHexToCharTool, ReadConfig, SaveConfigToAnother, OpenHelp });
            ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
            ToolsToolStripMenuItem.Size = new Size(44, 21);
            ToolsToolStripMenuItem.Text = "工具";
            // 
            // OpenHexToCharTool
            // 
            OpenHexToCharTool.Name = "OpenHexToCharTool";
            OpenHexToCharTool.Size = new Size(198, 22);
            OpenHexToCharTool.Text = "打开CHAR-HEX小工具";
            OpenHexToCharTool.Click += OpenHexToCharTool_Click;
            // 
            // ReadConfig
            // 
            ReadConfig.Name = "ReadConfig";
            ReadConfig.Size = new Size(198, 22);
            ReadConfig.Text = "读取配置文件";
            ReadConfig.Click += ReadConfig_Click;
            // 
            // SaveConfigToAnother
            // 
            SaveConfigToAnother.Name = "SaveConfigToAnother";
            SaveConfigToAnother.Size = new Size(198, 22);
            SaveConfigToAnother.Text = "配置文件另存为";
            SaveConfigToAnother.Click += SaveConfigToAnother_Click;
            // 
            // OpenHelp
            // 
            OpenHelp.Name = "OpenHelp";
            OpenHelp.Size = new Size(198, 22);
            OpenHelp.Text = "帮助";
            OpenHelp.Click += OpenHelp_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(7, 31);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(80, 17);
            label8.TabIndex = 84;
            label8.Text = "当前数据源：";
            // 
            // ReceiveModeSelectText
            // 
            ReceiveModeSelectText.AutoSize = true;
            ReceiveModeSelectText.Location = new Point(75, 31);
            ReceiveModeSelectText.Margin = new Padding(2, 0, 2, 0);
            ReceiveModeSelectText.Name = "ReceiveModeSelectText";
            ReceiveModeSelectText.Size = new Size(64, 17);
            ReceiveModeSelectText.TabIndex = 85;
            ReceiveModeSelectText.Text = "SerialPort";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.ImeMode = ImeMode.NoControl;
            label18.Location = new Point(1013, 519);
            label18.Name = "label18";
            label18.Size = new Size(92, 17);
            label18.TabIndex = 86;
            label18.Text = "缓冲区附加长度";
            // 
            // AdditionalBufferLengthBox
            // 
            AdditionalBufferLengthBox.BackColor = SystemColors.ButtonHighlight;
            AdditionalBufferLengthBox.Location = new Point(1108, 517);
            AdditionalBufferLengthBox.Margin = new Padding(3, 4, 3, 4);
            AdditionalBufferLengthBox.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            AdditionalBufferLengthBox.Name = "AdditionalBufferLengthBox";
            AdditionalBufferLengthBox.Size = new Size(66, 23);
            AdditionalBufferLengthBox.TabIndex = 87;
            // 
            // XStep
            // 
            XStep.DecimalPointNumber = 2;
            XStep.DoubleValue = 1D;
            XStep.FloatValue = 0F;
            XStep.Location = new Point(1288, 554);
            XStep.LongValue = 0L;
            XStep.Margin = new Padding(2, 3, 2, 3);
            XStep.MaxValue = "";
            XStep.MinValue = "";
            XStep.Name = "XStep";
            XStep.NumberType = NumberType.Double;
            XStep.Size = new Size(58, 23);
            XStep.TabIndex = 63;
            XStep.Text = "1.00";
            XStep.Leave += XStep_Leave;
            // 
            // YStep
            // 
            YStep.DecimalPointNumber = 2;
            YStep.DoubleValue = 1D;
            YStep.FloatValue = 0F;
            YStep.Location = new Point(1288, 585);
            YStep.LongValue = 0L;
            YStep.Margin = new Padding(2, 3, 2, 3);
            YStep.MaxValue = "";
            YStep.MinValue = "";
            YStep.Name = "YStep";
            YStep.NumberType = NumberType.Double;
            YStep.Size = new Size(58, 23);
            YStep.TabIndex = 62;
            YStep.Text = "1.00";
            YStep.Leave += YStep_Leave;
            // 
            // RightLimit
            // 
            RightLimit.DecimalPointNumber = 2;
            RightLimit.DoubleValue = 50D;
            RightLimit.FloatValue = 0F;
            RightLimit.Location = new Point(1148, 554);
            RightLimit.LongValue = 0L;
            RightLimit.Margin = new Padding(2, 3, 2, 3);
            RightLimit.MaxValue = "";
            RightLimit.MinValue = "";
            RightLimit.Name = "RightLimit";
            RightLimit.NumberType = NumberType.Double;
            RightLimit.Size = new Size(58, 23);
            RightLimit.TabIndex = 57;
            RightLimit.Text = "50.00";
            RightLimit.Leave += RightLimit_Leave;
            // 
            // LeftLimit
            // 
            LeftLimit.DecimalPointNumber = 2;
            LeftLimit.DoubleValue = 0D;
            LeftLimit.FloatValue = 0F;
            LeftLimit.Location = new Point(1085, 554);
            LeftLimit.LongValue = 0L;
            LeftLimit.Margin = new Padding(2, 3, 2, 3);
            LeftLimit.MaxValue = "";
            LeftLimit.MinValue = "";
            LeftLimit.Name = "LeftLimit";
            LeftLimit.NumberType = NumberType.Double;
            LeftLimit.Size = new Size(58, 23);
            LeftLimit.TabIndex = 56;
            LeftLimit.Text = "0.00";
            LeftLimit.Leave += LeftLimit_Leave;
            // 
            // ButtomLimit
            // 
            ButtomLimit.DecimalPointNumber = 2;
            ButtomLimit.DoubleValue = -5D;
            ButtomLimit.FloatValue = 0F;
            ButtomLimit.Location = new Point(1086, 585);
            ButtomLimit.LongValue = 0L;
            ButtomLimit.Margin = new Padding(2, 3, 2, 3);
            ButtomLimit.MaxValue = "";
            ButtomLimit.MinValue = "";
            ButtomLimit.Name = "ButtomLimit";
            ButtomLimit.NumberType = NumberType.Double;
            ButtomLimit.Size = new Size(58, 23);
            ButtomLimit.TabIndex = 55;
            ButtomLimit.Text = "-5.00";
            ButtomLimit.Leave += ButtomLimit_Leave;
            // 
            // TopLimit
            // 
            TopLimit.DecimalPointNumber = 2;
            TopLimit.DoubleValue = 5D;
            TopLimit.FloatValue = 0F;
            TopLimit.Location = new Point(1148, 585);
            TopLimit.LongValue = 0L;
            TopLimit.Margin = new Padding(2, 3, 2, 3);
            TopLimit.MaxValue = "";
            TopLimit.MinValue = "";
            TopLimit.Name = "TopLimit";
            TopLimit.NumberType = NumberType.Double;
            TopLimit.Size = new Size(58, 23);
            TopLimit.TabIndex = 54;
            TopLimit.Text = "5.00";
            TopLimit.Leave += TopLimit_Leave;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1362, 646);
            Controls.Add(menuStrip1);
            Controls.Add(AdditionalBufferLengthBox);
            Controls.Add(label18);
            Controls.Add(ReceiveModeSelectText);
            Controls.Add(label8);
            Controls.Add(UDP_Panel);
            Controls.Add(ClientTCP_Panel);
            Controls.Add(panel2);
            Controls.Add(ServerTCP_Panel);
            Controls.Add(panel1);
            Controls.Add(StateMassageTextBox);
            Controls.Add(label14);
            Controls.Add(StopRec);
            Controls.Add(FileNameText);
            Controls.Add(FileTypeText);
            Controls.Add(XStep);
            Controls.Add(YStep);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(RightLimit);
            Controls.Add(LeftLimit);
            Controls.Add(ButtomLimit);
            Controls.Add(TopLimit);
            Controls.Add(ClearData);
            Controls.Add(SavePlot);
            Controls.Add(label9);
            Controls.Add(ChannelLength);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(EndTextBox);
            Controls.Add(HeadTextBox);
            Controls.Add(ClearRec);
            Controls.Add(StrSendButton);
            Controls.Add(HexSendButton);
            Controls.Add(HexSendText);
            Controls.Add(StrSendText);
            Controls.Add(RecText);
            Controls.Add(ParityBox);
            Controls.Add(StopBitsBox);
            Controls.Add(DataBitsBox);
            Controls.Add(BaudRateBox);
            Controls.Add(PortNameBox);
            Controls.Add(flowLayoutPanel3);
            Controls.Add(formPlot1);
            Controls.Add(SerialPortPanel);
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "Form1";
            RightToLeftLayout = true;
            ShowIcon = false;
            Text = "PortOSC";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)ChannelLength).EndInit();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            SerialPortPanel.ResumeLayout(false);
            ServerTCP_Panel.ResumeLayout(false);
            ServerTCP_Panel.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ClientTCP_Panel.ResumeLayout(false);
            ClientTCP_Panel.PerformLayout();
            UDP_Panel.ResumeLayout(false);
            UDP_Panel.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)AdditionalBufferLengthBox).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox PortNameBox;
        private System.Windows.Forms.ComboBox BaudRateBox;
        private System.Windows.Forms.ComboBox DataBitsBox;
        private System.Windows.Forms.ComboBox StopBitsBox;
        private System.Windows.Forms.ComboBox ParityBox;
        private System.Windows.Forms.TextBox RecText;
        private System.Windows.Forms.TextBox StrSendText;
        private System.Windows.Forms.TextBox HexSendText;
        private System.Windows.Forms.Button OpenSerialPort;
        private System.Windows.Forms.Button CloseSerialPort;
        private System.Windows.Forms.CheckBox EnableOsc;
        private System.Windows.Forms.Button HexSendButton;
        private System.Windows.Forms.Button StrSendButton;
        private System.Windows.Forms.Button ClearRec;
        private System.Windows.Forms.TextBox HeadTextBox;
        private System.Windows.Forms.TextBox EndTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private ScottPlot.FormsPlot formPlot1;
        private System.Windows.Forms.Button SavePlot;
        private System.Windows.Forms.Button ClearData;
        private NumberTextBox TopLimit;
        private NumberTextBox ButtomLimit;
        private NumberTextBox LeftLimit;
        private NumberTextBox RightLimit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private NumberTextBox YStep;
        private NumberTextBox XStep;
        private System.Windows.Forms.ComboBox FileTypeText;
        private System.Windows.Forms.TextBox FileNameText;
        private System.Windows.Forms.NumericUpDown ChannelLength;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.RichTextBox StateMassageTextBox;
        private System.Windows.Forms.CheckBox StopRec;
        private System.Windows.Forms.CheckBox RecNewLine;
        private System.Windows.Forms.RadioButton ShowHex;
        private System.Windows.Forms.RadioButton ShowStr;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel SerialPortPanel;
        private System.Windows.Forms.Panel ServerTCP_Panel;
        private System.Windows.Forms.Panel panel2;
        private NumberTextBox TransmitServerIPBox3;
        private NumberTextBox TransmitServerIPBox4;
        private NumberTextBox TransmitServerIPBox2;
        private NumberTextBox TransmitServerIPBox1;
        private System.Windows.Forms.TextBox TransmitServerStateLight;
        private System.Windows.Forms.Button TransmitServerCloseButton;
        private System.Windows.Forms.Button TransmitServerStartButton;
        private System.Windows.Forms.TextBox ServerStateLight;
        private System.Windows.Forms.Button ServerCloseButton;
        private System.Windows.Forms.Button ServerStartButton;
        private NumberTextBox ServerPortBox;
        private NumberTextBox ServerIPBox3;
        private NumberTextBox ServerIPBox4;
        private NumberTextBox ServerIPBox2;
        private NumberTextBox ServerIPBox1;
        private NumberTextBox TransmitServerPortBox;
        private System.Windows.Forms.Panel ClientTCP_Panel;
        private NumberTextBox ClientPortBox;
        private NumberTextBox ClientIPBox3;
        private NumberTextBox ClientIPBox4;
        private NumberTextBox ClientIPBox2;
        private NumberTextBox ClientIPBox1;
        private System.Windows.Forms.Button ClientDisconnectButton;
        private System.Windows.Forms.Button ClientConnectButton;
        private System.Windows.Forms.Panel UDP_Panel;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private NumberTextBox UDPPortBox;
        private NumberTextBox UdpTargetIPBox3;
        private NumberTextBox UdpTargetIPBox4;
        private NumberTextBox UdpTargetIPBox2;
        private NumberTextBox UdpTargetIPBox1;
        private NumberTextBox UdpLocalIPBox3;
        private NumberTextBox UdpLocalIPBox4;
        private NumberTextBox UdpLocalIPBox2;
        private NumberTextBox UdpLocalIPBox1;
        private System.Windows.Forms.Button UDPUnbindButton;
        private System.Windows.Forms.Button UDPBindButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenHexToCharTool;
        private System.Windows.Forms.ToolStripMenuItem ReadConfig;
        private System.Windows.Forms.ToolStripMenuItem SaveConfigToAnother;
        private System.Windows.Forms.ToolStripMenuItem OpenHelp;
        private System.Windows.Forms.ToolStripMenuItem ChooseDataInput;
        private System.Windows.Forms.ToolStripMenuItem SerialPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TCPserverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TCPClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UDPToolStripMenuItem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label ReceiveModeSelectText;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown AdditionalBufferLengthBox;
        private System.Windows.Forms.CheckBox StopReceiveCheckBox;
        private System.Windows.Forms.CheckBox StopSendCheckBox;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private Label label21;
    }
}

