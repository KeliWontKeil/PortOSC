using _SerialPortSource;
using System.Collections.Concurrent;
using System.Data;
using System.Diagnostics;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;
using PortOSC.Services;
using PortOSC.Transport;
using TcpConnect;
using Tools;
using UdpConnect;

namespace PortOSC
{

    public partial class Form1 : Form
    {
        private const int WM_DEVICECHANGE = 0x219; //设备改变

        private readonly SerialPortSource OSCSerialPort;
        private readonly OneClientConnectiongOfServer OSCServerConnection;
        private readonly SimpleTcpClient OSCClient;
        private readonly SimpleUdpEndpoint OSCUDPEndpoint;

        private readonly ReceiveEndpointHub _receiveEndpointHub;
        private readonly ReceivePipeline _receivePipeline;
        private readonly ConcurrentQueue<double[]> _plotQueue;
        private readonly System.Windows.Forms.Timer _plotRefreshTimer;
        private string HeadStr, EndStr;
        private List<ScottPlot.Plottable.DataLogger> loggers;
        private int TimeStamp;
        private ScottPlot.Plottable.HLine Hline;
        private ScottPlot.Plottable.VLine Vline;

        private readonly OneClientConnectiongOfServer TransmitServerConnection;

        private class DataPoint(double x, double y)
        {
            public double X { get; set; } = x;
            public double Y { get; set; } = y;
        }
        private List<List<DataPoint>> dataPoints;

        private Form_HexToChar? HexToCharToolForm;

        //UI更新相关

        public Form1()
        {
            InitializeComponent();

            _receivePipeline = new ReceivePipeline();
            _plotQueue = [];
            _plotRefreshTimer = new System.Windows.Forms.Timer();
            HeadStr = new string("");
            EndStr = new string("");
            loggers = [];
            Hline = new ScottPlot.Plottable.HLine();
            Vline = new ScottPlot.Plottable.VLine();
            dataPoints = [];

            ServerTCP_Panel.Location = SerialPortPanel.Location;
            ClientTCP_Panel.Location = SerialPortPanel.Location;
            UDP_Panel.Location = SerialPortPanel.Location;
            PortNameBox.Items.Clear();
            PortNameBox.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            FileTypeText.Text = ".png";
            ReceiveModeSelectText.Text = "SerialPort";

            LoadConfig("DefaultConfig.json");

            OSCSerialPort = new SerialPortSource();
            OSCSerialPort.SerialState.ValueChangedOccured += SerialStateChangeHandle;
            OSCSerialPort.ReceiveErrorOccurred += SerialReceiveErrorHandle;

            OSCServerConnection = new OneClientConnectiongOfServer();
            OSCServerConnection.ReceiveErrorOccurred += OSCServerReceiveErrorHandle;
            OSCServerConnection.SendErrorOccurred += OSCServerSendErrorHandle;
            OSCServerConnection.ServerConnectState.ValueChangedOccured += OSCServerStateChangeHandle;

            OSCClient = new SimpleTcpClient();
            OSCClient.ReceiveErrorOccurred += OSCClientReceiveErrorHandle;
            OSCClient.SendErrorOccurred += OSCClientSendErrorHandle;
            OSCClient.ClientConnectState.ValueChangedOccured += OSCClientStateChangeHandle;

            OSCUDPEndpoint = new SimpleUdpEndpoint();
            OSCUDPEndpoint.ReceiveErrorOccurred += OSCUDPReceiveErrorHandle;
            OSCUDPEndpoint.SendErrorOccurred += OSCUDPSendErrorHandle;
            OSCUDPEndpoint.UdpState.ValueChangedOccured += OSCUDPStateChangeHandle;

            _receiveEndpointHub = new ReceiveEndpointHub();
            _receiveEndpointHub.Register(OSCSerialPort);
            _receiveEndpointHub.Register(OSCServerConnection);
            _receiveEndpointHub.Register(OSCClient);
            _receiveEndpointHub.Register(OSCUDPEndpoint);
            _receiveEndpointHub.DataReceived += PortReceivDataHandle;

            TransmitServerConnection = new OneClientConnectiongOfServer();
            TransmitServerConnection.ReceiveErrorOccurred += TransmitServerReceiveErrorHandle;
            TransmitServerConnection.SendErrorOccurred += TransmitServerSendErrorHandle;
            TransmitServerConnection.ReceiveMassage += TransmitServerReceivedMassageSend;
            TransmitServerConnection.ServerConnectState.ValueChangedOccured += TransmitServerStateChangeHandle;

            _plotRefreshTimer.Interval = 33;
            _plotRefreshTimer.Tick += PlotRefreshTimerTick;
            _plotRefreshTimer.Start();

        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_DEVICECHANGE:
                    var Port = SerialPort.GetPortNames();
                    if (OSCSerialPort.SerialState.Value == true)
                    {
                        foreach (string port in SerialPort.GetPortNames())
                        {
                            if (port == OSCSerialPort.SerialConfig.PortName)
                            {
                                return;
                            }
                        }
                        Tools.ShowMassageTools.ReportMassage(StateMassageTextBox, "设备离线，已关闭串口", ShowMassageTools.LogType.Error);
                        OSCSerialPort.Close();
                    }
                    OSCSerialPort.SerialState.Value = false;
                    PortNameBox.Items.Clear();
                    PortNameBox.Items.AddRange(Port);
                    break;
            }
        }

        private void FormsPlot1_Load(object sender, EventArgs e)
        {
            formPlot1.Plot.Clear();
            formPlot1.Plot.Title("");
            ResetPlot();
        }

        private void SerialPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerialPortToolStripMenuItem.Checked = true;
            TCPserverToolStripMenuItem.Checked = false;
            TCPClientToolStripMenuItem.Checked = false;
            UDPToolStripMenuItem.Checked = false;
            ReceiveModeSelectText.Text = "SerialPort";
            PanelVisableChange();
        }

        private void TCPserverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerialPortToolStripMenuItem.Checked = false;
            TCPserverToolStripMenuItem.Checked = true;
            TCPClientToolStripMenuItem.Checked = false;
            UDPToolStripMenuItem.Checked = false;
            ReceiveModeSelectText.Text = "TCP(Server)";
            PanelVisableChange();
        }

        private void TCPClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerialPortToolStripMenuItem.Checked = false;
            TCPserverToolStripMenuItem.Checked = false;
            TCPClientToolStripMenuItem.Checked = true;
            UDPToolStripMenuItem.Checked = false;
            ReceiveModeSelectText.Text = "TCP(Client)";
            PanelVisableChange();
        }

        private void UDPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerialPortToolStripMenuItem.Checked = false;
            TCPserverToolStripMenuItem.Checked = false;
            TCPClientToolStripMenuItem.Checked = false;
            UDPToolStripMenuItem.Checked = true;
            ReceiveModeSelectText.Text = "UDP";
            PanelVisableChange();
        }

        private void PanelVisableChange()
        {
            SerialPortPanel.Visible = false;
            ServerTCP_Panel.Visible = false;
            ClientTCP_Panel.Visible = false;
            UDP_Panel.Visible = false;
            if (SerialPortToolStripMenuItem.Checked)
            {
                SerialPortPanel.Visible = true;
            }
            if (TCPserverToolStripMenuItem.Checked)
            {
                ServerTCP_Panel.Visible = true;
            }
            if (TCPClientToolStripMenuItem.Checked)
            {
                ClientTCP_Panel.Visible = true;
            }
            if (UDPToolStripMenuItem.Checked)
            {
                UDP_Panel.Visible = true;
            }
        }

        private void TransmitServerStateChangeHandle(object? sender, OneClientConnectiongOfServer.ConnectState e)
        {
            SafeOperateTools.SafeInvoke(this, obj =>
            {
                switch (e)
                {
                    case OneClientConnectiongOfServer.ConnectState.Disconnect:
                        TransmitServerStateLight.BackColor = System.Drawing.Color.Red;
                        TransmitServerCloseButton.Enabled = false;
                        TransmitServerStartButton.Enabled = true;
                        TransmitServerIPBox1.Enabled = true;
                        TransmitServerIPBox2.Enabled = true;
                        TransmitServerIPBox3.Enabled = true;
                        TransmitServerIPBox4.Enabled = true;
                        TransmitServerPortBox.Enabled = true;
                        break;
                    case OneClientConnectiongOfServer.ConnectState.WaitConnect:
                        TransmitServerStateLight.BackColor = System.Drawing.Color.Blue;
                        TransmitServerCloseButton.Enabled = true;
                        TransmitServerStartButton.Enabled = false;
                        TransmitServerIPBox1.Enabled = false;
                        TransmitServerIPBox2.Enabled = false;
                        TransmitServerIPBox3.Enabled = false;
                        TransmitServerIPBox4.Enabled = false;
                        TransmitServerPortBox.Enabled = false;
                        break;
                    case OneClientConnectiongOfServer.ConnectState.Connected:
                        TransmitServerStateLight.BackColor = System.Drawing.Color.Green;
                        TransmitServerCloseButton.Enabled = true;
                        TransmitServerStartButton.Enabled = false;
                        TransmitServerStartButton.Enabled = false;
                        TransmitServerIPBox1.Enabled = false;
                        TransmitServerIPBox2.Enabled = false;
                        TransmitServerIPBox3.Enabled = false;
                        TransmitServerIPBox4.Enabled = false;
                        TransmitServerPortBox.Enabled = false;
                        break;
                }
            });

        }

        private void SerialStateChangeHandle(object? sender, bool e)
        {
            switch (OSCSerialPort.SerialState.Value)
            {
                case true:
                    OpenSerialPort.Enabled = false;
                    CloseSerialPort.Enabled = true;
                    HexSendButton.Enabled = true;
                    StrSendButton.Enabled = true;
                    PortNameBox.Enabled = false;
                    BaudRateBox.Enabled = false;
                    DataBitsBox.Enabled = false;
                    StopBitsBox.Enabled = false;
                    ParityBox.Enabled = false;
                    break;
                case false:
                    OpenSerialPort.Enabled = true;
                    CloseSerialPort.Enabled = false;
                    HexSendButton.Enabled = false;
                    StrSendButton.Enabled = false;
                    PortNameBox.Enabled = true;
                    BaudRateBox.Enabled = true;
                    DataBitsBox.Enabled = true;
                    StopBitsBox.Enabled = true;
                    ParityBox.Enabled = true;
                    break;
            }
        }

        private void OSCServerStateChangeHandle(object? sender, OneClientConnectiongOfServer.ConnectState e)
        {
            SafeOperateTools.SafeInvoke(this, obj =>
            {
                switch (e)
                {
                    case OneClientConnectiongOfServer.ConnectState.Disconnect:
                        ServerStateLight.BackColor = System.Drawing.Color.Red;
                        ServerCloseButton.Enabled = false;
                        ServerStartButton.Enabled = true;
                        ServerIPBox1.Enabled = true;
                        ServerIPBox2.Enabled = true;
                        ServerIPBox3.Enabled = true;
                        ServerIPBox4.Enabled = true;
                        ServerPortBox.Enabled = true;
                        break;
                    case OneClientConnectiongOfServer.ConnectState.WaitConnect:
                        ServerStateLight.BackColor = System.Drawing.Color.Blue;
                        ServerCloseButton.Enabled = true;
                        ServerStartButton.Enabled = false;
                        ServerIPBox1.Enabled = false;
                        ServerIPBox2.Enabled = false;
                        ServerIPBox3.Enabled = false;
                        ServerIPBox4.Enabled = false;
                        ServerPortBox.Enabled = false;
                        break;
                    case OneClientConnectiongOfServer.ConnectState.Connected:
                        ServerStateLight.BackColor = System.Drawing.Color.Green;
                        ServerCloseButton.Enabled = true;
                        ServerStartButton.Enabled = false;
                        ServerIPBox1.Enabled = false;
                        ServerIPBox2.Enabled = false;
                        ServerIPBox3.Enabled = false;
                        ServerIPBox4.Enabled = false;
                        ServerPortBox.Enabled = false;
                        break;
                }
            });
        }

        private void OSCClientStateChangeHandle(object? sender, bool e)
        {
            SafeOperateTools.SafeInvoke(this, obj =>
            {
                switch (e)
                {
                    case true:
                        ClientConnectButton.Enabled = false;
                        ClientDisconnectButton.Enabled = true;
                        ClientIPBox1.Enabled = false;
                        ClientIPBox2.Enabled = false;
                        ClientIPBox3.Enabled = false;
                        ClientIPBox4.Enabled = false;
                        ClientPortBox.Enabled = false;
                        break;
                    case false:
                        ClientConnectButton.Enabled = true;
                        ClientDisconnectButton.Enabled = false;
                        ClientIPBox1.Enabled = true;
                        ClientIPBox2.Enabled = true;
                        ClientIPBox3.Enabled = true;
                        ClientIPBox4.Enabled = true;
                        ClientPortBox.Enabled = true;
                        break;
                }
            });
        }

        private void OSCUDPStateChangeHandle(object? sender, bool e)
        {
            SafeOperateTools.SafeInvoke(this, obj =>
            {
                switch (e)
                {
                    case true:
                        UDPBindButton.Enabled = false;
                        UDPUnbindButton.Enabled = true;
                        UdpLocalIPBox1.Enabled = false;
                        UdpLocalIPBox2.Enabled = false;
                        UdpLocalIPBox3.Enabled = false;
                        UdpLocalIPBox4.Enabled = false;
                        UdpTargetIPBox1.Enabled = false;
                        UdpTargetIPBox2.Enabled = false;
                        UdpTargetIPBox3.Enabled = false;
                        UdpTargetIPBox4.Enabled = false;
                        UDPPortBox.Enabled = false;
                        break;
                    case false:
                        UDPBindButton.Enabled = true;
                        UDPUnbindButton.Enabled = false;
                        UdpLocalIPBox1.Enabled = true;
                        UdpLocalIPBox2.Enabled = true;
                        UdpLocalIPBox3.Enabled = true;
                        UdpLocalIPBox4.Enabled = true;
                        UdpTargetIPBox1.Enabled = true;
                        UdpTargetIPBox2.Enabled = true;
                        UdpTargetIPBox3.Enabled = true;
                        UdpTargetIPBox4.Enabled = true;
                        UDPPortBox.Enabled = true;
                        break;
                }
            });
        }

        //数据处理相关

        private void PortReceivDataHandle(object? sender, byte[] e)
        {
            try
            {
                var result = _receivePipeline.Process(e, BuildReceivePipelineOptions());
                var stopSend = SafeOperateTools.SafeRead(StopSendCheckBox, () => StopSendCheckBox.Checked);

                if (!stopSend &&
                    TransmitServerConnection.ServerConnectState.Value == OneClientConnectiongOfServer.ConnectState.Connected)
                {
                    _ = TransmitServerConnection.AsyncSend(result.ForwardData);
                }

                foreach (var frame in result.OscFrames)
                {
                    _plotQueue.Enqueue(frame);
                }

                if (string.IsNullOrEmpty(result.DisplayText))
                {
                    return;
                }

                SafeOperateTools.SafeInvoke(RecText, obj =>
                {
                    if (result.AppendNewLine)
                    {
                        obj.AppendText("\r\n");
                    }

                    obj.AppendText(result.DisplayText);
                });
            }
            catch (InvalidOperationException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "数据转换发生错误", ShowMassageTools.LogType.Error);
            }
            catch (ArgumentException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "数据转换发生错误", ShowMassageTools.LogType.Error);
            }
            catch (FormatException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "数据转换发生错误", ShowMassageTools.LogType.Error);
            }
        }

        private ReceivePipelineOptions BuildReceivePipelineOptions()
        {
            var stopReceive = SafeOperateTools.SafeRead(StopRec, () => StopRec.Checked);
            var enableOsc = SafeOperateTools.SafeRead(EnableOsc, () => EnableOsc.Checked);
            var showHex = SafeOperateTools.SafeRead(ShowHex, () => ShowHex.Checked);
            var appendNewLine = SafeOperateTools.SafeRead(RecNewLine, () => RecNewLine.Checked);
            var channelLength = SafeOperateTools.SafeRead(ChannelLength, () => decimal.ToInt32(ChannelLength.Value));
            var additionalBufferLength = SafeOperateTools.SafeRead(AdditionalBufferLengthBox, () => decimal.ToInt32(AdditionalBufferLengthBox.Value));

            return new ReceivePipelineOptions(
                stopReceive,
                enableOsc,
                showHex,
                appendNewLine,
                channelLength,
                additionalBufferLength,
                HeadStr,
                EndStr);
        }

        private void PlotRefreshTimerTick(object? sender, EventArgs e)
        {
            if (_plotQueue.IsEmpty)
            {
                return;
            }

            var frameUpdated = false;
            const int maxDrainCount = 1200;
            var drainCount = 0;

            while (drainCount < maxDrainCount && _plotQueue.TryDequeue(out var frame))
            {
                for (int ch = 0; ch < frame.Length && ch < loggers.Count; ch++)
                {
                    dataPoints[ch].Add(new DataPoint(TimeStamp, frame[ch]));
                    loggers[ch].Add(TimeStamp, frame[ch]);
                }

                TimeStamp++;
                frameUpdated = true;
                drainCount++;
            }

            if (frameUpdated)
            {
                formPlot1.Refresh();
            }

        }

        //串口相关

        private void SerialReceiveErrorHandle(object? sender, Exception e)
        {
            ShowMassageTools.ReportMassage(StateMassageTextBox, "接收发生错误，串口已关闭", ShowMassageTools.LogType.Error);
        }

        private void OpenSerialPort_Click(object sender, EventArgs e)
        {
            try
            {
                OSCSerialPort.SerialConfig.PortName = PortNameBox.Text;
                OSCSerialPort.SerialConfig.BaudRate = BaudRateBox.Text;
                OSCSerialPort.SerialConfig.DataBits = DataBitsBox.Text;
                OSCSerialPort.SerialConfig.StopBits = StopBitsBox.Text;
                OSCSerialPort.SerialConfig.Parity = ParityBox.Text;
                OSCSerialPort.Open();
                ShowMassageTools.ReportMassage(StateMassageTextBox, "串口已开启", ShowMassageTools.LogType.Success);
            }
            catch
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "串口打开失败", ShowMassageTools.LogType.Error);
            }
        }

        private void CloseSerialPort_Click(object sender, EventArgs e)
        {
            try
            {
                OSCSerialPort.Close();
                ShowMassageTools.ReportMassage(StateMassageTextBox, "串口已关闭", ShowMassageTools.LogType.Success);
            }
            catch
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "串口繁忙，无法关闭", ShowMassageTools.LogType.Error);
            }

        }

        //作服务器相关

        private void ServerStartButton_Click(object sender, EventArgs e)
        {
            try
            {
                OSCServerConnection.StartServer(
                    $"{ServerIPBox1.Text}.{ServerIPBox2.Text}.{ServerIPBox3.Text}.{ServerIPBox4.Text}", ServerPortBox.IntValue);
                ShowMassageTools.ReportMassage(StateMassageTextBox, "服务器启动成功", ShowMassageTools.LogType.Success);
            }
            catch (SocketException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "服务器启动失败，端口已被占用", ShowMassageTools.LogType.Warning);
            }
            catch (Exception ex)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "服务器启动错误:" + ex.Message, ShowMassageTools.LogType.Error);
            }
        }

        private async void ServerCloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                await OSCServerConnection.StopServer();
                ShowMassageTools.ReportMassage(StateMassageTextBox, "服务器已关闭", ShowMassageTools.LogType.Success);
            }
            catch (NullReferenceException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "无可关闭的连接", ShowMassageTools.LogType.Warning);
            }
            catch (Exception ex)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "服务器关闭错误:" + ex.Message, ShowMassageTools.LogType.Error);
            }
        }

        private void OSCServerSendErrorHandle(object? sender, Exception e)
        {
            SafeOperateTools.SafeInvoke(StopSendCheckBox, obj => obj.Checked = true);
            if (e is OperationCanceledException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "发送任务被关闭", ShowMassageTools.LogType.Info);
            }
            else if (e is SocketException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "客户端断开连接", ShowMassageTools.LogType.Warning);
            }
            else
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "发送异常：" + e.Message, ShowMassageTools.LogType.Error);
            }
        }

        private void OSCServerReceiveErrorHandle(object? sender, Exception e)
        {
            if (e is OperationCanceledException || e is ObjectDisposedException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "服务器监听进程被关闭", ShowMassageTools.LogType.Info);
            }
            else if (e is SocketException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "客户端断开连接", ShowMassageTools.LogType.Warning);
            }
            else
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "服务器：" + e.Message, ShowMassageTools.LogType.Error);
            }
        }


        //作客户端相关

        private async void ClientConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                await OSCClient.Connect($"{ClientIPBox1.Text}.{ClientIPBox2.Text}.{ClientIPBox3.Text}.{ClientIPBox4.Text}", ClientPortBox.IntValue);
                ShowMassageTools.ReportMassage(StateMassageTextBox, "已连接", ShowMassageTools.LogType.Success);
                ClientConnectButton.Enabled = false;
                ClientDisconnectButton.Enabled = true;
            }
            catch (TimeoutException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "连接超时", ShowMassageTools.LogType.Error);
            }
            catch (Exception ex)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "连接错误:" + ex.Message, ShowMassageTools.LogType.Error);
            }
        }

        private async void ClientDisconnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                await OSCClient.Disconnect();
                ShowMassageTools.ReportMassage(StateMassageTextBox, "连接已关闭", ShowMassageTools.LogType.Success);
                ClientConnectButton.Enabled = true;
                ClientDisconnectButton.Enabled = false;
            }
            catch (Exception ex)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "关闭连接错误:" + ex.Message, ShowMassageTools.LogType.Error);
            }
        }

        private void OSCClientSendErrorHandle(object? sender, Exception e)
        {
            if (e is OperationCanceledException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "发送任务被关闭", ShowMassageTools.LogType.Info);
            }
            else if (e is SocketException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "服务器断开连接", ShowMassageTools.LogType.Warning);
            }
            else
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "发送异常：" + e.Message, ShowMassageTools.LogType.Error);
            }
        }

        private void OSCClientReceiveErrorHandle(object? sender, Exception e)
        {
            if (e is OperationCanceledException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "客户端监听进程被关闭", ShowMassageTools.LogType.Info);
            }
            else if (e is ObjectDisposedException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "客户端监听进程被关闭", ShowMassageTools.LogType.Info);
            }
            else if (e is SocketException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "服务器断开连接", ShowMassageTools.LogType.Warning);
            }
            else
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "客户端：" + e.Message, ShowMassageTools.LogType.Error);
            }
        }
        //UDP相关

        private void UDPBindButton_Click(object sender, EventArgs e)
        {
            try
            {
                OSCUDPEndpoint.Bind($"{UdpLocalIPBox1.Text}.{UdpLocalIPBox2.Text}.{UdpLocalIPBox3.Text}.{UdpLocalIPBox4.Text}", UDPPortBox.IntValue);
                OSCUDPEndpoint.SetRemote($"{UdpTargetIPBox1.Text}.{UdpTargetIPBox2.Text}.{UdpTargetIPBox3.Text}.{UdpTargetIPBox4.Text}", UDPPortBox.IntValue);
                ShowMassageTools.ReportMassage(StateMassageTextBox, "UDP本地端已绑定，监听已启用", ShowMassageTools.LogType.Success);
            }
            catch (Exception ex)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "端口绑定错误:" + ex.Message, ShowMassageTools.LogType.Error);
            }
        }

        private async void UDPUnbindButton_Click(object sender, EventArgs e)
        {
            try
            {
                await OSCUDPEndpoint.UnBind();
                ShowMassageTools.ReportMassage(StateMassageTextBox, "UDP本地端已解绑", ShowMassageTools.LogType.Success);
            }
            catch (Exception ex)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "端口解绑失败" + ex.Message, ShowMassageTools.LogType.Error);
            }
        }

        private void OSCUDPSendErrorHandle(object? sender, Exception e)
        {
            if (e is OperationCanceledException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "UDP发送被取消", ShowMassageTools.LogType.Info);
            }
            else
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "发送异常：" + e.Message, ShowMassageTools.LogType.Error);
            }
        }

        private void OSCUDPReceiveErrorHandle(object? sender, Exception e)
        {
            if (e is OperationCanceledException || e is ObjectDisposedException || e is SocketException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "UDP监听进程被关闭", ShowMassageTools.LogType.Info);
            }
            else
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "UDP：" + e.Message, ShowMassageTools.LogType.Error);
            }
        }


        //发送相关

        private async void HexSendButton_Click(object sender, EventArgs e)
        {
            try
            {
                string[] HexStr = HexSendText.Text.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                byte[] ByteStr = [.. HexStr.Select(h => Convert.ToByte(h, 16))];
                try
                {
                    await SendBytes(ByteStr);
                }
                catch (Exception ex)
                {
                    ShowMassageTools.ReportMassage(StateMassageTextBox, "发送失败" + ex.Message, ShowMassageTools.LogType.Error);
                }
            }
            catch
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "HEX字符串转换失败，请检查输入", ShowMassageTools.LogType.Error);
            }

        }

        private async void StrSendButton_Click(object sender, EventArgs e)
        {
            byte[] ByteStr = [.. StrSendText.Text.Select(h => Convert.ToByte(h))];
            try
            {
                await SendBytes(ByteStr);
            }
            catch (Exception ex)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "发送失败" + ex.Message, ShowMassageTools.LogType.Error);
            }
        }

        private async Task SendBytes(byte[] data)
        {
            if (ReceiveModeSelectText.Text == "SerialPort")
            {
                OSCSerialPort.Write(data, 0, data.Length);
            }
            if (ReceiveModeSelectText.Text == "TCP(Server)")
            {
                await OSCServerConnection.AsyncSend(data);
            }
            if (ReceiveModeSelectText.Text == "TCP(Client)")
            {
                await OSCClient.SendAsync(data);
            }
            if (ReceiveModeSelectText.Text == "UDP")
            {
                OSCUDPEndpoint.Send(data);
            }
        }

        //数据转发服务器相关

        private async void TransmitServerReceivedMassageSend(object? sender, byte[] e)
        {
            byte[] ByteStr = e;
            if (!StopReceiveCheckBox.Checked)
            {
                try
                {
                    await SendBytes(ByteStr);
                }
                catch
                {
                    StopReceiveCheckBox.Checked = true;
                    ShowMassageTools.ReportMassage(StateMassageTextBox, "接收转发出现异常,已暂停", ShowMassageTools.LogType.Error);
                }
            }
        }

        private void TransmitServerSendErrorHandle(object? sender, Exception e)
        {
            if (e is OperationCanceledException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "发送任务被关闭", ShowMassageTools.LogType.Info);
            }
            else if (e is SocketException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "转发客户端断开连接", ShowMassageTools.LogType.Warning);
            }
            else
            {
                SafeOperateTools.SafeInvoke(StopSendCheckBox, obj => obj.Checked = true);
                ShowMassageTools.ReportMassage(StateMassageTextBox, "转发出现异常，已暂停：" + e.Message, ShowMassageTools.LogType.Error);
            }
        }

        private void TransmitServerReceiveErrorHandle(object? sender, Exception e)
        {
            if (e is OperationCanceledException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "转发服务器监听进程被关闭", ShowMassageTools.LogType.Info);
            }
            else if (e is ObjectDisposedException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "转发服务器监听进程被关闭", ShowMassageTools.LogType.Info);
            }
            else if (e is SocketException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "转发客户端断开连接", ShowMassageTools.LogType.Warning);
            }
            else
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "转发服务器：" + e.Message, ShowMassageTools.LogType.Error);
            }
        }

        private void TransmitServerStartButton_Click(object sender, EventArgs e)
        {
            try
            {
                TransmitServerConnection.StartServer(
                    $"{TransmitServerIPBox1.Text}.{TransmitServerIPBox2.Text}.{TransmitServerIPBox3.Text}.{TransmitServerIPBox4.Text}", TransmitServerPortBox.IntValue);
                ShowMassageTools.ReportMassage(StateMassageTextBox, "转发服务器启动成功", ShowMassageTools.LogType.Success);
            }
            catch (SocketException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "转发服务器启动失败，端口已被占用", ShowMassageTools.LogType.Warning);
            }
            catch (Exception ex)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "转发服务器启动错误:" + ex.Message, ShowMassageTools.LogType.Error);
            }
        }

        private async void TransmitServerCloseButton_Click(object sender, EventArgs e)
        {
            try
            {
                await TransmitServerConnection.StopServer();
                ShowMassageTools.ReportMassage(StateMassageTextBox, "转发服务器已关闭", ShowMassageTools.LogType.Success);
            }
            catch (NullReferenceException)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "无可关闭的连接", ShowMassageTools.LogType.Warning);
            }
            catch (Exception ex)
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "转发服务器关闭错误:" + ex.Message, ShowMassageTools.LogType.Error);
            }
        }

        //数据保存相关

        private void SavePlot_Click(object sender, EventArgs e)
        {
            string SaveFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveData");
            Directory.CreateDirectory(SaveFolderPath);
            if (FileNameText.Text != "")
            {
                try
                {
                    DialogResult result = DialogResult.Yes;
                    if (File.Exists(Path.Combine(SaveFolderPath, FileNameText.Text + FileTypeText.Text)))
                    {
                        result = MessageBox.Show("已经存在同名文件，是否覆盖", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    }
                    if (result == DialogResult.Yes)
                    {
                        switch (FileTypeText.Text)
                        {
                            case ".csv":
                                {
                                    string filePath = Path.Combine(SaveFolderPath, FileNameText.Text + FileTypeText.Text);
                                    using var writer = new StreamWriter(filePath);
                                    // 输出 CSV 表头
                                    var header = new List<string> { "Time" };
                                    for (int i = 0; i < dataPoints.Count; i++)
                                    {
                                        header.Add($"CH{i + 1}");
                                    }
                                    writer.WriteLine(string.Join(",", header));

                                    int rowCount = dataPoints.Min(list => list.Count);
                                    // 输出数据行
                                    for (int i = 0; i < rowCount; i++)
                                    {
                                        var row = new List<string> { dataPoints[0][i].X.ToString() };  // 时间戳
                                        row.AddRange(dataPoints.Select(dp => dp[i].Y.ToString()));  // 每个通道的数据
                                        writer.WriteLine(string.Join(",", row));
                                    }
                                }
                                ;
                                break;
                            default: formPlot1.Plot.SaveFig(Path.Combine(SaveFolderPath, FileNameText.Text + FileTypeText.Text)); break;
                        }
                        MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                }
                catch
                {
                    MessageBox.Show("保存数据失败:", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("文件名不能为空:", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _plotRefreshTimer.Stop();
                _plotRefreshTimer.Dispose();
                _receiveEndpointHub.Dispose();

                SaveConfigTools.SaveConfig<ConfigData>(new ConfigData
                {
                    PortName = PortNameBox.Text,
                    BaudRate = BaudRateBox.Text,
                    DataBits = DataBitsBox.Text,
                    StopBits = StopBitsBox.Text,
                    Parity = ParityBox.Text,
                    OSCHeadText = HeadTextBox.Text,
                    OSCEndText = EndTextBox.Text,
                    OSCTransmitIP = $"{TransmitServerIPBox1.Text}.{TransmitServerIPBox2.Text}.{TransmitServerIPBox3.Text}.{TransmitServerIPBox4.Text}.{TransmitServerPortBox.Text}",
                    OSCTCPServerIP = $"{ServerIPBox1.Text}.{ServerIPBox2.Text}.{ServerIPBox3.Text}.{ServerIPBox4.Text}.{ServerPortBox.Text}",
                    OSCTCPClientIP = $"{ClientIPBox1.Text}.{ClientIPBox2.Text}.{ClientIPBox3.Text}.{ClientIPBox4.Text}.{ClientPortBox.Text}",
                    OSCUDPLocalIP = $"{UdpLocalIPBox1.Text}.{UdpLocalIPBox2.Text}.{UdpLocalIPBox3.Text}.{UdpLocalIPBox4.Text}.{UDPPortBox.Text}",
                    OSCUDPTargetIP = $"{UdpTargetIPBox1.Text}.{UdpTargetIPBox2.Text}.{UdpTargetIPBox3.Text}.{UdpTargetIPBox4.Text}.{UDPPortBox.Text}"
                }, "DefaultConfig.json");
            }
            catch
            {
                DialogResult result = MessageBox.Show("保存设置信息失败，是否直接退出", "错误",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (result == DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        void LoadConfig(string FileName)
        {
            var OSCConfig = SaveConfigTools.LoadConfig<ConfigData>(FileName);
            if (OSCConfig != null)
            {
                try
                {
                    if (FillInConfig(OSCConfig))
                    {
                        ShowMassageTools.ReportMassage(StateMassageTextBox, "配置加载成功", ShowMassageTools.LogType.Success);
                    }
                    else
                    {
                        ShowMassageTools.ReportMassage(StateMassageTextBox, "配置仅部分加载", ShowMassageTools.LogType.Warning);
                    }
                }
                catch
                {
                    ShowMassageTools.ReportMassage(StateMassageTextBox, "配置加载失败", ShowMassageTools.LogType.Warning);
                }
            }
        }

        bool FillInConfig(ConfigData Config)
        {
            PortNameBox.Text = Config.PortName;
            BaudRateBox.Text = Config.BaudRate;
            DataBitsBox.Text = Config.DataBits;
            StopBitsBox.Text = Config.StopBits;
            ParityBox.Text = Config.Parity;
            HeadTextBox.Text = Config.OSCHeadText;
            EndTextBox.Text = Config.OSCEndText;
            if (string.IsNullOrWhiteSpace(Config.OSCTransmitIP))
            {
                return false;
            }
            string[] Strs = Config.OSCTransmitIP.Split('.');
            TransmitServerIPBox1.Text = Strs[0];
            TransmitServerIPBox2.Text = Strs[1];
            TransmitServerIPBox3.Text = Strs[2];
            TransmitServerIPBox4.Text = Strs[3];
            TransmitServerPortBox.Text = Strs[4];

            if (string.IsNullOrWhiteSpace(Config.OSCTCPServerIP))
            {
                return false;
            }
            Strs = Config.OSCTCPServerIP.Split('.');
            ServerIPBox1.Text = Strs[0];
            ServerIPBox2.Text = Strs[1];
            ServerIPBox3.Text = Strs[2];
            ServerIPBox4.Text = Strs[3];
            ServerPortBox.Text = Strs[4];

            if (string.IsNullOrWhiteSpace(Config.OSCTCPClientIP))
            {
                return false;
            }
            Strs = Config.OSCTCPClientIP.Split('.');
            ClientIPBox1.Text = Strs[0];
            ClientIPBox2.Text = Strs[1];
            ClientIPBox3.Text = Strs[2];
            ClientIPBox4.Text = Strs[3];
            ClientPortBox.Text = Strs[4];

            if (string.IsNullOrWhiteSpace(Config.OSCUDPLocalIP))
            {
                return false;
            }
            Strs = Config.OSCUDPLocalIP.Split('.');
            UdpLocalIPBox1.Text = Strs[0];
            UdpLocalIPBox2.Text = Strs[1];
            UdpLocalIPBox3.Text = Strs[2];
            UdpLocalIPBox4.Text = Strs[3];
            UDPPortBox.Text = Strs[4];

            if (string.IsNullOrWhiteSpace(Config.OSCUDPTargetIP))
            {
                return false;
            }
            Strs = Config.OSCUDPTargetIP.Split('.');
            UdpTargetIPBox1.Text = Strs[0];
            UdpTargetIPBox2.Text = Strs[1];
            UdpTargetIPBox3.Text = Strs[2];
            UdpTargetIPBox4.Text = Strs[3];
            UDPPortBox.Text = Strs[4];

            return true;
        }

        //示波器显示相关//
        private void ResetPlot()
        {
            formPlot1.Plot.Clear();
            formPlot1.Plot.SetAxisLimits(LeftLimit.DoubleValue, RightLimit.DoubleValue, ButtomLimit.DoubleValue, TopLimit.DoubleValue);
            loggers = [];
            for (int i = 0; i < ChannelLength.Value; i++)
            {
                var logger = formPlot1.Plot.AddDataLogger(label: $"Ch{i + 1}");
                loggers.Add(logger);
            }

            formPlot1.Plot.SetAxisLimits(0, 50, -10, 10);

            Hline = new ScottPlot.Plottable.HLine();
            Hline = formPlot1.Plot.AddHorizontalLine(0);
            Vline = new ScottPlot.Plottable.VLine();
            Vline = formPlot1.Plot.AddVerticalLine(10);

            Hline.DragEnabled = true;
            Hline.PositionLabel = true;
            Hline.Color = System.Drawing.Color.Red;
            Hline.PositionLabelBackground = Hline.Color;
            Vline.DragEnabled = true;
            Vline.PositionLabel = true;
            Vline.Color = System.Drawing.Color.Red;
            Vline.PositionLabelBackground = Vline.Color;
            TimeStamp = 0;

            dataPoints = [];
            for (int i = 0; i < ChannelLength.Value; i++)
            {
                dataPoints.Add([]);
            }

            foreach (var log in loggers)
                log.ManageAxisLimits = false;


            formPlot1.Plot.Legend();
            formPlot1.Refresh();
        }

        private void EnableOsc_CheckedChanged(object sender, EventArgs e)
        {
            if (EnableOsc.Checked)
            {
                try
                {
                    string[] HexStr;
                    HexStr = HeadTextBox.Text.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                    HeadStr = string.Concat(HexStr.Select(h => (Char)Convert.ToByte(h, 16)));
                    HexStr = EndTextBox.Text.Split([' '], StringSplitOptions.RemoveEmptyEntries);
                    EndStr = string.Concat(HexStr.Select(h => (Char)Convert.ToByte(h, 16)));
                    ResetPlot();
                    ChannelLength.Enabled = false;
                    HeadTextBox.Enabled = false;
                    EndTextBox.Enabled = false;
                    AdditionalBufferLengthBox.Enabled = false;
                }
                catch
                {
                    ShowMassageTools.ReportMassage(StateMassageTextBox, "帧头/帧尾转换失败，无法启用示波器，请检查输入格式", ShowMassageTools.LogType.Error);
                    EnableOsc.Checked = false;
                }
            }
            else
            {
                ChannelLength.Enabled = true;
                HeadTextBox.Enabled = true;
                EndTextBox.Enabled = true;
                AdditionalBufferLengthBox.Enabled = true;
            }
        }

        private void ClearData_Click(object sender, EventArgs e)
        {
            ResetPlot();
        }

        private void AxisLimitsBoxLeaveOperate()
        {
            if (TopLimit.DoubleValue < ButtomLimit.DoubleValue)
            {
                TopLimit.BackColor = System.Drawing.Color.Red;
                ButtomLimit.BackColor = System.Drawing.Color.Red;
                ShowMassageTools.ReportMassage(StateMassageTextBox, "示波器下界不能比上界高", ShowMassageTools.LogType.Error);
                return;
            }
            else if (RightLimit.DoubleValue < LeftLimit.DoubleValue)
            {
                LeftLimit.BackColor = System.Drawing.Color.Red;
                RightLimit.BackColor = System.Drawing.Color.Red;
                ShowMassageTools.ReportMassage(StateMassageTextBox, "示波器下界不能比上界高", ShowMassageTools.LogType.Error);
                return;
            }
            foreach (var log in loggers)
                log.ManageAxisLimits = true;
            formPlot1.Plot.SetAxisLimits(LeftLimit.DoubleValue, RightLimit.DoubleValue, ButtomLimit.DoubleValue, TopLimit.DoubleValue);
            foreach (var log in loggers)
                log.ManageAxisLimits = false;
            TopLimit.BackColor = System.Drawing.Color.White;
            ButtomLimit.BackColor = System.Drawing.Color.White;
            LeftLimit.BackColor = System.Drawing.Color.White;
            RightLimit.BackColor = System.Drawing.Color.White;
            formPlot1.Refresh();
        }

        private void TopLimit_Leave(object sender, EventArgs e)
        {
            AxisLimitsBoxLeaveOperate();
        }

        private void ButtomLimit_Leave(object sender, EventArgs e)
        {
            AxisLimitsBoxLeaveOperate();
        }

        private void LeftLimit_Leave(object sender, EventArgs e)
        {
            AxisLimitsBoxLeaveOperate();
        }

        private void RightLimit_Leave(object sender, EventArgs e)
        {
            AxisLimitsBoxLeaveOperate();
        }

        private void YStep_Leave(object sender, EventArgs e)
        {
            formPlot1.Plot.YAxis.TickDensity(YStep.DoubleValue);
            formPlot1.Refresh();
        }

        private void XStep_Leave(object sender, EventArgs e)
        {
            formPlot1.Plot.XAxis.TickDensity(XStep.DoubleValue);
            formPlot1.Refresh();
        }

        private void ClearRec_Click(object sender, EventArgs e)
        {
            RecText.Clear();
        }

        private void FormsPlot1_AxesChanged(object sender, EventArgs e)
        {
            TopLimit.DoubleValue = formPlot1.Plot.GetAxisLimits().YMax;
            ButtomLimit.DoubleValue = formPlot1.Plot.GetAxisLimits().YMin;
            LeftLimit.DoubleValue = formPlot1.Plot.GetAxisLimits().XMin;
            RightLimit.DoubleValue = formPlot1.Plot.GetAxisLimits().XMax;
        }

        //工具栏相关
        private void OpenHexToCharTool_Click(object sender, EventArgs e)
        {
            HexToCharToolForm ??= new Form_HexToChar();
            HexToCharToolForm.Show();
        }

        private void ReadConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog.Filter = "JSON配置文件 (*.json)|*.json";
            OpenFileDialog.DefaultExt = ".json";
            OpenFileDialog.Multiselect = false;
            OpenFileDialog.ShowDialog();
            if (OpenFileDialog.FileName != "")
            {
                LoadConfig(OpenFileDialog.FileName);
            }
        }

        private void SaveConfigToAnother_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog.Filter = "JSON配置文件 (*.json)|*.json";
                SaveFileDialog.DefaultExt = ".json";
                SaveFileDialog.AddExtension = false;
                SaveFileDialog.ShowDialog();
                SaveConfigTools.SaveConfig<ConfigData>(new ConfigData
                {
                    PortName = PortNameBox.Text,
                    BaudRate = BaudRateBox.Text,
                    DataBits = DataBitsBox.Text,
                    StopBits = StopBitsBox.Text,
                    Parity = ParityBox.Text,
                    OSCHeadText = HeadTextBox.Text,
                    OSCEndText = EndTextBox.Text,
                    OSCTransmitIP = $"{TransmitServerIPBox1.Text}.{TransmitServerIPBox2.Text}.{TransmitServerIPBox3.Text}.{TransmitServerIPBox4.Text}.{TransmitServerPortBox.Text}",
                    OSCTCPServerIP = $"{ServerIPBox1.Text}.{ServerIPBox2.Text}.{ServerIPBox3.Text}.{ServerIPBox4.Text}.{ServerPortBox.Text}",
                    OSCTCPClientIP = $"{ClientIPBox1.Text}.{ClientIPBox2.Text}.{ClientIPBox3.Text}.{ClientIPBox4.Text}.{ClientPortBox.Text}",
                    OSCUDPLocalIP = $"{UdpLocalIPBox1.Text}.{UdpLocalIPBox2.Text}.{UdpLocalIPBox3.Text}.{UdpLocalIPBox4.Text}.{UDPPortBox.Text}",
                    OSCUDPTargetIP = $"{UdpTargetIPBox1.Text}.{UdpTargetIPBox2.Text}.{UdpTargetIPBox3.Text}.{UdpTargetIPBox4.Text}.{UDPPortBox.Text}"
                }, SaveFileDialog.FileName);
                ShowMassageTools.ReportMassage(StateMassageTextBox, "配置文件保存成功", ShowMassageTools.LogType.Success);
            }
            catch
            {
                ShowMassageTools.ReportMassage(StateMassageTextBox, "配置文件保存失败", ShowMassageTools.LogType.Error);
            }
        }

        private void OpenHelp_Click(object sender, EventArgs e)
        {

            Process.Start(new ProcessStartInfo
            {
                FileName = "help\\index.html",
                UseShellExecute = true
            });
        }
    }

    public class ConfigData
    {
        public string? PortName { get; set; }
        public string? BaudRate { get; set; }
        public string? DataBits { get; set; }
        public string? StopBits { get; set; }
        public string? Parity { get; set; }
        public string? OSCHeadText { get; set; }
        public string? OSCEndText { get; set; }
        public string? OSCTransmitIP { get; set; }
        public string? OSCTCPClientIP { get; set; }
        public string? OSCTCPServerIP { get; set; }
        public string? OSCUDPLocalIP { get; set; }
        public string? OSCUDPTargetIP { get; set; }

    }
}
