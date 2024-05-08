using BuildingBlocks.Domain.Entities;

namespace OrderService.Core.Services.OrderItems.Contracts;

public interface IOrderItemService
{
    Task<bool> CreateOrderItem(List<OrderItem> orderItems);
}