using BuildingBlocks.Domain.Models.Responses;
using MediatR;

namespace ShowroomService.Core.Query.Vehicles;

public record GetVehicleByIdQuery(Guid VehicleId) : IRequest<AmwResponse>;