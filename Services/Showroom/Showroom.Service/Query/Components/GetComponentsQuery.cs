using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Domain.Models.Responses;
using MediatR;
using ShowroomService.Data.Models.Requests.Components;

namespace ShowroomService.Core.Query.Components;

public record GetComponentsQuery(PaginationModel PaginationModel, GetComponentFilter GetComponentFilter) : IRequest<AmwResponse>;