using KafkaClient.Security;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaClient.Configurations;

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
    private readonly IServiceCollection _services;

    public ClusterConfigurationBuilder(IServiceCollection services)
    {
        _services = services;
    }

    private List<ConsumerConfigurationBuilder> Consumers { get; } = new();

    public ClusterConfiguration Build()
    {
        var clusterConfiguration = new ClusterConfiguration
        {
            BootstrapsServers = _bootstrapsServers,
            SecurityInformation = _securityInformationBuilder?.Build()
        };

        var serviceProvider = _services.BuildServiceProvider();

        clusterConfiguration.Consumers = Consumers.Select(_ => _.Build(clusterConfiguration, serviceProvider)).ToList();

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

    public IClusterConfigurationBuilder AddSecurityInformation(Action<SecurityInformationBuilder> securityInformation)
    {
        if (!string.Equals(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")?.ToUpper(), "Production".ToUpper(), StringComparison.OrdinalIgnoreCase))
            return this;

        _securityInformationBuilder = new SecurityInformationBuilder();

        securityInformation.Invoke(_securityInformationBuilder);

        return this;
    }
}
