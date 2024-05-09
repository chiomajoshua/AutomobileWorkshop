using AssemblyService.Core.Command;
using AssemblyService.Core.Services.Contracts;
using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Enumerators;
using BuildingBlocks.Domain.Models.Requests;
using BuildingBlocks.Infrastructure.InternetClient.Contracts;
using BuildingBlocks.Infrastructure.RabbitMq.Contracts;
using MediatR;
using Newtonsoft.Json;

namespace AssemblyService.Core.Handler;

public class AssembleOrderHandler : IRequestHandler<AssembleOrderCommand, bool>
{
    private readonly IAssemblyService _assemblyService;
    private readonly IHttpClientService _httpClientService;
    private readonly IRabbitMqProducer _rabbitMqProducer;
    public AssembleOrderHandler(IAssemblyService assemblyService, IHttpClientService httpClientService, IRabbitMqProducer rabbitMqProducer)
    {
        _assemblyService = assemblyService;
        _httpClientService = httpClientService;
        _rabbitMqProducer = rabbitMqProducer;
    }

    public async Task<bool> Handle(AssembleOrderCommand request, CancellationToken cancellationToken)
    {
        var assemblyRequest = request.OrderRequest;
        var assemblyTask = await _assemblyService.GetQueueTaskByOrderId(assemblyRequest.OrderId);
        if (assemblyTask is null)
        {
            var order = await GetOrderById(assemblyRequest.OrderId);
            if(order is null)
            {
                _rabbitMqProducer.PublishMessage(new { Message = $"Order with Id, {assemblyRequest.OrderId}, not found" }, "order.notfound");
                return true;
            }
            if (order.OrderStatus != OrderStatus.Completed || order.OrderStatus != OrderStatus.Cancelled)
            {
                assemblyTask = await _assemblyService.AssembleOrder(new AssemblyQueue { OrderId = assemblyRequest.OrderId, AssemblyStatus = BuildingBlocks.Domain.Enumerators.TaskStatus.Pending, IsDelivered = false });
                int totalComponentsNeeded = 0;
                int totalComponentsAvailable = 0;
                if (order.OrderItems?.Count > 0)
                {
                    foreach (var componentNeeded in order.OrderItems)
                    {
                        totalComponentsNeeded += componentNeeded.Quantity;
                        var component = await GetComponentById(componentNeeded.ComponentId);
                        if (component?.QuantityAvailable < componentNeeded.Quantity)
                            _rabbitMqProducer.PublishMessage(new OrderRequest { OrderId = assemblyRequest.OrderId }, "produce.component");
                        else
                            totalComponentsAvailable += componentNeeded.Quantity;
                    }
                }

                if (totalComponentsAvailable >= totalComponentsNeeded)
                {
                    assemblyTask.AssemblyStatus = BuildingBlocks.Domain.Enumerators.TaskStatus.Completed;
                    assemblyTask.IsDelivered = true;
                    await _assemblyService.UpdateAssemblyQueue(assemblyTask);
                    _rabbitMqProducer.PublishMessage(new OrderRequest { OrderId = assemblyRequest.OrderId }, "assemble.ready");
                    return true;
                }
                else
                {
                    assemblyTask.AssemblyStatus = BuildingBlocks.Domain.Enumerators.TaskStatus.InProgress;
                    assemblyTask.IsDelivered = false;
                    await _assemblyService.UpdateAssemblyQueue(assemblyTask);
                    return true;
                }
            }
            return false;
        }

        var status = assemblyTask?.AssemblyStatus == BuildingBlocks.Domain.Enumerators.TaskStatus.Completed && assemblyTask.IsDelivered;
        if(!status)
            _rabbitMqProducer.PublishMessage(new OrderRequest { OrderId = assemblyRequest.OrderId }, "assemble.ready");      
        return true;
    }

    private async Task<Component> GetComponentById(Guid componentId)
    {
        var baseUrl = Environment.GetEnvironmentVariable("WarehouseServiceBaseUrl", EnvironmentVariableTarget.Process);
        var getVehicleUrl = Environment.GetEnvironmentVariable("GetComponentById", EnvironmentVariableTarget.Process).Replace("{componentId}", componentId.ToString());
        var url = $"{baseUrl}{getVehicleUrl}";
        var response = await _httpClientService.MakeHttpCall(HttpMethod.Get, url);
        if (response.IsSuccessStatusCode)
            return JsonConvert.DeserializeObject<Component>(await response.Content.ReadAsStringAsync());
        return null;
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
