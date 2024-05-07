namespace Showroom.Data.Models.Requests;

public class OrderItemRequest
{
    public Guid ComponentId { get; set; }
    public int Quantity { get; set; }
}