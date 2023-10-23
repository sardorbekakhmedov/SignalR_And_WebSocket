using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SignalRController : ControllerBase
{
    private readonly HubConnectionContext _hubConnectionContest;

    public SignalRController(HubConnectionContext hubConnectionContest)
    {
        _hubConnectionContest = hubConnectionContest;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok("Salom web api");
    }
}