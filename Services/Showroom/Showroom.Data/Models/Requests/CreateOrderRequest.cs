namespace Showroom.Data.Models.Requests;

public class CreateOrderRequest
{
    public List<OrderItemRequest> OrderItems { get; set; } = new List<OrderItemRequest>();
}