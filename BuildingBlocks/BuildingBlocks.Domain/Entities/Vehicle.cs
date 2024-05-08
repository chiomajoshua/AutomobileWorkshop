using BuildingBlocks.Domain.Enumerators;

namespace BuildingBlocks.Domain.Entities;

public class Vehicle : TrackedEntity
{
    public string Manufacturer { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public VehicleType VehicleType { get; set; }    
    public int QuantityAvailable { get; set; }
    public decimal Price { get; set; }
    public virtual ICollection<VehicleComponent> VehicleComponents { get; set; } = new List<VehicleComponent>();
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}