﻿using System.Net.WebSockets;
using System.Text;

namespace WebSocket.Server_1.WebSocketManager;

public class WebSocketMessageHandler : SocketHandler
{
    public WebSocketMessageHandler(ConnectionManager connections) : base(connections)
    { }

    public override async Task OnConnected(System.Net.WebSockets.WebSocket socket)
    {
        await base.OnConnected(socket);
        var socketId = Connections.GetId(socket);
        await SendMessageToAll($"{socketId} just joined the party *****");
    }

    public override async Task Receive(System.Net.WebSockets.WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
    {
        var socketId = Connections.GetId(socket);

        var message = $"{socketId} said: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";
        await SendMessageToAll(message);
    }
}