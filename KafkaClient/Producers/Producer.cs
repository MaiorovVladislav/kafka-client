using Confluent.Kafka;
using KafkaClient.Extensions;

namespace KafkaClient.Producers;

public sealed class Producer
{
    private readonly ProducerConfig _config;

    private readonly IProducer<string, string> _producer;
    private readonly string _topicName;

    public string ProducerName { get; set; }

    public Producer(string producerName, string topicName, ProducerConfig config)
    {
        _config = config;
        _topicName = topicName;
        // _producer = new ProducerBuilder<string, string>(config)
        //.SetLogHandler()
        //.SetErrorHandler()
        //    .Build();

        ProducerName = producerName;
    }

    public async Task<DeliveryResult<string, string>> ProduceAsync(string key, CloudEvent @event,
        CancellationToken cancellationToken)
    {
        var message = new Message<string, string>
        {
            Key = key,
            Value = JsonSerializerHelper.Serialize(@event)
        };

        return await _producer.ProduceAsync(_topicName, message, cancellationToken);
    }
}