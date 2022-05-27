namespace KafkaClient;

public interface IClusterConfigurationBuilder
{
    IClusterConfigurationBuilder AddBootstrapsServers(string bootstrapsServers);
    
    IClusterConfigurationBuilder AddConsumer(Action<IConsumerConfigurationBuilder> consumer);
    
    IClusterConfigurationBuilder AddSecurityInformation(Action<SecurityInformationBuilder> securityInformation);
}

public class ClusterConfigurationBuilder : IClusterConfigurationBuilder
{
    private string _bootstrapsServers = default!;

    private SecurityInformationBuilder? _securityInformationBuilder;

    private List<ConsumerConfigurationBuilder> Consumers { get; } = new();

    public ClusterConfiguration Build()
    {
        var clusterConfiguration = new ClusterConfiguration
        {
            BootstrapsServers = _bootstrapsServers,
            SecurityInformation = _securityInformationBuilder?.Build()
        };

        clusterConfiguration.Consumers = Consumers.Select(_ => _.Build(clusterConfiguration)).ToList();
        
        return clusterConfiguration;
    }

    public IClusterConfigurationBuilder AddBootstrapsServers(string bootstrapsServers)
    {
        _bootstrapsServers = bootstrapsServers;

        return this;
    }

    public IClusterConfigurationBuilder AddConsumer(Action<IConsumerConfigurationBuilder> consumer)
    {
        var consumerConfigurationBuilder = new ConsumerConfigurationBuilder();

        consumer.Invoke(consumerConfigurationBuilder);

        Consumers.Add(consumerConfigurationBuilder);

        return this;
    }

    public IClusterConfigurationBuilder AddSecurityInformation(Action<SecurityInformationBuilder> securityInformation)
    {
        _securityInformationBuilder = new SecurityInformationBuilder();

        securityInformation.Invoke(_securityInformationBuilder);

        return this;
    }
}