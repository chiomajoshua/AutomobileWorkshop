namespace BuildingBlocks.Domain.Entities
{
    public class WarehouseOrder : TrackedEntity
    {
        public Guid OrderId { get; set; }
        public Guid InventoryId { get; set; }
        public DateTimeOffset DateCreated { get; set; } = DateTimeOffset.Now;
    }
}