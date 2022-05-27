using Confluent.Kafka;

namespace KafkaClient;

public class ConsumerConfiguration
{
    public string Topic { get; }

    public ConsumerConfig ConsumerConfig { get; }
    
    public IMessageHandler MessageHandler { get; }

    public ConsumerConfiguration(
        string topic, 
        ConsumerConfig consumerConfig, 
        IMessageHandler messageHandler)
    {
        Topic = topic;
        ConsumerConfig = consumerConfig;
        MessageHandler = messageHandler;
    }

    public Consumer CreateConsumer()
    {
        return new Consumer(Topic, ConsumerConfig);
    }
}