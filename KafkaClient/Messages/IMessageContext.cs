using Confluent.Kafka;

namespace KafkaClient.Messages;


public interface IMessageContext
{
    string Key { get; }

    CloudEvent Value { get; }

    string Topic { get; }

    string Offset { get; }

    public ConsumeResult<string,string> ConsumerResult { get; }
}
