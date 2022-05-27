using Confluent.Kafka;
namespace KafkaClient.Messages;

public class MessageContext : IMessageContext
{
    public string Key { get; set; }

    public CloudEvent Value { get; set; }

    public string Topic { get; set; }

    public string Offset { get; set; }

    public ConsumeResult<string,string> ConsumerResult { get; set; }
}
