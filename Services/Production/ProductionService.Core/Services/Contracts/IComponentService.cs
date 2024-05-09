using BuildingBlocks.Domain.Entities;

namespace ProductionService.Core.Services.Contracts;

public interface IComponentService
{
    Task<Component> GetComponentAsync(Guid id);
    Task<Component> CreateComponent(Component component);
    Task<bool> UpdateComponent(Component component);
}