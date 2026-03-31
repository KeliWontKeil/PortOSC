namespace PortOSC.Transport;

public interface IReceiveEndpoint
{
    event EventHandler<byte[]>? DataReceived;
}

public sealed class ReceiveEndpointHub : IDisposable
{
    private readonly List<IReceiveEndpoint> _endpoints = [];

    public event EventHandler<byte[]>? DataReceived;

    public void Register(IReceiveEndpoint endpoint)
    {
        ArgumentNullException.ThrowIfNull(endpoint);

        if (_endpoints.Contains(endpoint))
        {
            return;
        }

        endpoint.DataReceived += EndpointOnDataReceived;
        _endpoints.Add(endpoint);
    }

    public void Unregister(IReceiveEndpoint endpoint)
    {
        ArgumentNullException.ThrowIfNull(endpoint);

        if (!_endpoints.Remove(endpoint))
        {
            return;
        }

        endpoint.DataReceived -= EndpointOnDataReceived;
    }

    public void Dispose()
    {
        foreach (var endpoint in _endpoints)
        {
            endpoint.DataReceived -= EndpointOnDataReceived;
        }

        _endpoints.Clear();
    }

    private void EndpointOnDataReceived(object? sender, byte[] e)
    {
        DataReceived?.Invoke(sender, e);
    }
}
