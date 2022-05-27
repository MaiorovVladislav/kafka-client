namespace KafkaClient;

using Confluent.Kafka;

public class Consumer
{
    private IConsumer<string, string> _consumer;

    public Consumer(string topicName, ConsumerConfig сonsumerConfig)
    {
        _consumer = new ConsumerBuilder<string, string>(сonsumerConfig)
            // .SetLogHandler()
            // .SetErrorHandler()
            .Build();

        _consumer.Subscribe(topicName);
    }

    public ConsumeResult<string, string> Consume(CancellationToken cancellationToken)
    {
        return _consumer.Consume(cancellationToken);
    }
}