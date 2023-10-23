
using System.Net.WebSockets;
using System.Text;

int counter = 1;
Console.Title = "Client";

using var ws = new ClientWebSocket();

await ws.ConnectAsync(new Uri("ws://localhost:5001/ws"), CancellationToken.None);

byte[] buffer = new byte[1024];

while (ws.State == WebSocketState.Open)
{
    if (ws.State != WebSocketState.None)
    {
        var result = await ws.ReceiveAsync(buffer, CancellationToken.None);

        if (result.MessageType == WebSocketMessageType.Close)
        {
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
            Console.WriteLine(result.CloseStatusDescription);
        }
        else
        {
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine("\tID Data:  " + counter++);
            Console.WriteLine(message);
            Thread.Sleep(2000);
        }
    }
}


Console.ReadKey();
