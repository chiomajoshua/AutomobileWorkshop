using BuildingBlocks.Domain.Entities;

namespace OrderService.Core.Services.Orders.Contracts;

public interface IOrderService
{
    Task<Order> CreateOrder(Order order);
    Task<bool> UpdateOrder(Order order);
}