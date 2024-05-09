using BuildingBlocks.Domain.Enumerators;

namespace BuildingBlocks.Domain.Entities;

public class Component : TrackedEntity
{
    public string Manufacturer { get; set; } = null!;
    public ComponentType ComponentType { get; set; }
    public int QuantityAvailable { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset LastModifiedDate { get; set; } = DateTimeOffset.Now;
}