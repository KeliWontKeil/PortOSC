using System.Net;
using System.Net.Sockets;
using PortOSC.Transport;
using Tools;

namespace UdpConnect
{
    public sealed class SimpleUdpEndpoint : IDisposable, IReceiveEndpoint
    {
        public string? RemoteIPaddress { get; set; }
        public int RemotetPort { get; set; }

        private Socket? _udpSocket;
        public IPEndPoint? LocalEndPoint { get; private set; }
        private Task? ReceiveTaskHandle;
        private CancellationTokenSource? _Receive_cts;

        public string? LocalIPAddress { get; set; }
        public int LocalPort { get; private set; }
        public string? RemoteIPAddress { get; private set; }
        public int RemotePort { get; private set; }

        public ChangeEventValue<bool> UdpState { get; }

        public event EventHandler<byte[]>? ReceiveMessage;
        public event EventHandler<Exception>? ReceiveErrorOccurred;
        public event EventHandler<Exception>? SendErrorOccurred;
        public event EventHandler<byte[]>? DataReceived
        {
            add => ReceiveMessage += value;
            remove => ReceiveMessage -= value;
        }

        private void ResetSocketState()
        {
            _Receive_cts?.Cancel();
            _Receive_cts?.Dispose();
            _Receive_cts = null;

            if (_udpSocket != null)
            {
                _udpSocket.Dispose();
                _udpSocket = null;
            }

            ReceiveTaskHandle = null;
            UdpState.Value = false;
        }

        private void StartReceiveLoop()
        {
            _Receive_cts = new CancellationTokenSource();
            ReceiveTaskHandle = Task.Run(() => ReceiveTask(_Receive_cts.Token));
        }

        public SimpleUdpEndpoint(string? IP = default, int Port = default)
        {
            UdpState = new ChangeEventValue<bool>(false);
            if (IP != null)
                LocalIPAddress = IP;
            if (Port != 0)
                LocalPort = Port;
        }

        public void Bind(string? IP = default, int Port = default)
        {
            if (_udpSocket != null)
                throw new InvalidOperationException("UDP already started.");

            if (IP != null)
                LocalIPAddress = IP;

            if (Port != 0)
                LocalPort = Port;

            if (LocalIPAddress == null || LocalPort == 0)
            {
                throw new InvalidOperationException("Local IP address or port is not set.");
            }

            _udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _udpSocket.Bind(new IPEndPoint(IPAddress.Parse(LocalIPAddress), LocalPort));

            if (_udpSocket.LocalEndPoint == null)
            {
                throw new InvalidOperationException("Failed to bind UDP socket.");
            }

            LocalEndPoint = (IPEndPoint)_udpSocket.LocalEndPoint;

            StartReceiveLoop();
            UdpState.Value = true;
        }

        public async Task UnBind()
        {
            if (_udpSocket == null)
                return;

            _Receive_cts?.Cancel();
            _udpSocket.Close();

            if (ReceiveTaskHandle != null)
            {
                await ReceiveTaskHandle;
            }

            ResetSocketState();
        }
        public void Dispose()
        {
            ResetSocketState();
        }

        public void SetRemote(string IP, int Port)
        {
            RemoteIPAddress = IP;
            RemotePort = Port;
        }

        private async Task ReceiveTask(CancellationToken token)
        {
            var buffer = new byte[8192];

            while (!token.IsCancellationRequested)
            {
                try
                {
                    EndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

                    var result = await _udpSocket!.ReceiveFromAsync(
                        new ArraySegment<byte>(buffer),
                        SocketFlags.None,
                        remoteEP);

                    var receiveDataLength = result.ReceivedBytes;

                    if (receiveDataLength > 0)
                    {
                        var data = new byte[receiveDataLength];
                        Array.Copy(buffer, data, receiveDataLength);

                        ReceiveMessage?.Invoke(this, data);
                    }
                }
                catch (OperationCanceledException) when (token.IsCancellationRequested)
                {
                    break;
                }
                catch (ObjectDisposedException) when (token.IsCancellationRequested)
                {
                    break;
                }
                catch (SocketException ex)
                {
                    ReceiveErrorOccurred?.Invoke(this, ex);
                    break;
                }
                catch (InvalidOperationException ex)
                {
                    ReceiveErrorOccurred?.Invoke(this, ex);
                    break;
                }
            }
        }

        public void Send(byte[] data)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(data);

                if (_udpSocket == null)
                    throw new InvalidOperationException("UDP socket not started.");
                if (RemoteIPAddress == null || RemotePort == 0)
                {
                    throw new InvalidOperationException("Illegal remote address or port.");
                }

                var remoteEP = new IPEndPoint(IPAddress.Parse(RemoteIPAddress), RemotePort);

                _udpSocket.SendTo(data, remoteEP);
            }
            catch (SocketException ex)
            {
                SendErrorOccurred?.Invoke(this, ex);
            }
            catch (InvalidOperationException ex)
            {
                SendErrorOccurred?.Invoke(this, ex);
            }
        }
    }
}