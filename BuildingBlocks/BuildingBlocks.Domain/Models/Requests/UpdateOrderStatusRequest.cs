using BuildingBlocks.Domain.Enumerators;

namespace BuildingBlocks.Domain.Models.Requests;

public class UpdateOrderStatusRequest
{
    public Guid OrderId { get; set; }
    public OrderStatus OrderStatus { get; set; }
}
