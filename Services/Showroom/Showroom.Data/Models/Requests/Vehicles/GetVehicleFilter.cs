using BuildingBlocks.Domain.Enumerators;

namespace ShowroomService.Data.Models.Requests.Vehicles;

public class GetVehicleFilter
{
    public VehicleType[] VehicleType { get; set; } = null;
    public ComponentType[] ComponentType { get; set; } = null;
    public string Manufacturer { get; set; } = null;
    public string Model { get; set; } = null;
    public int? Year { get; set; }
}