using BuildingBlocks.Domain.Models.Requests;
using MediatR;

namespace ProductionService.Core.Command;

public record BuildComponentCommand(OrderRequest OrderRequest) : IRequest<bool>;