using Confluent.Kafka;

namespace KafkaClient.Messages;

public interface IMessageContext
{
    string Topic { get; }

    string Offset { get; }

    public ConsumeResult<string, string> ConsumerResult { get; }
}

public interface IConsumeMessageContext<out TKey, out TValue> : IMessageContext
{
    TKey Key { get; }

    TValue Value { get; }
}