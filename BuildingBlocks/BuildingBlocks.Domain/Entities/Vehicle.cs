namespace BuildingBlocks.Domain.Entities;

public class Vehicle : TrackedEntity
{
    public string Manufacturer { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }   
    public int QuantityAvailable { get; set; }
    public decimal Price { get; set; }
}