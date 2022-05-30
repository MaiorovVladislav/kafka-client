using KafkaClient.Producers;
using KafkaClient.Security;

namespace KafkaClient.Configurations;

public class ClusterConfiguration
{
    public List<ConsumerConfiguration> Consumers { get; set; } = new();
    
    public List<ProducerConfiguration> Producers { get; set; } = new();

    public string BootstrapsServers { get; init; } = default!;

    public SecurityInformation? SecurityInformation { get; init; }
}
