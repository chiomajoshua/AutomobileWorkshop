using BuildingBlocks.Domain.Enumerators;
using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Domain.Models.Responses;
using BuildingBlocks.Infrastructure.RabbitMq.Contracts;
using MediatR;
using OrderService.Core.Command;
using OrderService.Core.Services.Orders.Contracts;

namespace OrderService.Core.Handler;

public class CancelOrderHandler : IRequestHandler<CancelOrderCommand, AmwResponse>
{
    private readonly IOrderService _orderService;
    private readonly IRabbitMqProducer _rabbitMqProducer;
    public CancelOrderHandler(IOrderService orderService, IRabbitMqProducer rabbitMqProducer)
    {
        _orderService = orderService;
        _rabbitMqProducer = rabbitMqProducer;
    }

    public async Task<AmwResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        var cancelRequest = request.CancelOrderRequest;
        
        var order = await _orderService.GetOrderByIdAsync(cancelRequest.OrderId);
        if (order is null)
            return AmwResponse.BadRequestResponse(order);

        order.OrderStatus = OrderStatus.Cancelled;
        order.LastModifiedDate = DateTime.UtcNow;

        if(!await _orderService.UpdateOrderAsync(order))
            return AmwResponse.ExceptionResponse();
        
        _rabbitMqProducer.PublishMessage(new OrderRequest { OrderId = order.Id }, "order.cancelled");
        return AmwResponse.SuccessResponse(order);
    }
}
