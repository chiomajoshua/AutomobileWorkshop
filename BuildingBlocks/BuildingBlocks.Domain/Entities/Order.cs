using BuildingBlocks.Domain.Enumerators;

namespace BuildingBlocks.Domain.Entities;

public class Order : TrackedEntity
{
    public Guid CustomerId { get; set; }
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Placed;
    public Guid VehicleId { get; set; }   
    public decimal TotalPrice { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset LastModifiedDate { get; set; } = DateTimeOffset.Now;
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}