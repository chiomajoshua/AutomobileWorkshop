using BuildingBlocks.Domain.Models.Requests;
using MediatR;

namespace ShowroomService.Core.Command;

public record ProcessOrderCommand(OrderRequest OrderRequest) : IRequest<bool>;