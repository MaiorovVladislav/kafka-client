using KafkaClient.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace KafkaClient;

public class KafkaClientManagerBuilder
{
    private readonly IServiceCollection _services;

    public KafkaClientManagerBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public KafkaClientManager AddKafkaClient(Action<IClusterConfigurationBuilder> cluster)
    {
        var clusterConfigurationBuilder = new ClusterConfigurationBuilder(_services);

        cluster.Invoke(clusterConfigurationBuilder);

        return new KafkaClientManager(clusterConfigurationBuilder.Build());
    }
}
