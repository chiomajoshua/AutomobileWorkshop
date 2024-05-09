using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;
using OrderService.Core.Services.Orders.Contracts;
using BuildingBlocks.Infrastructure.Extensions;

namespace OrderService.Core.Services.Orders.Implementation;

public class OrderService : IOrderService
{
    private readonly IRepositoryService<Order> _order;
    public OrderService(IRepositoryService<Order> order)
    {
        _order = order;   
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        if (order is null)
            throw new ArgumentNullException(nameof(order));
        return await _order.InsertAsync(order);
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
       => await _order.GetSingleAsync(filter: x => x.Id == id, includeFunc: query => query.ExtendOrderIncludes());

    public async Task<bool> UpdateOrderAsync(Order order)
    {
        if (order is null)
            throw new ArgumentNullException(nameof(order));
        return await _order.UpdateAsync(order);
    }
}
