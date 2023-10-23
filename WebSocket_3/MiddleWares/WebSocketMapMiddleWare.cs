using Newtonsoft.Json;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using WebSocket.Server_3.Entities;

namespace WebSocket.Server_3.MiddleWares;

public static class WebSocketMapMiddleWare
{
    public static IApplicationBuilder UseWebSocketMap(this IApplicationBuilder app, Order order)
    {
        return app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/ws")
            {
                await SendMessageAsync(context, order);
            }
            else
            {
                await next();
            }
        });
    }

    public static async Task SendMessageAsync(HttpContext httpContext, Order order)
    {
        if (httpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();

            var dataObject = JsonConvert.SerializeObject(order);

            byte[] data = Encoding.UTF8.GetBytes($"{dataObject}");

            await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);

            if (webSocket.State != WebSocketState.None)
            {
                await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
            }
            //else
            //{
            //    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Random closing",
            //        CancellationToken.None);
            //}

            /*  while (true)
              {
                  //var now = DateTime.UtcNow;
                  //byte[] data = Encoding.UTF8.GetBytes($"{now}");

                  var dataObject = JsonConvert.SerializeObject(order);

                  //var dataObject =
                  //    "{\r\n  \"User\": {\r\n    \"Id\": 0,\r\n    \"Name\": \"string\",\r\n    \"Orders\": [\r\n     {\r\n        " +
                  //    "\"Id\": 0,\r\n        " +
                  //    "\"UserId\": 0,\r\n        " +
                  //    "\"ProductName\": \"string\",\r\n       " +
                  //    " \"Amount\": 0\r\n     }\r\n   ]\r\n  }\r\n}\r\n";

                  byte[] data = Encoding.UTF8.GetBytes($"{dataObject}");

                  await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);

                  if (webSocket.State != WebSocketState.None)
                  {
                      await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
                  }
                  else
                  {
                      await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Random closing",
                          CancellationToken.None);

                      break;
                  }

                  //  await Task.Delay(1000);

                  //long randomNumber = new Random().Next(0, 10);

                  //if (randomNumber != 7)
                  //    continue;

                  //await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Random closing",
                  //    CancellationToken.None);

                  //return;
              }*/
        }
        else
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}