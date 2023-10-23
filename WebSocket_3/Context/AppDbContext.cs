using Microsoft.EntityFrameworkCore;
using WebSocket.Server_3.Entities;

namespace WebSocket.Server_3.Context;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
}