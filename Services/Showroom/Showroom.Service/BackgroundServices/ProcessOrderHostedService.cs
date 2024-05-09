using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Newtonsoft.Json;
using System.Text;
using BuildingBlocks.Domain.Models.Requests;
using ShowroomService.Core.Command;

namespace ShowroomService.Core.BackgroundServices;

public class ProcessOrderHostedService : BackgroundService
{
    private readonly string _connectionString;
    private readonly string _exchange;
    private readonly IMediator _mediator;
    public ProcessOrderHostedService(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _connectionString = configuration.GetValue<string>("RabbitMq:ConnectionString");
        _exchange = configuration.GetValue<string>("RabbitMq:Exchange");
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
                          routingKey: "order.placed");

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            var message = JsonConvert.DeserializeObject<OrderRequest>(Encoding.UTF8.GetString(body));
            await _mediator.Send(new ProcessOrderCommand(message));
        };
        channel.BasicConsume(queue: queueName,
                             autoAck: true,
                             consumer: consumer);

        return Task.CompletedTask;
    }
}
