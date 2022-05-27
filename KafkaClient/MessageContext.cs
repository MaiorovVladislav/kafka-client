namespace KafkaClient;

public class MessageContext : IMessageContext
{
    public MessageContext(string key, CloudEvent value)
    {
        Key = key;
        Value = value;
    }

    public string Key { get; set; }
    public CloudEvent Value { get; set; }
}