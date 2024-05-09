namespace OrderService.Data.Models.Requests;

public class CreateOrderRequest
{
    public Guid VehicleId { get; set; }
    public decimal TotalPrice { get; set; }
    public int Quantity { get; set; }
    public List<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();
}