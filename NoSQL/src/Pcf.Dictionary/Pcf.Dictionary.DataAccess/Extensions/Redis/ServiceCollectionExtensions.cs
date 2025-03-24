using Microsoft.Extensions.DependencyInjection;
using Pcf.Dictionary.DataAccess.Repositories;
using Pcf.Dictionary.DataAccess.Repositories.Impl;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;

namespace Pcf.Dictionary.DataAccess.Extensions.Redis
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRedis(this IServiceCollection services, string? redisConnectionString) 
        {
            ArgumentNullException.ThrowIfNull(redisConnectionString, nameof(redisConnectionString));
            
            services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(new RedisConfiguration
            {
                IsDefault = true,
                Name = "default",
                ConnectionString = redisConnectionString,
                KeyPrefix = "pcf:dictionary:"
            });

            services.AddSingleton<IPreferenceRepository, CachedPreferenceRepository>();
        }
    }
}
