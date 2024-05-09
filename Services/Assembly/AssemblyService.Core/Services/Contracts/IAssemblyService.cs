using BuildingBlocks.Domain.Entities;

namespace AssemblyService.Core.Services.Contracts;

public interface IAssemblyService
{
    Task<AssemblyQueue> AssembleOrder(AssemblyQueue assemblyQueue);
    Task<AssemblyQueue> GetQueueTaskByOrderId(Guid orderId);
    Task<AssemblyQueue> GetQueueTaskById(Guid id);
    Task<bool> UpdateAssemblyQueue(AssemblyQueue assemblyQueue);
}
