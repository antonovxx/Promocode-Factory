using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pcf.ReceivingFromPartner.Integration.RabbitMq.Exceptions;
using Pcf.ReceivingFromPartner.Integration.RabbitMq.Producer.Options;
using RabbitMQ.Client;

namespace Pcf.ReceivingFromPartner.Integration.RabbitMq.Producer.Impl;

public class RabbitMqProducer<T>(
    IConnection connection,
    IChannel channel,
    RabbitMqProducerOptions options,
    ILogger<RabbitMqProducer<T>> logger)
    : IRabbitMqProducer<T>
    where T : class
{
    private readonly IConnection _connection = connection;
    private readonly IChannel _channel = channel;
    private readonly RabbitMqProducerOptions _options = options;
    private readonly ILogger<RabbitMqProducer<T>> _logger = logger;

    public async Task PublishAsync(string routingKey, T message, CancellationToken cancellationToken = default)
    {
        try
        {
            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            await _channel.ExchangeDeclareAsync(
                exchange: _options.ExchangeName,
                type: _options.ExchangeType,
                durable: true,
                autoDelete: false,
                cancellationToken: cancellationToken);

            await _channel.BasicPublishAsync(
                exchange: _options.ExchangeName,
                routingKey: routingKey,
                body: body,
                cancellationToken: cancellationToken);

            _logger.LogDebug("Message published to exchange {Exchange} with routing key {RoutingKey}",
                _options.ExchangeName, routingKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to publish message to exchange {Exchange}", _options.ExchangeName);
            throw new RabbitMqProducerException("Failed to publish message to exchange", ex);
        }
    }
}