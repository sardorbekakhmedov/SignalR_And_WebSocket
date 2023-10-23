using System.Net.WebSockets;
using WebSocket.Server_3.Entities;

namespace WebSocket.Server_3.Services;

public class WebSocketOrderHandler
{
    private readonly IConfiguration _configuration;
    private Action<Order>? _action = null;

    public WebSocketOrderHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Start(Action<Order> action, Order order)
    {
        _action = action;

        _action(order);
    }


    public async Task Stop(System.Net.WebSockets.WebSocket webSocket)
    {
        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Working Stop() Action", CancellationToken.None);
    }
}