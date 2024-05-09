namespace BuildingBlocks.Infrastructure.RabbitMq.Contracts;

public interface IRabbitMqProducer
{
    void PublishMessage<T>(T message, string topicName);
}