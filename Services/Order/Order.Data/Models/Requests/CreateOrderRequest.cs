namespace OrderService.Data.Models.Requests;

public class CreateOrderRequest
{
    public Guid VehicleId { get; set; }
    public List<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();
}