using BuildingBlocks.Domain.Models.Requests;
using MediatR;

namespace AssemblyService.Core.Command;

public record AssembleOrderCommand(OrderRequest OrderRequest) : IRequest<bool>;