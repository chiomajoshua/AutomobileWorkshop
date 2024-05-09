using BuildingBlocks.Domain.Models.Responses;
using MediatR;
using OrderService.Core.Query;
using OrderService.Core.Services.Orders.Contracts;

namespace OrderService.Core.Handler;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, AmwResponse>
{
    private readonly IOrderService _orderService;
    public GetOrderByIdHandler(IOrderService orderService)
    {
        _orderService = orderService; 
    }

    public async Task<AmwResponse> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetOrderByIdAsync(request.OrderId);
        return AmwResponse.ExistsResponse(order);
    }
}
