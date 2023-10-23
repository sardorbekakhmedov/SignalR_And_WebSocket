using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace WebSocket.Server_1.WebSocketManager;

public class ConnectionManager
{
    private readonly ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> _conntections = new();

    public void AddSocket(System.Net.WebSockets.WebSocket socket)
    {
        _conntections.TryAdd(GetConnectionId(), socket);
    }

    public System.Net.WebSockets.WebSocket GetSocketById(string id)
    {
        return _conntections.FirstOrDefault(x => x.Key == id).Value;
    }

    public ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> GetAllConnections()
    {
        return _conntections;
    }

    public string GetId(System.Net.WebSockets.WebSocket socket)
    {
        return _conntections.FirstOrDefault(x => x.Value == socket).Key;
    }

    public async Task RemoveSocketAsync(string id)
    {
        _conntections.TryRemove(id, out var socket);

        if (socket is not null)
        {
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Socket connection closed", CancellationToken.None);
        }
    }

    public string GetConnectionId()
    {
        return Guid.NewGuid().ToString("N");
    }


}