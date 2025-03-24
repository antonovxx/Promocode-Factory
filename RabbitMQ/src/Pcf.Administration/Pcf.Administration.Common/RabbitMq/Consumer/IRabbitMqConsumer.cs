namespace Pcf.Administration.Common.RabbitMq.Consumer;

public interface IRabbitMqConsumer<out T>
    where T : class
{
    Task Subscribe(Func<T, Task> handler, CancellationToken cancellationToken = default);
}