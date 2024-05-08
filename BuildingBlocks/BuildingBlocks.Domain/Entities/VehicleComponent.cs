namespace BuildingBlocks.Domain.Entities;

public class VehicleComponent : TrackedEntity
{
    public Guid VehicleId { get; set; }
    public Guid ComponentId { get; set; }
    public virtual Vehicle Vehicle { get; set; } = null!;
    public virtual Component Component { get; set; } = null!;
}