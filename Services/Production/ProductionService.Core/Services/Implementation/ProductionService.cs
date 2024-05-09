using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;
using ProductionService.Core.Services.Contracts;

namespace ProductionService.Core.Services.Implementation;

public class ProductionService : IProductionService
{
    private readonly IRepositoryService<Component> _component;
    public ProductionService(IRepositoryService<Component> component)
    {
        _component = component;
    }

    public async Task<bool> UpdateComponent(Component component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));
        return await _component.UpdateAsync(component);
    }
}