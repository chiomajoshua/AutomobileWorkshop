namespace BuildingBlocks.Domain.Entities;

public class OrderItem : TrackedEntity
{
    public Guid OrderId { get; set; }
    public Guid ComponentId { get; set; }
    public int Quantity { get; set; }
    public decimal PricePerUnit {  get; set; }
    public virtual Order Order { get; set; } = null!;
}