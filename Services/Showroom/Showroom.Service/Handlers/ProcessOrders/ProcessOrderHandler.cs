using BuildingBlocks.Domain.Entities;
using BuildingBlocks.Domain.Enumerators;
using BuildingBlocks.Infrastructure.InternetClient.Contracts;
using BuildingBlocks.Infrastructure.RabbitMq.Contracts;
using MediatR;
using Newtonsoft.Json;
using ShowroomService.Core.Command;

namespace ShowroomService.Core.Handlers.ProcessOrders;

public class ProcessOrderHandler : IRequestHandler<ProcessOrderCommand, bool>
{
    private readonly IHttpClientService _httpClientService;
    private readonly IRabbitMqProducer _rabbitMqProducer;
    public ProcessOrderHandler(IHttpClientService httpClientService,IRabbitMqProducer rabbitMqProducer)
    {
        _httpClientService = httpClientService;
        _rabbitMqProducer = rabbitMqProducer;
    }

    public async Task<bool> Handle(ProcessOrderCommand request, CancellationToken cancellationToken)
    {
        var orderRequest = request.OrderRequest;        
        var baseUrl = Environment.GetEnvironmentVariable("OrderServiceBaseUrl", EnvironmentVariableTarget.Process);
        var getOrderUrl = Environment.GetEnvironmentVariable("GetOrderById", EnvironmentVariableTarget.Process).Replace("{orderId}", orderRequest.OrderId.ToString());
        var url = $"{baseUrl}{getOrderUrl}";
        var response = await _httpClientService.MakeHttpCall(HttpMethod.Get, url);
        if (response.IsSuccessStatusCode)
        {
            var order = JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());
            if (order.OrderStatus != OrderStatus.Completed || order.OrderStatus != OrderStatus.Cancelled)
            {
                var vehicle = await GetVehicleById(order.VehicleId);
                if (vehicle is null || vehicle?.QuantityAvailable < 1)
                {
                    _rabbitMqProducer.PublishMessage(new { OrderId = order.Id }, "assemble.order");
                    return true;
                }
            }
            _rabbitMqProducer.PublishMessage(new { OrderId = order.Id }, "order.ready");
            return true;
        }
        _rabbitMqProducer.PublishMessage(new { Message = $"Order with Id, {orderRequest.OrderId}, not found" }, "order.notfound");
        return true;
    }

    private async Task<Vehicle> GetVehicleById(Guid vehicleId)
    {       
        var baseUrl = Environment.GetEnvironmentVariable("WarehouseServiceBaseUrl", EnvironmentVariableTarget.Process);
        var getVehicleUrl = Environment.GetEnvironmentVariable("GetVehicleById", EnvironmentVariableTarget.Process).Replace("{vehicleId}", vehicleId.ToString());
        var url = $"{baseUrl}{getVehicleUrl}";
        var response = await _httpClientService.MakeHttpCall(HttpMethod.Get, url);
        if (response.IsSuccessStatusCode)
            return  JsonConvert.DeserializeObject<Vehicle>(await response.Content.ReadAsStringAsync());        
        return null;
    }
}