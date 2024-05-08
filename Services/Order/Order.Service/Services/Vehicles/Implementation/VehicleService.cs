using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Infrastructure.Extensions;
using BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;
using OrderService.Core.Services.Vehicles.Contracts;

namespace OrderService.Core.Services.Vehicles.Implementation;

public class VehicleService : IVehicleService
{
    private readonly IRepositoryService<Vehicle> _vehicle;
    public VehicleService(IRepositoryService<Vehicle> vehicle)
    {
        _vehicle = vehicle;
    }

    public async Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId)
     => await _vehicle.GetSingleAsync(filter: x => x.Id == vehicleId, includeFunc: query => query.ExtendVehicleIncludes());
}