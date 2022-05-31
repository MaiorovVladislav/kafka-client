using Confluent.Kafka;

namespace KafkaClient.Configurations;

public interface IProducerConfigurationBuilder
{
    IProducerConfigurationBuilder SetTopicDestination(string topicName);
}

public class ProducerConfigurationBuilder : IProducerConfigurationBuilder
{
    private ProducerConfig _producerConfig = null!;
    private string _topicName = null!;
    private readonly string _producerName;

    public ProducerConfigurationBuilder(string producerName)
    {
        _producerName = producerName;
    }

    public IProducerConfigurationBuilder SetTopicDestination(string topicName)
    {
        _topicName = topicName;
        return this;
    }

    public ProducerConfiguration Build(ClusterConfiguration clusterConfiguration)
    {
        _producerConfig = new ProducerConfig
        {
            BootstrapServers = clusterConfiguration.BootstrapsServers,
            SecurityProtocol =
                clusterConfiguration.SecurityInformation?.SecurityProtocol is not null
                    ? Confluent.Kafka.SecurityProtocol.Ssl
                    : null,

            SslKeyLocation = clusterConfiguration.SecurityInformation?.SslKeyLocation,
            SslKeyPassword = clusterConfiguration.SecurityInformation?.SslKeyPassword,
            SslCaLocation = clusterConfiguration.SecurityInformation?.SslCaLocation
        };

        return new ProducerConfiguration(_producerName, _topicName, _producerConfig);
    }
}