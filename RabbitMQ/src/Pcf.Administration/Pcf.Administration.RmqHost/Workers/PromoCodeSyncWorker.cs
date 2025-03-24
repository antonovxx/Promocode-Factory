using Pcf.Administration.Common.Dto.Sync;
using Pcf.Administration.Common.RabbitMq.Consumer;
using Pcf.Administration.Common.SyncServices.SyncServices;

namespace Pcf.Administration.RmqHost.Workers;

public class PromoCodeSyncWorker(
    ILogger<PromoCodeSyncWorker> logger,
    IRabbitMqConsumer<PromoCodeSyncDto> consumer,
    IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly ILogger<PromoCodeSyncWorker> _logger = logger;
    private readonly IRabbitMqConsumer<PromoCodeSyncDto> _consumer = consumer;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        await _consumer.Subscribe(async message =>
        {
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var syncService = scope.ServiceProvider.GetRequiredService<ISyncService<PromoCodeSyncDto>>();
                
                await syncService.SyncAsync(message, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error processing event sync message. Id: {PromoCodeSyncId}",
                    message.PartnerManagerId);

                throw;
            }
        }, cancellationToken);
    }
}