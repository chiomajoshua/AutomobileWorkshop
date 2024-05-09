using MediatR;

namespace ProductionService.Core.Command;

public record BuildComponentCommand(object BuildComponentRequest) : IRequest<bool>;