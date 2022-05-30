using KafkaClient.Configurations;
using KafkaClient.Extensions;
using KafkaClient.Handlers;
using KafkaClient.Messages;

namespace KafkaClient.Consumers;

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

                        var context = new ConsumeConsumeMessageContext
                        {
                            Key = consumeResult.Message.Key,
                            Value = cloudEvent,
                            Offset = consumeResult.Offset.Value.ToString(),
                            Topic = consumeResult.Topic,
                            ConsumerResult = consumeResult
                        };

                        await _handler
                            .Invoke(context)
                            .ConfigureAwait(false);

                        if (!_consumer.EnableAutCommit)
                            _consumer.Commit();
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
