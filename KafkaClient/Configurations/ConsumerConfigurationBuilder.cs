using System.Reflection;
using Confluent.Kafka;
using KafkaClient.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaClient.Configurations;

public interface IConsumerConfigurationBuilder
{
    IConsumerConfigurationBuilder SetGroupId(string groupId);

    IConsumerConfigurationBuilder SetTopicDestination(string topicName);

    IConsumerConfigurationBuilder SetAutoCommitOffset(bool enableAutoCommit);

    IConsumerConfigurationBuilder SetMessageHandler<TMessageHandler>() where TMessageHandler : IMessageHandler;
}

public class ConsumerConfigurationBuilder : IConsumerConfigurationBuilder
{
    private string _groupId = default!;
    private string _topicName = default!;
    private bool _enableAutoCommit;
    private ConsumerConfig _consumerConfig = null!;

    private readonly IServiceCollection _services;
    private Type _typeMessageHandler;

    public ConsumerConfigurationBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public IConsumerConfigurationBuilder SetMessageHandler<TMessageHandler>()
        where TMessageHandler : IMessageHandler
    {
        _typeMessageHandler = typeof(TMessageHandler);
        
        _services.AddSingleton(_typeMessageHandler);

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

    public ConsumerConfiguration Build(ClusterConfiguration clusterConfiguration, IServiceProvider serviceProvider)
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

        var messageHandler = serviceProvider.GetService(_typeMessageHandler.GetTypeInfo());

        ArgumentNullException.ThrowIfNull(messageHandler);

        return new ConsumerConfiguration(_topicName, _consumerConfig, (IMessageHandler)messageHandler);
    }
}
