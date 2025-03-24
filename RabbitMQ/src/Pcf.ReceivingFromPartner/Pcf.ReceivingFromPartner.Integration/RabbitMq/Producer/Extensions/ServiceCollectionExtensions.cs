using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pcf.ReceivingFromPartner.Integration.RabbitMq.Producer.Impl;
using Pcf.ReceivingFromPartner.Integration.RabbitMq.Producer.Options;
using RabbitMQ.Client;

namespace Pcf.ReceivingFromPartner.Integration.RabbitMq.Producer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProducer<T>(
        this IServiceCollection services,
        Action<RabbitMqProducerOptions> configure)
        where T : class
    {
        var name = typeof(T).Name;
    
        services.Configure(name, configure);

        if (services.All(x => x.ServiceType != typeof(IConnection)))
        {
            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptionsMonitor<RabbitMqProducerOptions>>().Get(name);
                var factory = new ConnectionFactory
                {
                    HostName = options.HostName,
                    Port = options.Port,
                    UserName = options.UserName,
                    Password = options.Password,
                    VirtualHost = options.VirtualHost,
                };

                return factory.CreateConnectionAsync().GetAwaiter().GetResult();
            });
        }

        if (services.All(x => x.ServiceType != typeof(IRabbitMqProducerFactory)))
        {
            services.AddSingleton<IRabbitMqProducerFactory, RabbitMqProducerFactory>();
        }

        services.AddSingleton(sp =>
        {
            var factory = sp.GetRequiredService<IRabbitMqProducerFactory>();
            return factory.CreateProducer<T>(name).GetAwaiter().GetResult();
        });

        return services;
    }
}