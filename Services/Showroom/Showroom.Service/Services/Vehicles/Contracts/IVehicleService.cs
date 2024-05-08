using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Domain.Models.Responses;
using ShowroomService.Data.Models.Requests.Vehicles;

namespace ShowroomService.Core.Services.Vehicles.Contracts;

public interface IVehicleService
{
    Task<PageData<Vehicle>> GetVehiclesAsync(PaginationModel paginationModel, GetVehicleFilter getVehicleFilter);
    Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId);
}