namespace KafkaClient;

public interface IConsumerWorker
{
    Task StartAsync(CancellationToken cancellationToken);
}