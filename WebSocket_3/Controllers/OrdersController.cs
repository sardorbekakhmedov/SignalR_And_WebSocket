using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSocket.Server_3.Context;
using WebSocket.Server_3.Entities;
using WebSocket.Server_3.MiddleWares;

namespace WebSocket.Server_3.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly HttpContext _httpContext;

    public OrdersController(AppDbContext dbContext, HttpContext httpContext)
    {
        _dbContext = dbContext;
        _httpContext = httpContext;
    }

    [HttpPost]
    public async Task<IActionResult> Create(string productName, uint amount)
    {
        var order = new Order
        {
            ProductName = productName,
            Amount = amount
        };

        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();

        await WebSocketMapMiddleWare.SendMessageAsync(_httpContext, order);

        return Created("Created", new { Id = order.Id });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _dbContext.Orders.Include(x => x.User).ToListAsync());
    }
}