using BuildingBlocks.Domain.Entities;
using ShowroomService.Data.Models.Responses.Components;

namespace ShowroomService.Data.Models.MappingExtensions;

public static class ComponentExtensions
{
    public static List<ComponentResponse> ComponentResponseList(this IEnumerable<Component> components)
    {
        List<ComponentResponse> result = null;
        if (components?.Count() is null)
            return result;

        result?.AddRange(components.Select(componentResponse => new ComponentResponse()
        {
            Id = componentResponse.Id,
            Manufacturer = componentResponse.Manufacturer,
            ComponentType = componentResponse.ComponentType,
            Price = componentResponse.Price
        }));
        return result;
    }
}
