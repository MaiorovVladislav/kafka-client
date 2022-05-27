using System.Text.Json;
using Confluent.Kafka;

namespace KafkaClient;

public class ConsumerWorker : IConsumerWorker
{
    private Consumer _consumer;
    private IMessageHandler _handler;

    public ConsumerConfiguration ConsumerConfiguration { get; }

    public ConsumerWorker(ConsumerConfiguration consumerConfiguration)
    {
        ConsumerConfiguration = consumerConfiguration;

        //TODO вынести в Factory?
        _consumer = consumerConfiguration.CreateConsumer();
        _handler = consumerConfiguration.MessageHandler;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        return Task.Factory.StartNew(async () =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var stopConsume = new CancellationTokenSource(TimeSpan.FromMinutes(3));
                    
                    var consumeResult = _consumer.Consume(stopConsume.Token);

                    try
                    {
                        var cloudEvent = JsonSerializerHelper.Deserialize<CloudEvent>(consumeResult.Message.Value);

                        var context = new MessageContext(consumeResult.Message.Key, cloudEvent);

                        await _handler
                            .Invoke(context)
                            .ConfigureAwait(false);
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
                catch (OperationCanceledException)
                {
                }
            }
        }, TaskCreationOptions.LongRunning);
    }
}