using System.IO.Ports;
using Tools;

namespace _SerialPortSource
{
    public class SerialConfig
    {
        public string? PortName { get; set; }
        public string? BaudRate { get; set; }
        public string? DataBits { get; set; }
        public string? StopBits { get; set; }
        public string? Parity { get; set; }
    }
    public class SerialPortSource
    {
        private readonly SerialPort _SerialPort;
        public SerialConfig SerialConfig { get; set; }
        public ChangeEventValue<bool> SerialState { get; set; }


        public event EventHandler<byte[]>? RawDataReceived;
        public event EventHandler<Exception>? ReceiveErrorOccurred;

        public SerialPortSource(SerialConfig? config = default)
        {
            _SerialPort = new SerialPort();
            SerialState = new ChangeEventValue<bool>(false);
            SerialConfig = new SerialConfig();
            if (config != null)
                SerialConfig = config;
        }

        public void Open()
        {
            _SerialPort.PortName = SerialConfig.PortName;
            _SerialPort.BaudRate = int.Parse(SerialConfig.BaudRate!);
            _SerialPort.DataBits = int.Parse(SerialConfig.DataBits!);
            switch (SerialConfig.StopBits)
            {
                case "1": _SerialPort.StopBits = StopBits.One; break;
                case "1.5": _SerialPort.StopBits = StopBits.OnePointFive; break;
                case "2": _SerialPort.StopBits = StopBits.Two; break;
            }
            switch (SerialConfig.Parity)
            {
                case "奇校验": _SerialPort.Parity = Parity.Odd; break;
                case "偶校验": _SerialPort.Parity = Parity.Even; break;
                case "无": _SerialPort.Parity = Parity.None; break;
            }
            SerialState.Value = true;
            _SerialPort.Open();
            _SerialPort.DataReceived += SerialPort_DataReceived;
        }

        public void Close()
        {
            SerialState.Value = false;
            _SerialPort.Close();
            _SerialPort.DataReceived -= SerialPort_DataReceived;
        }

        public void Write(string text) => _SerialPort.Write(text);
        public void Write(byte[] buffer, int offset, int count) => _SerialPort.Write(buffer, offset, count);
        public void Write(char[] buffer, int offset, int count) => _SerialPort.Write(buffer, offset, count);

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = _SerialPort.BytesToRead;
                byte[] buf = new byte[n];
                _SerialPort.Read(buf, 0, n);

                RawDataReceived?.Invoke(this, buf);
            }
            catch (Exception ex)
            {
                try
                {
                    Close();
                }
                catch
                { }
                finally
                {
                    ReceiveErrorOccurred?.Invoke(this, ex);
                }
            }
        }

    }

}