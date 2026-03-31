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

        // Kept as the shared connection state reset path so connect, disconnect, and fault handling stay aligned.
        private void ResetClientState()
        {
            _ClientListen_cts?.Cancel();
            _ClientListen_cts?.Dispose();
            _ClientListen_cts = null;

            _stream?.Dispose();
            _stream = null;

            _client?.Dispose();
            _client = null;

            ClientConnectState.Value = false;
        }

        private void StartClientListen()
        {
            _ClientListen_cts = new CancellationTokenSource();
            ClientListenTaskHandle = Task.Run(() => ClientListenTask(_ClientListen_cts.Token));
        }

        private async Task ConnectCoreAsync(IPAddress address, int port, int timeoutMs)
        {
            if (Connected)
                throw new InvalidOperationException("Already connected.");

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(timeoutMs);

            ResetClientState();
            _client = new TcpClient();

            using var timeoutCts = new CancellationTokenSource(timeoutMs);

            try
            {
                await _client.ConnectAsync(address, port, timeoutCts.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested)
            {
                _client.Dispose();
                _client = null;
                throw new TimeoutException("Connect timeout.");
            }

            _stream = _client.GetStream();
            StartClientListen();
            ClientConnectState.Value = true;
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
            catch (OperationCanceledException) when (token.IsCancellationRequested)
            {
            }
            catch (ObjectDisposedException) when (token.IsCancellationRequested)
            {
            }
            catch (Exception ex)
            {
                ResetClientState();
                ReceiveErrorOccurred?.Invoke(this, ex);
            }
            finally
            {
                ClientConnectState.Value = false;
            }
        }

        public async Task Connect(int timeoutMs = 1000)
        {
            if (ToConnectServerIPaddress == null || ToConnectServerPort == 0)
                throw new InvalidOperationException("Invaild parameter.");

            await ConnectCoreAsync(ToConnectServerIPaddress, ToConnectServerPort, timeoutMs).ConfigureAwait(false);
        }

        public async Task Connect(string IP, int Port, int timeoutMs = 1000)
        {
            ToConnectServerIPaddress = IPAddress.Parse(IP);
            ToConnectServerPort = Port;

            await ConnectCoreAsync(ToConnectServerIPaddress, ToConnectServerPort, timeoutMs).ConfigureAwait(false);
        }

        public async Task Disconnect()
        {
            _ClientListen_cts?.Cancel();
            _stream?.Close();
            _client?.Close();

            if (ClientListenTaskHandle != null)
            {
                await ClientListenTaskHandle.ConfigureAwait(false);
                ClientListenTaskHandle.Dispose();
                ClientListenTaskHandle = null;
            }

            ResetClientState();
        }

        public async Task SendAsync(byte[] data, CancellationToken token = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(data);

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
            ResetClientState();
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

        // Keeps server lifecycle changes concentrated so start/stop and fault recovery share the same cleanup path.
        private void ResetServerState()
        {
            _ServerListen_cts?.Cancel();
            _ServerListen_cts?.Dispose();
            _ServerListen_cts = null;

            ServerConnectedClient?.Dispose();
            ServerConnectedClient = null;

            Server?.Close();
            Server = null;

            ServerConnectState.Value = ConnectState.Disconnect;
        }

        private void StartServerListen()
        {
            _ServerListen_cts = new CancellationTokenSource();
            ServerListenTaskHandle = Task.Run(() => ServerListenTask(_ServerListen_cts.Token), _ServerListen_cts.Token);
        }

        private void StartServerCore(string ip, int port)
        {
            if (Server != null)
                throw new InvalidOperationException("Server already started.");

            ServerIPAddress = ip;
            ServerPort = port;
            Server = new SimpleTcpServer(ServerIPAddress, ServerPort);
            Server.Start();
            StartServerListen();
        }

        private async Task ServerListenTask(CancellationToken cancellationtoken)
        {
            while (!cancellationtoken.IsCancellationRequested)
            {
                try
                {
                    ServerConnectState.Value = ConnectState.WaitConnect;
                    var acceptTask = Server!.AcceptAsync();
                    var completed = await Task.WhenAny(acceptTask, Task.Delay(Timeout.Infinite, cancellationtoken));
                    if (completed != acceptTask)
                    {
                        break;
                    }

                    ServerConnectedClient = await acceptTask;
                    ServerConnectState.Value = ConnectState.Connected;

                    while (!cancellationtoken.IsCancellationRequested)
                    {
                        var recdata = await ServerConnectedClient.ReceiveAsync(8192, cancellationtoken);
                        ReceiveMassage?.Invoke(this, recdata);
                    }
                }
                catch (OperationCanceledException) when (cancellationtoken.IsCancellationRequested)
                {
                    break;
                }
                catch (ObjectDisposedException) when (cancellationtoken.IsCancellationRequested)
                {
                    break;
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
                ArgumentNullException.ThrowIfNull(data);

                if (ServerConnectState.Value == ConnectState.Connected && ServerConnectedClient?.Connected == true)
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
            StartServerCore(IP, Port);
        }

        public void StartServer()
        {
            if (ServerIPAddress == null || ServerPort == 0)
                throw new InvalidOperationException("Invaild parameter.");

            StartServerCore(ServerIPAddress, ServerPort);
        }

        public async Task StopServer()
        {
            _ServerListen_cts?.Cancel();
            ServerConnectedClient?.Close();
            Server?.Close();

            if (ServerListenTaskHandle != null)
            {
                await ServerListenTaskHandle.ConfigureAwait(false);
                ServerListenTaskHandle.Dispose();
                ServerListenTaskHandle = null;
            }

            ResetServerState();
        }
    }
}
