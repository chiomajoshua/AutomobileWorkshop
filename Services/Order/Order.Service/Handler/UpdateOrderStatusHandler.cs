using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Infrastructure.RabbitMq.Contracts;
using MediatR;
using OrderService.Core.Command;
using OrderService.Core.Services.Orders.Contracts;

namespace OrderService.Core.Handler;

public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
{
    private readonly IOrderService _orderService;
    private readonly IRabbitMqProducer _rabbitMqProducer;
    public UpdateOrderStatusHandler(IOrderService orderService, IRabbitMqProducer rabbitMqProducer)
    {
        _orderService = orderService;
        _rabbitMqProducer = rabbitMqProducer;
    }

    public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var updateOrderRequest = request.UpdateOrderStatusRequest;
        var order = await _orderService.GetOrderByIdAsync(updateOrderRequest.OrderId);
        if (order is null)
        {
            _rabbitMqProducer.PublishMessage(new { Message = $"Order with Id, {updateOrderRequest.OrderId}, not found" }, "order.notfound");
            return false;
        }
        order.OrderStatus = updateOrderRequest.OrderStatus;
        if(!await _orderService.UpdateOrderAsync(order))
            return false;
        
        _rabbitMqProducer.PublishMessage(new OrderRequest { OrderId = updateOrderRequest.OrderId  }, "order.statusupdated");
        return false;
    }
}