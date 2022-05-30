using KafkaClient.Configurations;
using KafkaClient.Consumers;
using KafkaClient.Producers;

namespace KafkaClient;

public class KafkaClientManager
{
    private IEnumerable<IConsumerWorker>? _workers;

    public ClusterConfiguration? Cluster { get; }

    public IProducerAccessor ProducerAccessor { get; }

    private List<ConsumerConfiguration>? ConsumerConfigurations => Cluster?.Consumers;

    public KafkaClientManager(ClusterConfiguration clusterConfiguration, IProducerAccessor producerAccessor)
    {
        Cluster = clusterConfiguration;
        ProducerAccessor = producerAccessor;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(Cluster);
        ArgumentNullException.ThrowIfNull(ConsumerConfigurations);

        _workers = ConsumerConfigurations
            .Select(consumerConfiguration =>
                new ConsumerWorker(consumerConfiguration))
            .ToList();
        
        await Task
            .WhenAll(_workers.Select(_ => _.StartAsync(cancellationToken)))
            .ConfigureAwait(false);
    }
}
