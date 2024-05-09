using BuildingBlocks.Infrastructure.RabbitMq.Contracts;
using MediatR;
using ProductionService.Core.Command;
using ProductionService.Core.Services.Contracts;

namespace ProductionService.Core.Handler;

public class BuildComponentHandler : IRequestHandler<BuildComponentCommand, bool>
{
    private readonly IProductionService _productionService;
    private readonly IRabbitMqProducer _rabbitMqProducer;

    public BuildComponentHandler(IProductionService productionService, IRabbitMqProducer rabbitMqProducer)
    {
        _productionService = productionService;
        _rabbitMqProducer = rabbitMqProducer;
    }

    public async Task<bool> Handle(BuildComponentCommand request, CancellationToken cancellationToken)
    {
        //Gets The AssemblyId and the components
        //var assemblyDetails = await _assemblyService.GetAssemblyTaskById()

        //Produces each component and updates the component counter
        var component =  await _productionService.UpdateComponent(new BuildingBlocks.Domain.Entities.Component { });
        
        //notifies the assembly service of the availability of the components.        
        _rabbitMqProducer.PublishMessage(new { AssemblyId = Guid.NewGuid() }, "component.created");
        return component;
    }
}