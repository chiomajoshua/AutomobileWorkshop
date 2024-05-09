using BuildingBlocks.Domain.Models.Requests;
using MediatR;

namespace OrderService.Core.Command;

public record UpdateOrderStatusCommand(UpdateOrderStatusRequest UpdateOrderStatusRequest) : IRequest<bool>;