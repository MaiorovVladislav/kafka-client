using KafkaClient;
using KafkaClient.Extensions;
using KafkaClient.Handlers;
using KafkaClient.Messages;
using KafkaClient.Producers;
using Microsoft.Extensions.DependencyInjection;

var collection = new ServiceCollection();

collection.AddKafkaHostedService(builder => builder
    .AddKafkaClient(cluster => cluster
        .AddBootstrapsServers("localhost:9092")
        .AddSecurityInformation(true, security => security
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
            .SetMessageHandler<CloudEventMessageHandler>())
        .AddProducer("ProducerErrorMessage", producer => producer
            .SetTopicDestination("testTopic"))));

//
// var provider = collection.BuildServiceProvider();
// var accessor = (IProducerAccessor) provider.GetRequiredService(typeof(IProducerAccessor));
//
// var producerError = accessor.GetProducer("ErrorProducer");
// await producerError?.ProduceAsync("123", new CloudEvent(), CancellationToken.None)!;
//
// Console.WriteLine(producerError.ProducerName);

public class CloudEventMessageHandler : IMessageHandler
{
    private readonly Producer _producer;
    public CloudEventMessageHandler(IProducerAccessor accessor)
    {
        _producer = accessor.GetProducer("ProducerErrorMessage");
    }

    public Task Invoke(IConsumeMessageContext<string, CloudEvent> context)
    {
        throw new NotImplementedException();
    }
}