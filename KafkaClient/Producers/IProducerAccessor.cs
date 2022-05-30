namespace KafkaClient.Producers;

public interface IProducerAccessor
{
    Producer? GetProducer(string producerName);
}

public class ProducerAccessor : IProducerAccessor
{
    private readonly Dictionary<string, Producer> producers;

    public ProducerAccessor(IEnumerable<Producer> producers)
    {
        this.producers = producers.ToDictionary(x => x.ProducerName);
    }

    public Producer? GetProducer(string name) =>
        this.producers.TryGetValue(name, out var consumer) ? consumer : null;
}