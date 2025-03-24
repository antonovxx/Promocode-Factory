namespace Pcf.Administration.Common.SyncServices.SyncServices;

public interface ISyncService<in T>
{
    Task SyncAsync(T message, CancellationToken cancellationToken = default);
}