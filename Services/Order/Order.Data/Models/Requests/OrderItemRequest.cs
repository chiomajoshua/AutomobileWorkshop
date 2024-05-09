namespace OrderService.Data.Models.Requests;

public class OrderItemRequest
{
    public Guid ComponentId { get; set; }
    public decimal PricePerUnit { get; set; }
}