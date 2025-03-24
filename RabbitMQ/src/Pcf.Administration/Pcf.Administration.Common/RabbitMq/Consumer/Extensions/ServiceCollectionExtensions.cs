using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pcf.Administration.Common.RabbitMq.Consumer.Impl;
using Pcf.Administration.Common.RabbitMq.Consumer.Options;
using RabbitMQ.Client;

namespace Pcf.Administration.Common.RabbitMq.Consumer.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConsumer<T>(
        this IServiceCollection services,
        Action<RabbitMqConsumerOptions> configure)
        where T : class
    {
        var name = typeof(T).Name;
    
        services.Configure(name, configure);

        if (services.All(x => x.ServiceType != typeof(IConnection)))
        {
            services.AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptionsMonitor<RabbitMqConsumerOptions>>().Get(name);
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

        if (services.All(x => x.ServiceType != typeof(IRabbitMqConsumerFactory)))
        {
            services.AddSingleton<IRabbitMqConsumerFactory, RabbitMqConsumerFactory>();
        }

        services.AddSingleton<IRabbitMqConsumer<T>>(sp =>
        {
            var factory = sp.GetRequiredService<IRabbitMqConsumerFactory>();
            return factory.CreateConsumer<T>(name).GetAwaiter().GetResult();
        });

        return services;
    }
}