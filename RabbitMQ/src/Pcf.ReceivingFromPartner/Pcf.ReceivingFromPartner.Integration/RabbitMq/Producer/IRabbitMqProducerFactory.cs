using System.Threading.Tasks;

namespace Pcf.ReceivingFromPartner.Integration.RabbitMq.Producer;

public interface IRabbitMqProducerFactory
{
    Task<IRabbitMqProducer<T>> CreateProducer<T>(string name) where T : class;
}