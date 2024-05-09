using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.PipelineBehaviours;

public static class ConfigurePipelineBehavioursModule
{
    public static IServiceCollection RegisterPipelineBehaviours(this IServiceCollection services)
    {
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));

        return services;
    }
}