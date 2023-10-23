using Microsoft.AspNetCore.Mvc;
using WebSocket.Api_2.WebSocketServices;

namespace WebSocket.Api_2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly ChatWebSocketHandler _webSocketHandler;

    public ChatController(ChatWebSocketHandler webSocketHandler)
    {
        _webSocketHandler = webSocketHandler;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            await _webSocketHandler.HandleWebSocket(webSocket);
        }
        else
        {
            return BadRequest("WebSocket is not supported!");
        }

        return Ok();
    }
}