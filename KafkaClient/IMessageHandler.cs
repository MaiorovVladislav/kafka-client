namespace KafkaClient;

public interface IMessageHandler
{
    Task Invoke(IMessageContext context);
}