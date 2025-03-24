using System.Threading;
using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Integration.RabbitMq.Producer;

public interface IRabbitMqProducer<in T>
    where T : class
{
    Task PublishAsync(string routingKey, T message, CancellationToken cancellationToken = default);
}