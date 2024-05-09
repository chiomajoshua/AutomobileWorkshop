using AssemblyService.Core.Command;
using BuildingBlocks.Domain.Models.Requests;
using MediatR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace AssemblyService.Core.BackgroundServices;

public class AssembleComponentHostedService : BackgroundService
{                        
    private readonly string _connectionString;
    private readonly string _exchange;
    private readonly IMediator _mediator;
    public AssembleComponentHostedService(IMediator mediator)
    {
        _mediator = mediator;
        _connectionString = Environment.GetEnvironmentVariable("RabbitMqConnectionString", EnvironmentVariableTarget.Process);
        _exchange = Environment.GetEnvironmentVariable("RabbitMqExchangeName", EnvironmentVariableTarget.Process);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory() { Uri = new Uri(_connectionString) };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: _exchange, type: ExchangeType.Fanout);
        var queueName = channel.QueueDeclare().QueueName;

        channel.QueueBind(queue: queueName,
                          exchange: _exchange,
                          routingKey: "assemble.order");

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            var message = JsonConvert.DeserializeObject<OrderRequest>(Encoding.UTF8.GetString(body));
            await _mediator.Send(new AssembleOrderCommand(message));
        };
        channel.BasicConsume(queue: queueName,
                             autoAck: true,
                             consumer: consumer);

        return Task.CompletedTask;
    }
}