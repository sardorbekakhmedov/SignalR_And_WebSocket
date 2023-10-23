namespace WebSocket.Server_3.Entities;

public class Order
{
    public uint Id { get; set; }
    public uint UserId { get; set; }
    public string ProductName { get; set; } = null!;
    public uint Amount { get; set; }

    public virtual User? User { get; set; }
}