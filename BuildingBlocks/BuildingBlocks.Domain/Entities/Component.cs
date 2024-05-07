using BuildingBlocks.Domain.Enumerators;

namespace BuildingBlocks.Domain.Entities;

public class Component : TrackedEntity
{
    public ComponentType ComponentType { get; set; }
    public int QuantityAvailable { get; set; }
    public DateTimeOffset LastModifiedDate { get; set; } = DateTimeOffset.Now;
    public virtual ICollection<ProductionQueue> ProductionQueues { get; set; } = new List<ProductionQueue>();
    public virtual ICollection<AssemblyQueue> AssemblyQueues { get; set; } = new List<AssemblyQueue>();
}