using Microsoft.Extensions.DependencyInjection;

namespace KafkaClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKafkaHostedService(this IServiceCollection services,
        Func<KafkaClientManagerBuilder, KafkaClientManager> kafkaClientManagerBuilder)
    {
        return services
            .AddKafka(kafkaClientManagerBuilder)
            .AddHostedService<KafkaClientHostedService>();
    }

    private static IServiceCollection AddKafka(this IServiceCollection services,
        Func<KafkaClientManagerBuilder, KafkaClientManager> kafkaClientManagerBuilder)
    {
        var builder = new KafkaClientManagerBuilder();

        var kafkaClientManager = kafkaClientManagerBuilder.Invoke(builder);

        return services.AddSingleton(kafkaClientManager);
    }
}