using BuildingBlocks.Domain.Enumerators;

namespace ShowroomService.Data.Models.Requests.Components;

public class GetComponentFilter
{
    public string Manufacturer { get; set; } = null;
    public ComponentType? ComponentType { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}
