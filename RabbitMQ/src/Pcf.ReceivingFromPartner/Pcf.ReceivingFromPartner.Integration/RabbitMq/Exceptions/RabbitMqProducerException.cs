using System;

namespace Pcf.ReceivingFromPartner.Integration.RabbitMq.Exceptions;

public class RabbitMqProducerException : Exception
{
    public RabbitMqProducerException()
        : base()
    {
    }
    
    public RabbitMqProducerException(string message)
        : base(message)
    {
    }

    public RabbitMqProducerException(string message, Exception exception)
        : base(message, exception)
    {
    }
}