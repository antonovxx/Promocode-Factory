using Microsoft.EntityFrameworkCore;
using Pcf.Administration.Common.Dto.Sync;
using Pcf.Administration.Common.RabbitMq.Consumer.Extensions;
using Pcf.Administration.Common.SyncServices.SyncServices;
using Pcf.Administration.Common.SyncServices.SyncServices.Impl;
using Pcf.Administration.Core.Abstractions.Repositories;
using Pcf.Administration.Core.Domain.Administration;
using Pcf.Administration.DataAccess;
using Pcf.Administration.DataAccess.Repositories;
using Pcf.Administration.RmqHost.Workers;

namespace Pcf.Administration.RmqHost;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        
        builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        
        builder.Services.AddDbContext<DataContext>(x =>
        {
            x.UseNpgsql(builder.Configuration.GetConnectionString("PromocodeFactoryAdministrationDb"));
            x.UseSnakeCaseNamingConvention();
            x.UseLazyLoadingProxies();
        });
        
        builder.Services.AddConsumer<PromoCodeSyncDto>(builder.Configuration.GetRequiredSection("RabbitMqPromoCodeConsumer").Bind);
        
        builder.Services.AddScoped<ISyncService<PromoCodeSyncDto>, PromoCodeSyncService>();
        
        builder.Services.AddHostedService<PromoCodeSyncWorker>();

        var host = builder.Build();
        
        host.Run();
    }
}