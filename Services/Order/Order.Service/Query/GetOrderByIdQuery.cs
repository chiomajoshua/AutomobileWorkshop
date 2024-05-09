using BuildingBlocks.Domain.Models.Responses;
using MediatR;

namespace OrderService.Core.Query;

public record GetOrderByIdQuery(Guid OrderId) : IRequest<AmwResponse>;