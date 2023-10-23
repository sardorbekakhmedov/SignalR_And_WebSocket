using WebSocketSharp;
using WebSocketSharp.Server;

namespace Console.WebSocket.Server;

public class Echo : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        System.Console.WriteLine($"Received message from client: {e.Data}");
        Send(e.Data);
    }
}