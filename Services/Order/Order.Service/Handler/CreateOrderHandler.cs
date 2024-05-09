using BuildingBlocks.Domain.Models.Responses;
using BuildingBlocks.Infrastructure.Helpers;
using BuildingBlocks.Infrastructure.RabbitMq.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using OrderService.Core.Command;
using OrderService.Core.Services.OrderItems.Contracts;
using OrderService.Core.Services.Orders.Contracts;
using OrderService.Data.Models.MappingExtensions;
using System.IdentityModel.Tokens.Jwt;

namespace OrderService.Core.Handler;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, AmwResponse>
{
    private readonly IOrderService _orderService;
    private readonly IOrderItemService _orderItemService;
    private readonly IRabbitMqProducer _rabbitMqProducer;

    public CreateOrderHandler(IOrderService orderService, IOrderItemService orderItemService, 
                              IRabbitMqProducer rabbitMqProducer)
    { 
        _orderService = orderService;
        _orderItemService = orderItemService;
        _rabbitMqProducer = rabbitMqProducer;
    }

    public async Task<AmwResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderRequest = request.CreateOrderRequest;
        HttpContext context = HttpContextHelper.Current;
        var customerId = Guid.Parse(context.User?.Claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sid)?.Value);
                                                                       
        var order = await _orderService.CreateOrderAsync(orderRequest.ToCreateOrder(customerId));
        if (order is null)
            return AmwResponse.ExceptionResponse();
        else            
            await _orderItemService.CreateOrderItem(orderRequest.OrderItems.OrderItemDb(order.Id));
        
        _rabbitMqProducer.PublishMessage(new { OrderId = order.Id }, "order.placed");
        return AmwResponse.CreatedResponse("Your Order has been placed");
    }
}
