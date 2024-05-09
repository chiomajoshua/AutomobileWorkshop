using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;
using ProductionService.Core.Services.Contracts;

namespace ProductionService.Core.Services.Implementation;

public class ProductionServices : IProductionService
{
    private readonly IRepositoryService<ProductionQueue> _production;
    public ProductionServices(IRepositoryService<ProductionQueue> production)
    {
        _production = production;
    }

    public async Task<ProductionQueue> CreateProductionTask(ProductionQueue productionQueue)
    {
        if (productionQueue is null)
            throw new ArgumentNullException(nameof(productionQueue));
        return await _production.InsertAsync(productionQueue);
    }

    public async Task<bool> UpdateProductionQueue(ProductionQueue productionQueue)
    {
        if (productionQueue is null)
            throw new ArgumentNullException(nameof(productionQueue));
        return await _production.UpdateAsync(productionQueue);
    }
}