using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;
using ProductionService.Core.Services.Contracts;

namespace ProductionService.Core.Services.Implementation;

public class ComponentService : IComponentService
{
    private readonly IRepositoryService<Component> _component;
    public ComponentService(IRepositoryService<Component> component)
    {
        _component = component; 
    }

    public async Task<Component> CreateComponent(Component component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));
        return await _component.InsertAsync(component);
    }

    public async Task<Component> GetComponentAsync(Guid id)
    => await _component.GetSingleAsync(x => x.Id == id);

    public async Task<bool> UpdateComponent(Component component)
    {
        if (component is null)
            throw new ArgumentNullException(nameof(component));
        return await _component.UpdateAsync(component);
    }
}
