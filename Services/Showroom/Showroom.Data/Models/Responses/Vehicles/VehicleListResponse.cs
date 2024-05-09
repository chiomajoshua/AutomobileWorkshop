using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Models.Responses;

namespace ShowroomService.Data.Models.Responses.Vehicles;

public record VehicleListResponse : PageDetails
{
    public IEnumerable<Vehicle> Vehicles { get; set; }
}