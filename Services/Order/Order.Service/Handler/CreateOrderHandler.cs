using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Models.Responses;
using BuildingBlocks.Infrastructure.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;
using OrderService.Core.Services.OrderItems.Contracts;
using OrderService.Core.Services.Orders.Contracts;
using OrderService.Core.Services.Vehicles.Contracts;
using OrderService.Data.Models.MappingExtensions;
using OrderService.Service.Command;
using System.IdentityModel.Tokens.Jwt;

namespace OrderService.Service.Handler;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, AmwResponse>
{
    private readonly IOrderService _orderService;
    private readonly IOrderItemService _orderItemService;
    private readonly IVehicleService _vehicleService;
    //private readonly IEventBus _eventBus;

    public CreateOrderHandler(IVehicleService vehicleService, IOrderService orderService, IOrderItemService orderItemService)//IEventBus eventBus
    {
        _vehicleService = vehicleService;
        _orderService = orderService;
        _orderItemService = orderItemService;
    }

    public async Task<AmwResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderRequest = request.CreateOrderRequest;
        HttpContext context = HttpContextHelper.Current;
        var customerId = Guid.Parse(context.User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sid).Value);

        var vehicle = await _vehicleService.GetVehicleByIdAsync(orderRequest.VehicleId);
        if (vehicle is null)
            return AmwResponse.ExistsResponse((Vehicle)null);

        var order = await _orderService.CreateOrder(orderRequest.ToCreateOrder(customerId));
        if (order is null)
            return AmwResponse.SuccessResponse(order);
        else            
            await _orderItemService.CreateOrderItem(orderRequest.OrderItems.OrderItemDb(order.Id));
     

        //await _eventBus.PublishAsync(new OrderPlacedEvent(order.Id, order.CustomerId, order.VehicleId, order.ComponentIds));

        return AmwResponse.SuccessResponse(null);
    }
}
