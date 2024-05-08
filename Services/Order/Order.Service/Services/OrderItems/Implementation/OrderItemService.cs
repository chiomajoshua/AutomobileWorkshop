using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;
using OrderService.Core.Services.OrderItems.Contracts;

namespace OrderService.Core.Services.OrderItems.Implementation;

public class OrderItemService : IOrderItemService
{
    private readonly IRepositoryService<OrderItem> _orderItem;
    public OrderItemService(IRepositoryService<OrderItem> orderItem)
    {
        _orderItem = orderItem;
    }

    public async Task<bool> CreateOrderItem(List<OrderItem> orderItems)
    {
        if (orderItems?.Count < 1)
            throw new ArgumentNullException(nameof(orderItems));
        return await _orderItem.InsertBulkAsync(orderItems);
    }
}
