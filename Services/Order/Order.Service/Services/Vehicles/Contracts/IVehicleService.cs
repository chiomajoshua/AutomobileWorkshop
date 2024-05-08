using BuildingBlocks.Domain.Entities;

namespace OrderService.Core.Services.Vehicles.Contracts;

public interface IVehicleService
{
    Task<Vehicle> GetVehicleByIdAsync(Guid vehicleId);
}