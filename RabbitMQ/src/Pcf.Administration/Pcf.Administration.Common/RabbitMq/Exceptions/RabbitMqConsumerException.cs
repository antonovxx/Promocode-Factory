namespace Pcf.Administration.Common.RabbitMq.Exceptions;

public class RabbitMqConsumerException : Exception
{
    public RabbitMqConsumerException()
        : base()
    {
    }
    
    public RabbitMqConsumerException(string message)
        : base(message)
    {
    }

    public RabbitMqConsumerException(string message, Exception exception)
        : base(message, exception)
    {
    }
}