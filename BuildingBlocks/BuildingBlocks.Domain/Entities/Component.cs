using BuildingBlocks.Domain.Enumerators;

namespace BuildingBlocks.Domain.Entities;

public class Component : TrackedEntity
{
    public ComponentType ComponentType { get; set; }
    public int QuantityAvailable { get; set; }
    public DateTimeOffset LastModifiedDate { get; set; } = DateTimeOffset.Now;
}