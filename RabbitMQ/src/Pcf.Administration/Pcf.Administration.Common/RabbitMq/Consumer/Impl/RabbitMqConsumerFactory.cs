using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pcf.Administration.Common.RabbitMq.Consumer.Options;
using RabbitMQ.Client;

namespace Pcf.Administration.Common.RabbitMq.Consumer.Impl;

public class RabbitMqConsumerFactory(IConnection connection, IOptionsMonitor<RabbitMqConsumerOptions> optionsMonitor, ILoggerFactory loggerFactory)
    : IRabbitMqConsumerFactory
{
    private readonly ConcurrentDictionary<string, object> _consumers = new();
    private readonly IOptionsMonitor<RabbitMqConsumerOptions> _optionsMonitor = optionsMonitor;

    public async Task<IRabbitMqConsumer<T>> CreateConsumer<T>(string name) where T : class
    {
        var options = _optionsMonitor.Get(name);
        var channel = await connection.CreateChannelAsync();
        var logger = loggerFactory.CreateLogger<RabbitMqConsumer<T>>();

        return (IRabbitMqConsumer<T>)_consumers.GetOrAdd(name, _ =>
            new RabbitMqConsumer<T>(connection, channel, options, logger));
    }
}