using KafkaClient.Console;
using KafkaClient.Extensions;
using KafkaClient.Handlers;
using KafkaClient.Messages;
using Microsoft.Extensions.DependencyInjection;

var collection = new ServiceCollection();

collection.AddKafkaHostedService(builder => builder
    .AddKafkaClient(cluster => cluster
        .AddBootstrapsServers("localhost:9092")
        .AddSecurityInformation(security => security
            .AddSslKeyLocation("")
            .AddSslKeyPassword("")
            .AddSslCaLocation(""))
        .AddConsumer(consumer => consumer
            .SetTopicDestination("testTopic")
            .SetGroupId("test")
            .SetAutoCommitOffset(false)
            .SetMessageHandler<CloudEventMessageHandler>())
        .AddConsumer(consumer => consumer
            .SetTopicDestination("testTopic")
            .SetGroupId("test")
            .SetAutoCommitOffset(false)
            .SetMessageHandler<CloudEventMessageHandler>())));

    namespace KafkaClient.Console
    {
        public class CloudEventMessageHandler : IMessageHandler
        {
            public Task Invoke(IMessageContext messageContext)
            {
                throw new NotImplementedException();
            }
        }
    }
