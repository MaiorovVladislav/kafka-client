namespace KafkaClient;

public class SecurityInformation
{
    public SecurityProtocol SecurityProtocol { get; set; }

    public string SslKeyLocation { get; set; } = default!;

    public string SslKeyPassword { get; set; } = default!;

    public string SslCaLocation { get; set; } = default!;
}