using Microsoft.AspNetCore.SignalR;

namespace SignalR.Server.Hubs;

public class TestHub : Hub
{
    public async Task SendMessage(string message)
    {
        var connectionId = Context.ConnectionId;
        await Clients.Others.SendAsync("Send",
            $"\nUser connectionId: {connectionId}" +
            $",\nMessage:  {message}");
    }
}