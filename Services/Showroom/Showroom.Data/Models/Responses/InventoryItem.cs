namespace ShowroomService.Data.Models.Responses;

public class InventoryItem<T>
{
    public T? Item { get; set; }
    public int Quantity { get; set; }
}