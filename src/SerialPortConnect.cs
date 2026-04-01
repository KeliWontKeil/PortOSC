using System.IO.Ports;
using PortOSC.Transport;
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
    public class SerialPortSource(SerialConfig? config = default) : IReceiveEndpoint, IDisposable
    {
        private SerialPort? _serialPort;
        private Task? _receiveTask;
        private CancellationTokenSource? _receiveCts;
        private volatile bool _closing;

        public SerialConfig SerialConfig { get; set; } = config ?? new SerialConfig();
        public ChangeEventValue<bool> SerialState { get; set; } = new(false);

        public event EventHandler<byte[]>? RawDataReceived;
        public event EventHandler<Exception>? ReceiveErrorOccurred;
        public event EventHandler<byte[]>? DataReceived
        {
            add => RawDataReceived += value;
            remove => RawDataReceived -= value;
        }

        private static void ConfigurePort(SerialPort serialPort, SerialConfig serialConfig)
        {
            if (string.IsNullOrWhiteSpace(serialConfig.PortName))
                throw new InvalidOperationException("Serial port name is not set.");

            if (!int.TryParse(serialConfig.BaudRate, out var baudRate) || baudRate <= 0)
                throw new InvalidOperationException("Invalid serial baud rate.");

            if (!int.TryParse(serialConfig.DataBits, out var dataBits) || dataBits <= 0)
                throw new InvalidOperationException("Invalid serial data bits.");

            serialPort.PortName = serialConfig.PortName;
            serialPort.BaudRate = baudRate;
            serialPort.DataBits = dataBits;

            serialPort.StopBits = serialConfig.StopBits switch
            {
                "1" => StopBits.One,
                "1.5" => StopBits.OnePointFive,
                "2" => StopBits.Two,
                _ => throw new InvalidOperationException("Invalid serial stop bits.")
            };

            serialPort.Parity = serialConfig.Parity switch
            {
                "奇校验" => Parity.Odd,
                "偶校验" => Parity.Even,
                "无" => Parity.None,
                _ => throw new InvalidOperationException("Invalid serial parity.")
            };
        }

        private void StartReceiveLoop(SerialPort serialPort)
        {
            _receiveCts = new CancellationTokenSource();
            _receiveTask = Task.Run(() => ReceiveLoopAsync(serialPort, _receiveCts.Token));
        }

        private async Task ReceiveLoopAsync(SerialPort serialPort, CancellationToken token)
        {
            var buffer = new byte[4096];

            try
            {
                while (!token.IsCancellationRequested)
                {
                    if (!serialPort.IsOpen)
                    {
                        break;
                    }

                    var available = serialPort.BytesToRead;
                    if (available <= 0)
                    {
                        await Task.Delay(10, token).ConfigureAwait(false);
                        continue;
                    }

                    var count = Math.Min(available, buffer.Length);
                    var bytesRead = serialPort.Read(buffer, 0, count);
                    if (bytesRead <= 0)
                    {
                        continue;
                    }

                    var data = new byte[bytesRead];
                    Array.Copy(buffer, data, bytesRead);
                    RawDataReceived?.Invoke(this, data);
                }
            }
            catch (OperationCanceledException) when (token.IsCancellationRequested || _closing)
            {
            }
            catch (ObjectDisposedException) when (_closing || token.IsCancellationRequested)
            {
            }
            catch (IOException ex) when (!_closing)
            {
                ReceiveErrorOccurred?.Invoke(this, ex);
            }
            catch (UnauthorizedAccessException ex) when (!_closing)
            {
                ReceiveErrorOccurred?.Invoke(this, ex);
            }
            catch (InvalidOperationException ex) when (!_closing)
            {
                ReceiveErrorOccurred?.Invoke(this, ex);
            }
            finally
            {
                SerialState.Value = false;
                _closing = false;
            }
        }

        public void Open()
        {
            if (_serialPort?.IsOpen == true)
                throw new InvalidOperationException("Serial port already opened.");

            if (_receiveTask != null && !_receiveTask.IsCompleted)
                throw new InvalidOperationException("Serial port is closing.");

            var serialPort = new SerialPort();
            ConfigurePort(serialPort, SerialConfig);

            serialPort.Open();
            _serialPort = serialPort;
            _closing = false;
            StartReceiveLoop(serialPort);
            SerialState.Value = true;
        }

        public void Close()
        {
            var serialPort = _serialPort;
            if (serialPort == null)
            {
                SerialState.Value = false;
                return;
            }

            _closing = true;
            _receiveCts?.Cancel();

            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
            }
            finally
            {
                serialPort.Dispose();
                _serialPort = null;
                _receiveCts?.Dispose();
                _receiveCts = null;
                SerialState.Value = false;
            }
        }

        public Task CloseAsync()
        {
            Close();
            return Task.CompletedTask;
        }

        private SerialPort GetOpenSerialPort()
        {
            if (_serialPort is not { IsOpen: true } serialPort)
                throw new InvalidOperationException("Serial port is not open.");

            return serialPort;
        }

        public void Write(string text) => GetOpenSerialPort().Write(text);
        public void Write(byte[] buffer, int offset, int count) => GetOpenSerialPort().Write(buffer, offset, count);
        public void Write(char[] buffer, int offset, int count) => GetOpenSerialPort().Write(buffer, offset, count);

        public void Dispose()
        {
            Close();

            GC.SuppressFinalize(this);
        }

    }

}