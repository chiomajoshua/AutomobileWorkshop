using AssemblyService.Core.Command;
using AssemblyService.Core.Services.Contracts;
using BuildingBlocks.Domain.Entities;
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
           //Create Assembly Task
           //if components are availabe, update assembly task status to completed, initiate delivery and set IsDelivered to true
           
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
}
