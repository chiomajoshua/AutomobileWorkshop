using BuildingBlocks.Domain.Models.Responses;
using MediatR;
using ShowroomService.Core.Query.Vehicles;
using ShowroomService.Core.Services.Vehicles.Contracts;
using ShowroomService.Data.Models.MappingExtensions;
using ShowroomService.Data.Models.Responses.Vehicles;

namespace ShowroomService.Core.Handlers.Vehicles;

public class GetVehiclesHandler : IRequestHandler<GetVehiclesQuery, AmwResponse>
{
    private readonly IVehicleService _vehicleService;
    public GetVehiclesHandler(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    public async Task<AmwResponse> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
    {
        var vehicles = await _vehicleService.GetVehiclesAsync(request.PaginationModel, request.GetVehicleFilter);
        return AmwResponse.ExistsResponse(new VehicleListResponse
        {
            Vehicles = vehicles.EntityData?.VehicleResponseList(),
            CurrentPage = request.PaginationModel.Page,
            PageSize = request.PaginationModel.PageSize,
            TotalPages = (int)Math.Ceiling(vehicles.TotalCount / (double)request.PaginationModel.PageSize)
        });
    }
}
