using BuildingBlocks.Domain.Entities;

namespace ProductionService.Core.Services.Contracts;

public interface IProductionService
{
    Task<bool> UpdateComponent(Component component);
}