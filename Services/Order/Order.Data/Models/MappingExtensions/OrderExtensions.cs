using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Enumerators;
using OrderService.Data.Models.Requests;

namespace OrderService.Data.Models.MappingExtensions;

public static class OrderExtensions
{
    public static Order ToCreateOrder(this CreateOrderRequest orderRequest, Guid customerId)
    => new()
    {
         CustomerId = customerId,
         OrderStatus = OrderStatus.Placed
    };
}
