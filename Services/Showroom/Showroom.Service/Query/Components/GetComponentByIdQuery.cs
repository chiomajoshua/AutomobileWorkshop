using BuildingBlocks.Domain.Models.Responses;
using MediatR;

namespace ShowroomService.Core.Query.Components;

public record GetComponentByIdQuery(Guid ComponentId) : IRequest<AmwResponse>;