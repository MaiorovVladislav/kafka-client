using Confluent.Kafka;
namespace KafkaClient.Messages;

public class ConsumeConsumeMessageContext : IConsumeMessageContext<string, CloudEvent>
{
    public string Key { get; set; }

    public CloudEvent Value { get; set; }

    public string Topic { get; set; }

    public string Offset { get; set; }

    public ConsumeResult<string,string> ConsumerResult { get; set; }
}
