using System.Net.WebSockets;

namespace WebSocket.Server_1.WebSocketManager;

public class SocketMiddleware
{
    private readonly RequestDelegate _next;
    private readonly SocketHandler _handler;

    public SocketMiddleware(RequestDelegate next, SocketHandler handler)
    {
        _next = next;
        _handler = handler;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.WebSockets.IsWebSocketRequest)
            return;

        var socket = await context.WebSockets.AcceptWebSocketAsync();

        await _handler.OnConnected(socket);

        // ReSharper disable once AsyncVoidLambda
        await Receive(socket, async (result, buffer) =>
        {
            if (result.MessageType == WebSocketMessageType.Text)
            {
                await _handler.Receive(socket, result, buffer);
            }
            else if (result.MessageType == WebSocketMessageType.Close)
            {
                await _handler.OnDisConnected(socket);
            }
        });
    }

    private async Task Receive(System.Net.WebSockets.WebSocket socket, Action<WebSocketReceiveResult, byte[]> messageHandler)
    {
        var buffer = new byte[1024 * 4];

        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            messageHandler(result, buffer);
        }
    }
}