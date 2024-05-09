using BuildingBlocks.Domain.Entities;
using OrderService.Data.Models.Requests;

namespace OrderService.Data.Models.MappingExtensions;

public static class OrderItemExtensions
{
    public static List<OrderItem> OrderItemDb(this IEnumerable<OrderItemRequest> orderItemRequests, Guid orderId)
    {
        List<OrderItem> result = null;
        if (orderItemRequests?.Count() < 1)
            return result;

        result?.AddRange(orderItemRequests.Select(orderItem => new OrderItem()
        {
            ComponentId = orderItem.ComponentId,
            OrderId = orderId,
            PricePerUnit = orderItem.PricePerUnit
        }));
        return result;
    }
}