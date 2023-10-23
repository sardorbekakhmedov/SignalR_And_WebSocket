namespace WebSocket.Server_3.Entities;

public class User
{
    public uint Id { get; set; }
    public string Name { get; set; } = null!;

    public virtual ICollection<Order>? Orders { get; set; }
}