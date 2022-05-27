namespace KafkaClient.Consumers;

public interface IConsumerWorker
{
    Task StartAsync(CancellationToken cancellationToken);
}