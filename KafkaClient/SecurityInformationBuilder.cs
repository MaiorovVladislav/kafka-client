namespace KafkaClient;

public class SecurityInformationBuilder
{
    private readonly SecurityInformation _securityInformation = new()
    {
        SecurityProtocol = SecurityProtocol.Ssl
    };

    public SecurityInformation Build()
    {
        return _securityInformation;
    }
    
    public SecurityInformationBuilder AddSslKeyLocation(string sslKeyLocation)
    {
        _securityInformation.SslKeyLocation = sslKeyLocation;
        
        return this;
    }

    public SecurityInformationBuilder AddSslKeyPassword(string sslKeyPassword)
    {
        _securityInformation.SslKeyPassword = sslKeyPassword;
        
        return this;
    }

    public SecurityInformationBuilder AddSslCaLocation(string sslCaLocation)
    {
        _securityInformation.SslCaLocation = sslCaLocation;
        
        return this;
    }
}