using BuildingBlocks.Infrastructure.RabbitMq.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace BuildingBlocks.Infrastructure.RabbitMq.Implementation;

public class RabbitMqProducer : IRabbitMqProducer
{
    private readonly string _connectionString;
    private readonly string _exchange;
    public RabbitMqProducer(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("RabbitMq:ConnectionString");
        _exchange = configuration.GetValue<string>("RabbitMq:Exchange");
    }

    public void PublishMessage<T>(T message, string topicName)
    {
        var factory = new ConnectionFactory() { Uri = new Uri(_connectionString) };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare(_exchange, type: ExchangeType.Topic);
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));        
        channel.BasicPublish(exchange: "",
                             routingKey: topicName,
                             basicProperties: null,
                             body: body);
    }
}