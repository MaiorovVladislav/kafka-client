namespace KafkaClient;

public class ClusterConfiguration
{
    public List<ConsumerConfiguration>? Consumers { get; set; } = new();

    public string BootstrapsServers { get; set; } = default!;

    public SecurityInformation? SecurityInformation { get; set; }
}