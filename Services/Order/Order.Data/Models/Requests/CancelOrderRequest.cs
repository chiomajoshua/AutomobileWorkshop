namespace OrderService.Data.Models.Requests;

public class CancelOrderRequest
{
    public Guid OrderId { get; set; }
    public string CancellationReason { get; set; } = null;
}