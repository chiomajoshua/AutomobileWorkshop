using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Enumerators;
using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Infrastructure.InternetClient.Contracts;
using BuildingBlocks.Infrastructure.RabbitMq.Contracts;
using MediatR;
using Newtonsoft.Json;
using ProductionService.Core.Command;
using ProductionService.Core.Services.Contracts;

namespace ProductionService.Core.Handler;

public class BuildComponentHandler : IRequestHandler<BuildComponentCommand, bool>
{
    private readonly IProductionService _productionService;
    private readonly IComponentService _componentService;
    private readonly IHttpClientService _httpClientService;
    private readonly IRabbitMqProducer _rabbitMqProducer;

    public BuildComponentHandler(IProductionService productionService, IRabbitMqProducer rabbitMqProducer,
                                IHttpClientService httpClientService, IComponentService componentService)
    {
        _productionService = productionService;
        _rabbitMqProducer = rabbitMqProducer;
        _httpClientService = httpClientService;
        _componentService = componentService;
    }

    public async Task<bool> Handle(BuildComponentCommand request, CancellationToken cancellationToken)
    {           
        var productionRequest = request.OrderRequest;
        var order = await GetOrderById(productionRequest.OrderId);
        if (order is null)
        {
            _rabbitMqProducer.PublishMessage(new { Message = $"Order with Id, {productionRequest.OrderId}, not found" }, "order.notfound");
            return true;
        }
        if ((order.OrderStatus != OrderStatus.Completed || order.OrderStatus != OrderStatus.Cancelled) && order.OrderItems?.Count > 0)
        {
            foreach (var componentNeeded in order.OrderItems)
            {
                var productionQueue = await _productionService.CreateProductionTask(new ProductionQueue { OrderId = order.Id, ComponentId = componentNeeded.ComponentId, 
                    ProductionStatus = BuildingBlocks.Domain.Enumerators.TaskStatus.InProgress, Quantity = componentNeeded.Quantity });
                var component = await _componentService.GetComponentAsync(componentNeeded.ComponentId);
                component.QuantityAvailable += componentNeeded.Quantity;
                await _componentService.UpdateComponent(component);
                productionQueue.ProductionStatus = BuildingBlocks.Domain.Enumerators.TaskStatus.Completed;
                await _productionService.UpdateProductionQueue(productionQueue);
            }
            _rabbitMqProducer.PublishMessage(new OrderRequest { OrderId = productionRequest.OrderId }, "assemble.order");
        }
        return true;
    }

    private async Task<Order> GetOrderById(Guid orderId)
    {
        var baseUrl = Environment.GetEnvironmentVariable("OrderServiceBaseUrl", EnvironmentVariableTarget.Process);
        var getVehicleUrl = Environment.GetEnvironmentVariable("GetOrderById", EnvironmentVariableTarget.Process).Replace("{orderId}", orderId.ToString());
        var url = $"{baseUrl}{getVehicleUrl}";
        var response = await _httpClientService.MakeHttpCall(HttpMethod.Get, url);
        if (response.IsSuccessStatusCode)
            return JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());
        return null;
    }
}