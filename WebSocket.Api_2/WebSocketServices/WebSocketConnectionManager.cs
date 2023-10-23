using System.Collections.Concurrent;

namespace WebSocket.Api_2.WebSocketServices;


public class WebSocketConnectionManager
{
    private readonly ConcurrentDictionary<Guid, System.Net.WebSockets.WebSocket> _sockets = new();

    public Guid AddSocket(System.Net.WebSockets.WebSocket socket)
    {
        var socketId = Guid.NewGuid();

        _sockets.TryAdd(socketId, socket);

        return socketId;
    }

    public System.Net.WebSockets.WebSocket? GetSocket(Guid socketId)
    {
        _sockets.TryGetValue(socketId, out var socket);
        return socket;
    }

    public ConcurrentDictionary<Guid, System.Net.WebSockets.WebSocket> GetAllSockets()
    {
        return _sockets;
    }

    public Guid? GetSocketId(System.Net.WebSockets.WebSocket socket)
    {
        foreach (var (key, value) in _sockets)
        {
            if (value == socket)
            {
                return key;
            }
        }
        return null;
    }

    public void RemoveSocket(Guid socketId)
    {
        _sockets.TryRemove(socketId, out _);
    }

}

