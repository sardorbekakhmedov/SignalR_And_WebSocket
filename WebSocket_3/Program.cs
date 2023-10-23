using Microsoft.EntityFrameworkCore;
using WebSocket.Server_3.Context;
using WebSocket.Server_3.Entities;
using WebSocket.Server_3.MiddleWares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(config =>
{
    AppContext.SetSwitch(switchName: "Npgsql.EnableLegacyTimestampBehavior", isEnabled: true);
    config.UseSnakeCaseNamingConvention()
        // .UseInMemoryDatabase("sales_db");
        .UseNpgsql("Host=localhost; Port=5432; Database=postgres; Username=postgres; Password=123");
});

/*builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("https://localhost:5000")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});*/

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWebSockets();

app.UseCors(options =>
{
    options.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
});

app.UseWebSocketMap(new Order());  // CustomWebSocketMap

/*app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

        while (true)
        {
            // var now = DateTime.UtcNow;

            var dataObject =
                "{\r\n  \"User\": {\r\n    " +
                "\"Id\": 0,\r\n    " +
                "\"Name\": \"string\",\r\n   " +
                " \"Orders\": [\r\n     " +
                " {\r\n        " +
                "\"Id\": 0,\r\n        " +
                "\"UserId\": 0,\r\n        " +
                "\"ProductName\": \"string\",\r\n       " +
                " \"Amount\": 0\r\n     " +
                " }\r\n   " +
                " ]\r\n " +
                " }\r\n" +
                "}\r\n";

            byte[] data = Encoding.UTF8.GetBytes($"{dataObject}");

            //await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);

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

            //await Task.Delay(1000);

            //long randomNumber = new Random().Next(0, 10);

            //if (randomNumber != 7)
            //    continue;

            //await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Random closing",
            //    CancellationToken.None);

            //return;
        }
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});*/


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
