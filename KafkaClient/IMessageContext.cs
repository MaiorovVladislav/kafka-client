namespace KafkaClient;


public interface IMessageContext
{
    string Key { get; set; }
    
    CloudEvent Value { get; set; }
}