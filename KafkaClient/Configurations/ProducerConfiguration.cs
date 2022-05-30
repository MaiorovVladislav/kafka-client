using Confluent.Kafka;

namespace KafkaClient.Configurations;

public class ProducerConfiguration
{
    public string TopicName { get; }

    public string ProducerName { get; }

    public ProducerConfig ProducerConfig { get; }

    public ProducerConfiguration(string producerName, string topicNameName, ProducerConfig producerConfig)
    {
        ProducerName = producerName;
        TopicName = topicNameName;
        ProducerConfig = producerConfig;
    }

    public Producers.Producer CreateProducer()
    {
        return new Producers.Producer(ProducerName, TopicName, ProducerConfig);
    }
}