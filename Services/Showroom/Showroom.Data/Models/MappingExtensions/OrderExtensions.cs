using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Enumerators;
using Showroom.Data.Models.Requests;

namespace Showroom.Data.Models.MappingExtensions;

public static class OrderExtensions
{
    public static Order ToCreateOrder(this CreateOrderRequest orderRequest, Guid customerId)
    => new()
    {
         CustomerId = customerId,
         OrderStatus = OrderStatus.Placed
    };
}
