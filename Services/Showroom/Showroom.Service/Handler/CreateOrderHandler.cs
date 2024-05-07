using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Models.Responses;
using BuildingBlocks.Infrastructure.RepositoryManager.EfCore.Services;
using MediatR;
using Showroom.Service.Command;

namespace Showroom.Service.Handler;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, AmwResponse>
{
    private readonly IRepositoryService<Order> _orderRepository;
    //private readonly IEventBus _eventBus;

    public CreateOrderHandler(IRepositoryService<Order> orderRepository)//IEventBus eventBus
    {
        _orderRepository = orderRepository;
        //_eventBus = eventBus;
    }

    public async Task<AmwResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        //var order = new Order(request.CustomerId, request.VehicleId, request.ComponentIds);
        //await _orderRepository.AddAsync(order);

        //var orderDto = new OrderDto
        //{
        //    Id = order.Id,
        //    CustomerId = order.CustomerId,
        //    VehicleId = order.VehicleId,
        //    ComponentIds = order.ComponentIds
        //};

        //await _eventBus.PublishAsync(new OrderPlacedEvent(order.Id, order.CustomerId, order.VehicleId, order.ComponentIds));

        return AmwResponse.SuccessResponse(null);
    }
}
