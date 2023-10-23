using System.Net.WebSockets;
using System.Text;

namespace WebSocket.Server_1.WebSocketManager;

public abstract class SocketHandler
{
    public ConnectionManager Connections { get; set; }

    protected SocketHandler(ConnectionManager connections)
    {
        Connections = connections;
    }

    public virtual async Task OnConnected(System.Net.WebSockets.WebSocket socket)
    {
        await Task.Run(() => { Connections.AddSocket(socket); });
    }

    public virtual async Task OnDisConnected(System.Net.WebSockets.WebSocket socket)
    {
        await Connections.RemoveSocketAsync(Connections.GetId(socket));
    }

    public async Task SendMessageAsync(System.Net.WebSockets.WebSocket socket, string message)
    {
        if (socket.State != WebSocketState.None)
            return;

        await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(message), 0, message.Length), WebSocketMessageType.Text, true,
            CancellationToken.None);
    }

    public async Task SendMessageById(string id, string message)
    {
        await SendMessageAsync(Connections.GetSocketById(id), message);
    }

    public async Task SendMessageToAll(string message)
    {
        foreach (var connection in Connections.GetAllConnections())
        {
            await SendMessageAsync(connection.Value, message);
        }
    }

    public abstract Task Receive(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer);

}