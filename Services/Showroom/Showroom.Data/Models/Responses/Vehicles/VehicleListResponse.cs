using BuildingBlocks.Domain.Models.Responses;

namespace ShowroomService.Data.Models.Responses.Vehicles;

public record VehicleListResponse : PageDetails
{
    public List<VehicleResponse> Vehicles { get; set; }
}