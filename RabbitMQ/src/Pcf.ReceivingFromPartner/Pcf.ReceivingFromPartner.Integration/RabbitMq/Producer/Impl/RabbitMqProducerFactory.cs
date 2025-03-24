using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pcf.ReceivingFromPartner.Integration.RabbitMq.Producer.Options;
using RabbitMQ.Client;

namespace Pcf.ReceivingFromPartner.Integration.RabbitMq.Producer.Impl;

public class RabbitMqProducerFactory(
    IConnection connection,
    IOptionsMonitor<RabbitMqProducerOptions> optionsMonitor,
    ILoggerFactory loggerFactory)
    : IRabbitMqProducerFactory
{
    private readonly ConcurrentDictionary<string, object> _producers = new();
    private readonly IOptionsMonitor<RabbitMqProducerOptions> _optionsMonitor = optionsMonitor;

    public async Task<IRabbitMqProducer<T>> CreateProducer<T>(string name) where T : class
    {
        var options = _optionsMonitor.Get(name);
        var channel = await connection.CreateChannelAsync();
        var logger = loggerFactory.CreateLogger<RabbitMqProducer<T>>();

        return (IRabbitMqProducer<T>)_producers.GetOrAdd(name, _ =>
            new RabbitMqProducer<T>(connection, channel, options, logger));
    }
}