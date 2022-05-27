using Confluent.Kafka;

namespace KafkaClient;

public interface IConsumerConfigurationBuilder
{
    IConsumerConfigurationBuilder SetGroupId(string groupId);
    
    IConsumerConfigurationBuilder SetTopicDestination(string topicName);
    
    IConsumerConfigurationBuilder SetAutoCommitOffset(bool enableAutoCommit);
    
    IConsumerConfigurationBuilder SetMessageHandler(IMessageHandler messageHandler);
}

public class ConsumerConfigurationBuilder : IConsumerConfigurationBuilder
{
    private string _groupId = default!;
    private string _topicName = default!;
    private bool _enableAutoCommit;
    private ConsumerConfig _consumerConfig = null!;
    private IMessageHandler _messageHandler = null!;

    public IConsumerConfigurationBuilder SetMessageHandler(IMessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
        return this;
    }
    public IConsumerConfigurationBuilder SetGroupId(string groupId)
    {
        _groupId = groupId;
        return this;
    }
    
    public IConsumerConfigurationBuilder SetTopicDestination(string topicName)
    {
        _topicName = topicName;
        return this;
    }

    public IConsumerConfigurationBuilder SetAutoCommitOffset(bool enableAutoCommit)
    {
        _enableAutoCommit = enableAutoCommit;
        return this;
    }

    public ConsumerConfiguration Build(ClusterConfiguration clusterConfiguration)
    {
        _consumerConfig = new ConsumerConfig
        {
            BootstrapServers = clusterConfiguration.BootstrapsServers,
            SecurityProtocol = 
                clusterConfiguration.SecurityInformation?.SecurityProtocol is not null
                ? Confluent.Kafka.SecurityProtocol.Ssl 
                : null,
           
            SslKeyLocation = clusterConfiguration.SecurityInformation?.SslKeyLocation,
            SslKeyPassword = clusterConfiguration.SecurityInformation?.SslKeyPassword,
            SslCaLocation = clusterConfiguration.SecurityInformation?.SslCaLocation,
          
            AutoOffsetReset = AutoOffsetReset.Latest,
            GroupId = _groupId,
            EnableAutoCommit = _enableAutoCommit
        };
        
        return new ConsumerConfiguration(_topicName, _consumerConfig, _messageHandler);
    }
}