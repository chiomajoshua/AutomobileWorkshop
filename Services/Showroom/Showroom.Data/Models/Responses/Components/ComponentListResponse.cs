using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Models.Responses;

namespace ShowroomService.Data.Models.Responses.Components;

public record ComponentListResponse : PageDetails
{
    public IEnumerable<Component> Components { get; set; }
}