using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSocket.Server_3.Context;
using WebSocket.Server_3.Entities;

namespace WebSocket.Server_3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public UsersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> Create(string userName)
    {
        var user = new User
        {
            Name = userName
        };

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return Created("Created", new { Id = user.Id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _dbContext.Users.Include(x => x.Orders).ToListAsync());
    }
}