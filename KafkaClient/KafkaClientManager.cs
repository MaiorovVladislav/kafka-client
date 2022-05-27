using KafkaClient.Configurations;
using KafkaClient.Consumers;

namespace KafkaClient;

public class KafkaClientManager
{
    private IEnumerable<IConsumerWorker>? _workers;

    public ClusterConfiguration? Cluster { get; }

    private List<ConsumerConfiguration>? ConsumerConfigurations => Cluster?.Consumers;

    public KafkaClientManager(ClusterConfiguration clusterConfiguration)
    {
        Cluster = clusterConfiguration;
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
