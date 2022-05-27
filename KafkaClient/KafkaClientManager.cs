namespace KafkaClient;

public class KafkaClientManager
{
    public ClusterConfiguration? Cluster { get; set; }
    private List<ConsumerConfiguration>? ConsumerConfigurations => Cluster?.Consumers;
    private List<ConsumerWorker>? _workers;
        
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
