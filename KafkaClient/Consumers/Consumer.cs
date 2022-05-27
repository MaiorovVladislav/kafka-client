using Confluent.Kafka;

namespace KafkaClient.Consumers;

public class Consumer
{
    private readonly ConsumerConfig _config;

    private readonly IConsumer<string, string> _consumer;

    public bool EnableAutCommit => _config.EnableAutoCommit ?? true;

    public Consumer(string topicName, ConsumerConfig config)
    {
        _config = config;
        _consumer = new ConsumerBuilder<string, string>(config)
            // .SetLogHandler()
            // .SetErrorHandler()
            .Build();

        _consumer.Subscribe(topicName);
    }

    public ConsumeResult<string, string> Consume(CancellationToken cancellationToken)
    {
        return _consumer.Consume(cancellationToken);
    }

    public void Commit()
    {
        _consumer.Commit();
    }
}
