namespace Pcf.Administration.Common.RabbitMq.Consumer;

public interface IRabbitMqConsumerFactory
{
    Task<IRabbitMqConsumer<T>> CreateConsumer<T>(string name) where T : class;
}