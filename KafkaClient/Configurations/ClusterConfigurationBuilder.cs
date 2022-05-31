using KafkaClient.Producers;
using KafkaClient.Security;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaClient.Configurations;

public interface IClusterConfigurationBuilder
{
    IClusterConfigurationBuilder AddBootstrapsServers(string bootstrapsServers);

    IClusterConfigurationBuilder AddConsumer(Action<IConsumerConfigurationBuilder> consumer);

    IClusterConfigurationBuilder AddSecurityInformation(bool enable,
        Action<SecurityInformationBuilder> securityInformation);

    IClusterConfigurationBuilder AddProducer(string producerName, Action<IProducerConfigurationBuilder> producer);
}

public class ClusterConfigurationBuilder : IClusterConfigurationBuilder
{
    private string _bootstrapsServers = default!;

    private SecurityInformationBuilder? _securityInformationBuilder;

    private readonly IServiceCollection _services;

    private List<ConsumerConfigurationBuilder> Consumers { get; } = new();

    private List<ProducerConfigurationBuilder> Producers { get; } = new();

    public ClusterConfigurationBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public ClusterConfiguration Build()
    {
        var clusterConfiguration = new ClusterConfiguration
        {
            BootstrapsServers = _bootstrapsServers,
            SecurityInformation = _securityInformationBuilder?.Build()
        };
        
        BuildProducer(clusterConfiguration);
        BuildConsumer(clusterConfiguration);

        return clusterConfiguration;
    }
    private ClusterConfiguration BuildProducer(ClusterConfiguration clusterConfiguration)
    {
        clusterConfiguration.Producers = Producers.Select(
            _ => _.Build(clusterConfiguration)).ToList();

        clusterConfiguration.ProducersAccessor = 
            new ProducerAccessor(clusterConfiguration.Producers.Select(_ => _.CreateProducer()));

        _services.AddSingleton((IProducerAccessor)clusterConfiguration.ProducersAccessor);

        return clusterConfiguration;
    }

    private ClusterConfiguration BuildConsumer(ClusterConfiguration clusterConfiguration)
    {
        var serviceProvider = _services.BuildServiceProvider();

        clusterConfiguration.Consumers = Consumers.Select(
            _ => _.Build(clusterConfiguration, serviceProvider)).ToList();

        return clusterConfiguration;
    }



    public IClusterConfigurationBuilder AddBootstrapsServers(string bootstrapsServers)
    {
        _bootstrapsServers = bootstrapsServers;

        return this;
    }

    public IClusterConfigurationBuilder AddConsumer(Action<IConsumerConfigurationBuilder> consumer)
    {
        var consumerConfigurationBuilder = new ConsumerConfigurationBuilder(_services);

        consumer.Invoke(consumerConfigurationBuilder);

        Consumers.Add(consumerConfigurationBuilder);

        return this;
    }

    public IClusterConfigurationBuilder AddProducer(string producerName, Action<IProducerConfigurationBuilder> producer)
    {
        var producerConfigurationBuilder = new ProducerConfigurationBuilder(producerName);

        producer.Invoke(producerConfigurationBuilder);

        Producers.Add(producerConfigurationBuilder);

        return this;
    }

    public IClusterConfigurationBuilder AddSecurityInformation(bool enableSsl,
        Action<SecurityInformationBuilder> securityInformation)
    {
        if (!enableSsl) return this;

        _securityInformationBuilder = new SecurityInformationBuilder();

        securityInformation.Invoke(_securityInformationBuilder);

        return this;
    }
}