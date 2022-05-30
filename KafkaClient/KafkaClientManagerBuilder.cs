using KafkaClient.Configurations;
using KafkaClient.Producers;
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

        var clusterConfiguration = clusterConfigurationBuilder.Build();

        var producerAccessor = new ProducerAccessor(clusterConfiguration.Producers.Select(_ => _.CreateProducer()));
        
        _services.AddSingleton((IProducerAccessor)producerAccessor);

        return new KafkaClientManager(clusterConfiguration, producerAccessor);
    }
}