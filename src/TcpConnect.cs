using System.Net;
using System.Net.Sockets;
using PortOSC.Transport;
using Tools;

namespace TcpConnect
{
    public sealed class SimpleTcpClient : IDisposable, IReceiveEndpoint
    {
        public int ToConnectServerPort { get; set; }
        public IPAddress? ToConnectServerIPaddress { get; set; }

        private Task? ClientListenTaskHandle;
        private CancellationTokenSource? _ClientListen_cts;

        private TcpClient? _client;
        private NetworkStream? _stream;

        public ChangeEventValue<bool> ClientConnectState { get; }

        public event EventHandler<Exception>? ReceiveErrorOccurred;
        public event EventHandler<Exception>? SendErrorOccurred;
        public event EventHandler<byte[]>? ReceiveMassage;
        public event EventHandler<byte[]>? DataReceived
        {
            add => ReceiveMassage += value;
            remove => ReceiveMassage -= value;
        }

        public SimpleTcpClient(string? InputStrIPaddress = default, int InputPort = default)
        {
            if (InputStrIPaddress != null)
                ToConnectServerIPaddress = IPAddress.Parse(InputStrIPaddress);
            ToConnectServerPort = InputPort;
            ClientConnectState = new ChangeEventValue<bool>(false);
        }

        private bool Connected => _client?.Connected == true;

        private async Task ClientListenTask(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    var buffer = new byte[8192];
                    int bytes = await _stream!.ReadAsync(buffer, token);

                    if (bytes == 0)
                        throw new SocketException((int)SocketError.ConnectionReset);

                    var result = new byte[bytes];
                    Array.Copy(buffer, result, bytes);

                    ReceiveMassage?.Invoke(this, result);
                }
            }
            catch (Exception ex)
            {
                _client?.Close();
                _stream?.Close();
                ReceiveErrorOccurred?.Invoke(this, ex);
                ClientConnectState.Value = false;
            }
        }

        public async Task Connect(int timeoutMs = 1000)
        {
            if (Connected)
                throw new InvalidOperationException("Already connected.");

            _client = new TcpClient();
            var cts = new CancellationTokenSource(timeoutMs);

            if (ToConnectServerIPaddress == null || ToConnectServerPort == 0)
                throw new InvalidOperationException("Invaild parameter.");

            var connectTask = _client.ConnectAsync(ToConnectServerIPaddress, ToConnectServerPort);

            var completed = await Task.WhenAny(connectTask, Task.Delay(Timeout.Infinite, cts.Token));
            if (completed != connectTask)
                throw new TimeoutException("Connect timeout.");

            _stream = _client.GetStream();

            _ClientListen_cts = new CancellationTokenSource();
            ClientListenTaskHandle = Task.Run(() => ClientListenTask(_ClientListen_cts.Token));

            ClientConnectState.Value = true;
        }

        public async Task Connect(string IP, int Port, int timeoutMs = 1000)
        {
            if (Connected)
                throw new InvalidOperationException("Already connected.");

            ToConnectServerIPaddress = IPAddress.Parse(IP);
            ToConnectServerPort = Port;
            _client = new TcpClient();
            var cts = new CancellationTokenSource(timeoutMs);
            var connectTask = _client.ConnectAsync(ToConnectServerIPaddress, ToConnectServerPort);

            var completed = await Task.WhenAny(connectTask, Task.Delay(Timeout.Infinite, cts.Token));
            if (completed != connectTask)
                throw new TimeoutException("Connect timeout.");

            _stream = _client.GetStream();

            _ClientListen_cts = new CancellationTokenSource();
            ClientListenTaskHandle = Task.Run(() => ClientListenTask(_ClientListen_cts.Token));

            ClientConnectState.Value = true;
        }

        public async Task Disconnect()
        {
            _client?.Close();
            _stream?.Close();
            _ClientListen_cts?.Cancel();
            if (ClientListenTaskHandle != null)
            {
                await ClientListenTaskHandle;
            }
            ClientConnectState.Value = false;
        }

        public async Task SendAsync(byte[] data, CancellationToken token = default)
        {
            try
            {
                if (!Connected)
                    throw new InvalidOperationException("Not connected.");

                await _stream!.WriteAsync(data, token);
                await _stream.FlushAsync(token);
            }
            catch (Exception ex)
            {
                SendErrorOccurred?.Invoke(this, ex);
            }
        }

        public void Dispose()
        {
            _ClientListen_cts?.Cancel();
            _stream?.Dispose();
            _client?.Dispose();
        }
    }

    public class SimpleTcpServer
    {
        private readonly TcpListener _listener;

        public int ServerPort { get; }
        public IPAddress ServerIPaddress { get; }

        public SimpleTcpServer(string InputStrIPaddress, int InputPort)
        {
            ServerPort = InputPort;
            ServerIPaddress = IPAddress.Parse(InputStrIPaddress);
            _listener = new TcpListener(ServerIPaddress, ServerPort);
        }

        public void Start() => _listener.Start();

        public void Close() => _listener.Stop();

        public async Task<SimpleTcpConnectedClient> AcceptAsync()
        {
            var client = await _listener.AcceptTcpClientAsync();
            return new SimpleTcpConnectedClient(client);
        }

    }

    public sealed class SimpleTcpConnectedClient : IDisposable
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;

        public bool Connected => _client?.Connected == true;

        internal SimpleTcpConnectedClient(TcpClient client)
        {
            _client = client;
            _stream = client.GetStream();
        }

        public async Task SendAsync(byte[] data, CancellationToken token = default)
        {
            if (!Connected)
                throw new InvalidOperationException("Not connected.");

            await _stream.WriteAsync(data, token);
            await _stream.FlushAsync(token);
        }

        public async Task<byte[]> ReceiveAsync(int bufferSize = 8192, CancellationToken token = default)
        {
            if (!Connected)
                throw new InvalidOperationException("Not connected.");

            var buffer = new byte[bufferSize];
            int bytes = await _stream.ReadAsync(buffer, token);

            if (bytes == 0)
                throw new SocketException((int)SocketError.ConnectionReset);

            var result = new byte[bytes];
            Array.Copy(buffer, result, bytes);
            return result;
        }

        public void Close()
        {
            _stream?.Close();
            _client?.Close();
        }

        public void Dispose()
        {
            Close();
        }
    }

    public class OneClientConnectiongOfServer(string? IP = default, int Port = default) : IReceiveEndpoint
    {
        private SimpleTcpServer? Server;
        private Task? ServerListenTaskHandle;
        private CancellationTokenSource? _ServerListen_cts;
        private SimpleTcpConnectedClient? ServerConnectedClient;

        public enum ConnectState { Disconnect, Connected, WaitConnect }
        public ChangeEventValue<ConnectState> ServerConnectState { get; } = new ChangeEventValue<ConnectState>(ConnectState.Disconnect);
        public string? ServerIPAddress { get; set; } = IP;
        public int ServerPort { get; set; } = Port;

        public event EventHandler<Exception>? ReceiveErrorOccurred;
        public event EventHandler<Exception>? SendErrorOccurred;
        public event EventHandler<byte[]>? ReceiveMassage;
        public event EventHandler<byte[]>? DataReceived
        {
            add => ReceiveMassage += value;
            remove => ReceiveMassage -= value;
        }

        private async Task ServerListenTask(CancellationToken cancellationtoken)
        {
            while (!cancellationtoken.IsCancellationRequested)
            {
                try
                {
                    ServerConnectState.Value = ConnectState.WaitConnect;
                    ServerConnectedClient = await Server!.AcceptAsync();
                    ServerConnectState.Value = ConnectState.Connected;
                    while (!cancellationtoken.IsCancellationRequested)
                    {
                        var recdata = await ServerConnectedClient.ReceiveAsync(8192, cancellationtoken);
                        ReceiveMassage?.Invoke(this, recdata);
                    }
                }
                catch (Exception ex)
                {
                    ReceiveErrorOccurred?.Invoke(this, ex);
                }
            }
            ServerConnectState.Value = ConnectState.Disconnect;
        }

        public async Task AsyncSend(byte[] data)
        {
            try
            {
                if (ServerConnectState.Value == ConnectState.Connected)
                {
                    await ServerConnectedClient!.SendAsync(data);
                }
            }
            catch (Exception ex)
            {
                SendErrorOccurred?.Invoke(this, ex);
            }
        }

        public void StartServer(string IP, int Port)
        {
            ServerIPAddress = IP;
            ServerPort = Port;
            Server = new SimpleTcpServer(ServerIPAddress, ServerPort);
            _ServerListen_cts = new CancellationTokenSource();
            Server.Start();
            ServerListenTaskHandle = Task.Run(() => ServerListenTask(_ServerListen_cts.Token), _ServerListen_cts.Token);
        }
        public void StartServer()
        {
            if (ServerIPAddress == null || ServerPort == 0)
                throw new InvalidOperationException("Invaild parameter.");
            Server = new SimpleTcpServer(ServerIPAddress, ServerPort);
            _ServerListen_cts = new CancellationTokenSource();
            Server.Start();
            ServerListenTaskHandle = Task.Run(() => ServerListenTask(_ServerListen_cts.Token), _ServerListen_cts.Token);
        }

        public async Task StopServer()
        {
            Server?.Close();
            ServerConnectedClient?.Dispose();
            _ServerListen_cts?.Cancel();
            if (ServerListenTaskHandle != null)
            {
                await ServerListenTaskHandle;
                ServerListenTaskHandle.Dispose();
            }
            ServerConnectState.Value = ConnectState.Disconnect;
        }
    }
}
