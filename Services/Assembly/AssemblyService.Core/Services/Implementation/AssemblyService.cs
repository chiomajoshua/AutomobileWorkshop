using AssemblyService.Core.Services.Contracts;
using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;

namespace AssemblyService.Core.Services.Implementation;

public class AssemblyService : IAssemblyService
{
    private readonly IRepositoryService<AssemblyQueue> _assembly;
    public AssemblyService(IRepositoryService<AssemblyQueue> assembly)
    {
        _assembly = assembly; 
    }

    public async Task<AssemblyQueue> AssembleOrder(AssemblyQueue assemblyQueue)
    {
        if (assemblyQueue is null)
            throw new ArgumentNullException(nameof(assemblyQueue));
        return await _assembly.InsertAsync(assemblyQueue);
    }

    public async Task<AssemblyQueue> GetQueueTaskById(Guid id)
    => await _assembly.GetSingleAsync(filter: x => x.Id == id);

    public async Task<AssemblyQueue> GetQueueTaskByOrderId(Guid orderId)
    => await _assembly.GetSingleAsync(filter: x => x.OrderId == orderId);
}
