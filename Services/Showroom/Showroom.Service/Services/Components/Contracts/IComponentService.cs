using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Domain.Models.Responses;
using ShowroomService.Data.Models.Requests.Components;

namespace ShowroomService.Core.Services.Components.Contracts;

public interface IComponentService
{
    Task<PageData<Component>> GetComponentsAsync(PaginationModel paginationModel, GetComponentFilter getComponentFilter);
    Task<Component> GetComponentByIdAsync(Guid componentId);
}