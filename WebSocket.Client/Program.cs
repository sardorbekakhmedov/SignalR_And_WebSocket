using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;

if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length <= 2)
{
    Process.Start("WebSocket.Client.exe");
}

StartWebSocket().GetAwaiter().GetResult();

static async Task StartWebSocket()
{
    using var client = new ClientWebSocket();
    await client.ConnectAsync(new Uri("ws://localhost:5001/ws"), CancellationToken.None);
    Console.WriteLine($"Web socket connection established @{DateTime.UtcNow:F}");

    var send = Task.Run(async () =>
    {
        var message = Console.ReadLine();

        while (!string.IsNullOrEmpty(message))
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            await client?.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true,
                CancellationToken.None);
        }

        await client?.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
    });

    var receive = ReceiveAsync(client);

    await Task.WhenAll(send, receive);
}

static async Task ReceiveAsync(ClientWebSocket client)
{
    var buffer = new byte[1024 * 4];

    while (true)
    {
        var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, result.Count));

        if (result.MessageType == WebSocketMessageType.Close)
        {
            await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            break;
        }
    }
}
