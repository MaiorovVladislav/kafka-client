using Microsoft.Extensions.Hosting;

namespace KafkaClient.Extensions;

public class KafkaClientHostedService : BackgroundService
{
    private readonly KafkaClientManager _manager;

    public KafkaClientHostedService(KafkaClientManager manager)
    {
        _manager = manager;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _manager.StartAsync(stoppingToken);
    }
}