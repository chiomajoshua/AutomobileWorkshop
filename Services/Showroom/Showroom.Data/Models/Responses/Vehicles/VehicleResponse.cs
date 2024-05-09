using BuildingBlocks.Domain.Enumerators;
using ShowroomService.Data.Models.Responses.Components;

namespace ShowroomService.Data.Models.Responses.Vehicles;

public class VehicleResponse
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public int QuantityAvailable { get; set; }
    public decimal Price { get; set; }
    public Guid InventoryId { get; set; }
    public List<ComponentResponse> Components { get; set; } = new();
}
