using KafkaClient.Messages;

namespace KafkaClient.Handlers;

public interface IMessageHandler
{
    Task Invoke(IConsumeMessageContext<string, CloudEvent> context);
}