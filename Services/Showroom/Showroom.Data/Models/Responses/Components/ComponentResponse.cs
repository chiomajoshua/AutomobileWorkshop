using BuildingBlocks.Domain.Enumerators;

namespace ShowroomService.Data.Models.Responses.Components;

public class ComponentResponse
{
    public Guid Id { get; set; }
    public string Manufacturer { get; set; } = null!;
    public decimal Price { get; set; }
    public ComponentType ComponentType { get; set; }
    public int QuantityAvailable { get; set; }
}