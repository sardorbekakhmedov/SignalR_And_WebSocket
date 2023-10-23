using System.Net.WebSockets;
using System.Text;

namespace WebSocket.Api_2.WebSocketServices;

public class ChatWebSocketHandler
{
    private readonly WebSocketConnectionManager _connectionManager;
    private readonly ILogger<ChatWebSocketHandler> _logger;

    public ChatWebSocketHandler(WebSocketConnectionManager connectionManager, ILogger<ChatWebSocketHandler> logger)
    {
        _connectionManager = connectionManager;
        _logger = logger;
    }

    public async Task HandleWebSocket(System.Net.WebSockets.WebSocket webSocket)
    {
        var socketId = _connectionManager.AddSocket(webSocket);

        _logger.LogInformation($"WebSocket connection established with ID  {socketId}");

        while (webSocket.State == WebSocketState.Open)
        {
            var message = await ReceiveMessageAsync(webSocket);

            if (message != null)
            {
                _logger.LogInformation($"Received message from ID  {socketId}");
                await BroadcastMessageAsync(message);
            }
        }

        _connectionManager.RemoveSocket(socketId);
        _logger.LogInformation($"WebSocket connection established with ID  {socketId}");
    }


    private async Task<string?> ReceiveMessageAsync(System.Net.WebSockets.WebSocket webSocket)
    {
        var buffer = new byte[1024];

        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        return result.CloseStatus.HasValue ? null : Encoding.UTF8.GetString(buffer, 0, result.Count);
    }

    private async Task BroadcastMessageAsync(string message)
    {
        foreach (var socket in _connectionManager.GetAllSockets())
        {
            if (socket.Value.State == WebSocketState.Open)
            {
                var msg = Encoding.UTF8.GetBytes(message);

                await socket.Value.SendAsync(msg, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }

}