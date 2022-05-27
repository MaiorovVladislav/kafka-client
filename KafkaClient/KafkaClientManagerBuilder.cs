namespace KafkaClient;

public class KafkaClientManagerBuilder
{
    public KafkaClientManager AddCluster(Action<IClusterConfigurationBuilder> cluster)
    {
        var clusterConfigurationBuilder = new ClusterConfigurationBuilder();

        cluster.Invoke(clusterConfigurationBuilder);

        return new KafkaClientManager
        {
            Cluster = clusterConfigurationBuilder.Build()
        };
    }
}