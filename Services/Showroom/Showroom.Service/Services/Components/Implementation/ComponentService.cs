using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Domain.Models.Responses;
using BuildingBlocks.Infrastructure.Extensions;
using BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;
using ShowroomService.Core.Services.Components.Contracts;
using ShowroomService.Data.Models.Requests.Components;
using System.Linq.Expressions;

namespace ShowroomService.Core.Services.Components.Implementation;

public class ComponentService : IComponentService
{
    private readonly IRepositoryService<Component> _component;
    public ComponentService(IRepositoryService<Component> component)
    {
        _component = component;
    }

    public async Task<Component> GetComponentByIdAsync(Guid componentId)
    => await _component.GetSingleAsync(x => x.Id == componentId);
                                                                                      
    public async Task<PageData<Component>> GetComponentsAsync(PaginationModel paginationModel, GetComponentFilter getComponentFilter)
    {
        Expression<Func<Component, bool>> query_filter = BuildFilterParams(getComponentFilter);
        return await _component.GetPagedAsync(paginationModel, query_filter);
    }

    private static Expression<Func<Component, bool>> BuildFilterParams(GetComponentFilter getComponentFilter)
    {
        Expression<Func<Component, bool>> queryFilter = null;
        if (getComponentFilter is null)
            return queryFilter;

        if (!string.IsNullOrEmpty(getComponentFilter.Manufacturer))
            queryFilter = x => x.Manufacturer.Contains(getComponentFilter.Manufacturer);

        if(getComponentFilter.ComponentType.HasValue)
        {
            if(queryFilter is null)
                queryFilter = x => x.ComponentType == getComponentFilter.ComponentType.Value;
            else
                queryFilter = queryFilter.Or(x => x.ComponentType == getComponentFilter.ComponentType.Value);
        }

        if (getComponentFilter.MinPrice.Value >= 0 && (getComponentFilter.MaxPrice.Value > getComponentFilter.MinPrice.Value))
        {
            if (queryFilter is null)
                queryFilter = x => x.ComponentType == getComponentFilter.ComponentType.Value;
            else
                queryFilter = queryFilter.Or(x => x.ComponentType == getComponentFilter.ComponentType.Value);
        }

        return queryFilter;
    }
}