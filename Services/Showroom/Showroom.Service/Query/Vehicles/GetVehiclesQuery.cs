using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Domain.Models.Responses;
using MediatR;
using ShowroomService.Data.Models.Requests.Vehicles;

namespace ShowroomService.Core.Query.Vehicles;

public record GetVehiclesQuery(PaginationModel PaginationModel, GetVehicleFilter GetVehicleFilter) : IRequest<AmwResponse>;