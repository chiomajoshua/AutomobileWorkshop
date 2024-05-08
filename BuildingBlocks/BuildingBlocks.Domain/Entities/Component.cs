using BuildingBlocks.Domain.Enumerators;

namespace BuildingBlocks.Domain.Entities;

public class Component : TrackedEntity
{
    public string Manufacturer { get; set; } = null!;
    public ComponentType ComponentType { get; set; }
    public int QuantityAvailable { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset LastModifiedDate { get; set; } = DateTimeOffset.Now;
    public virtual ICollection<VehicleComponent> VehicleComponents { get; set; } = new List<VehicleComponent>();
    public virtual ICollection<ProductionQueue> ProductionQueues { get; set; } = new List<ProductionQueue>();
    public virtual ICollection<AssemblyQueue> AssemblyQueues { get; set; } = new List<AssemblyQueue>();
}