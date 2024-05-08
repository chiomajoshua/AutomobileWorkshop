using BuildingBlocks.Domain.Enumerators;

namespace BuildingBlocks.Domain.Entities;

public class Order : TrackedEntity
{
    public Guid CustomerId { get; set; }    
    public OrderStatus OrderStatus { get; set; }
    public Guid VehicleId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset LastModifiedDate { get; set; } = DateTimeOffset.Now;
    public virtual Customer Customer { get; set; } = null!;
    public virtual ICollection<OrderItem> OrderItems { get; set;} = new List<OrderItem>();
    public virtual Vehicle Vehicle { get; set; } = null!;
}