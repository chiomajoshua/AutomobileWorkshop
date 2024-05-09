using BuildingBlocks.Domain.Entities;

namespace OrderService.Core.Services.Orders.Contracts;

public interface IOrderService
{
    Task<Order> CreateOrderAsync(Order order);
    Task<bool> UpdateOrderAsync(Order order);
    Task<Order> GetOrderByIdAsync(Guid id);
}