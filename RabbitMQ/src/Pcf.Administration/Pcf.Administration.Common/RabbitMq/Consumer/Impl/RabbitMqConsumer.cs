using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Pcf.Administration.Common.RabbitMq.Consumer.Options;
using Pcf.Administration.Common.RabbitMq.Exceptions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Pcf.Administration.Common.RabbitMq.Consumer.Impl;

public class RabbitMqConsumer<T>(
    IConnection connection,
    IChannel channel,
    RabbitMqConsumerOptions options,
    ILogger<RabbitMqConsumer<T>> logger)
    : IRabbitMqConsumer<T>
    where T : class
{
    private readonly IConnection _connection = connection;
    private readonly IChannel _channel = channel;
    private readonly RabbitMqConsumerOptions _options = options;
    private readonly ILogger<RabbitMqConsumer<T>> _logger = logger;

    public async Task Subscribe(Func<T, Task> handler, CancellationToken cancellationToken = default)
    {
        try
        {
            await _channel.ExchangeDeclareAsync(
                exchange: _options.ExchangeName,
                type: _options.ExchangeType,
                durable: true,
                autoDelete: false,
                cancellationToken: cancellationToken);
            
            await _channel.QueueDeclareAsync(
                queue: _options.QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: cancellationToken);

            await _channel.QueueBindAsync(
                queue: _options.QueueName,
                exchange: _options.ExchangeName,
                routingKey: _options.RoutingKey,
                cancellationToken: cancellationToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);
        
            consumer.ReceivedAsync += async (_, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(body));

                    if (message is not null)
                    {
                        await handler(message);
                        await _channel.BasicAckAsync(ea.DeliveryTag, false, cancellationToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing message");
                    
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, true, cancellationToken);
                }
            };

            await _channel.BasicConsumeAsync(
                queue: _options.QueueName,
                autoAck: false,
                consumer: consumer,
                cancellationToken: cancellationToken);

            _logger.LogInformation(
                "Started consuming messages from queue {QueueName} with routing key {RoutingKey}",
                _options.QueueName, _options.RoutingKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to subscribe to queue {QueueName}", _options.QueueName);
            throw new RabbitMqConsumerException("Failed to subscribe to queue", ex);
        }
    }
}