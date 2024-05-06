namespace BuildingBlocks.Domain.Entities;

public class Vehicle : TrackedEntity
{
    public string Model { get; set; } = null!;
    public int QuantityAvailable { get; set; }
}