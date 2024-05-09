using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Domain.Models.Responses;
using BuildingBlocks.Infrastructure.Extensions;
using BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;
using ShowroomService.Core.Services.Vehicles.Contracts;
using ShowroomService.Data.Models.Requests.Vehicles;
using System.Linq.Expressions;

namespace ShowroomService.Core.Services.Vehicles.Implementation;

public class VehicleService : IVehicleService
{
    private readonly IRepositoryService<Vehicle> _vehicle;
    public VehicleService(IRepositoryService<Vehicle> vehicle)
    {
        _vehicle = vehicle;
    }

    public async Task<PageData<Vehicle>> GetVehiclesAsync(PaginationModel paginationModel, GetVehicleFilter getVehicleFilter)
    {
        Expression<Func<Vehicle, bool>> query_filter = BuildFilterParams(getVehicleFilter);
        return await _vehicle.GetPagedAsync(paginationModel, query_filter, query => query.ExtendVehicleIncludes());
    }

    public async Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId)
        => await _vehicle.GetSingleAsync(filter: x => x.Id == vehicleId, includeFunc: query => query.ExtendVehicleIncludes());

    private static Expression<Func<Vehicle, bool>> BuildFilterParams(GetVehicleFilter getVehicleFilter)
    {
        Expression<Func<Vehicle, bool>> queryFilter = null;
        if (getVehicleFilter is null)
            return queryFilter;

        if (getVehicleFilter.ComponentType?.Length > 0)
        {
            foreach (var item in getVehicleFilter.ComponentType)
            {
                if (queryFilter is null)
                    queryFilter = e => e.VehicleComponents.FirstOrDefault().Component.ComponentType == item;
                else
                    queryFilter = queryFilter.AndAlso(e => e.VehicleComponents.FirstOrDefault().Component.ComponentType == item);
            }
        }

        return queryFilter;
    }


}
