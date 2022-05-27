using KafkaClient;
using KafkaClient.Extensions;
using Microsoft.Extensions.DependencyInjection;

var collection = new ServiceCollection();

collection.AddKafkaHostedService(builder => builder
    .AddCluster(cluster => cluster
        .AddBootstrapsServers("localhost:9092")
        .AddSecurityInformation(security => security
            .AddSslKeyLocation("")
            .AddSslKeyPassword("")
            .AddSslCaLocation(""))
        .AddConsumer(consumer => consumer
            .SetTopicDestination("testTopic")
            .SetGroupId("test")
            .SetAutoCommitOffset(false)
            .SetMessageHandler(new CloudEventMessageHandler()))
        .AddConsumer(consumer => consumer
            .SetTopicDestination("testTopic")
            .SetGroupId("test")
            .SetAutoCommitOffset(false)
            .SetMessageHandler(new CloudEventMessageHandler()))));

public class CloudEventMessageHandler : IMessageHandler
{
    public Task Invoke(IMessageContext messageContext)
    {
        throw new NotImplementedException();
    }
}