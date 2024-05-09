using BuildingBlocks.Domain.Entities;
using ShowroomService.Data.Models.Responses.Vehicles;

namespace ShowroomService.Data.Models.MappingExtensions;

public static class VehicleExtensions
{
    public static List<VehicleResponse> VehicleResponseList(this IEnumerable<Vehicle> vehicles)
    {
        List<VehicleResponse> result = null;
        if (vehicles?.Count() < 1)
            return result;

        result?.AddRange(vehicles.Select(vehicleResponse => new VehicleResponse()
        {
            Id = vehicleResponse.Id,
            Manufacturer = vehicleResponse.Manufacturer,
            Model = vehicleResponse.Model,
            Year = vehicleResponse.Year,
            QuantityAvailable = vehicleResponse.QuantityAvailable,
            InventoryId = vehicleResponse.InventoryId,
            Price = vehicleResponse.Price,
            Components = vehicleResponse.VehicleComponents.Where(vc => vc.VehicleId == vehicleResponse.Id).Select(x => x.Component).ComponentResponseList()
        }));
        return result;
    }

    public static VehicleResponse VehicleResponse(this Vehicle vehicle)
    {
        if (vehicle is null)
            return null;

        return new VehicleResponse()
        {
            Id = vehicle.Id,
            Manufacturer = vehicle.Manufacturer,
            Model = vehicle.Model,
            Year = vehicle.Year,
            QuantityAvailable = vehicle.QuantityAvailable,
            InventoryId = vehicle.InventoryId,
            Price = vehicle.Price,
            Components = vehicle.VehicleComponents.Where(vc => vc.VehicleId == vehicle.Id).Select(x => x.Component).ComponentResponseList()
        };
    }
}