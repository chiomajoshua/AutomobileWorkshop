using BuildingBlocks.Domain.Models.Responses;
using MediatR;
using ShowroomService.Core.Query.Vehicles;
using ShowroomService.Core.Services.Vehicles.Contracts;
using ShowroomService.Data.Models.MappingExtensions;

namespace ShowroomService.Core.Handlers.Vehicles;

public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdQuery, AmwResponse>
{
    private readonly IVehicleService _vehicleService;
    public GetVehicleByIdHandler(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public async Task<AmwResponse> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _vehicleService.GetVehicleByIdAsync(request.VehicleId);
        return AmwResponse.ExistsResponse(response.VehicleResponse());
    }
}