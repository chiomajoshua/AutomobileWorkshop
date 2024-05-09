using BuildingBlocks.Domain.Entities;

namespace ProductionService.Core.Services.Contracts;

public interface IProductionService
{
    Task<ProductionQueue> CreateProductionTask(ProductionQueue productionQueue);
    Task<bool> UpdateProductionQueue(ProductionQueue productionQueue);
}