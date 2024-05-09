using BuildingBlocks.Domain.Enumerators;

namespace BuildingBlocks.Domain.Entities;

public class Inventory : TrackedEntity
{
    public Guid VehicleId { get; set; }
    public AvailabilityStatus AvailabilityStatus { get; set; }
    public string Location { get; set; } = null!;
    public virtual Vehicle Vehicle { get; set; } = null!;
}
