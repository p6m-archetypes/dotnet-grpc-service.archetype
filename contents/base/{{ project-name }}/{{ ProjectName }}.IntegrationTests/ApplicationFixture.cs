using {{ ProjectName }}.Client;
using {{ ProjectName }}.Server;

namespace {{ ProjectName }}.IntegrationTests;

public class ApplicationFixture: IDisposable
{
    private readonly {{ ProjectName }}Server _server;
    private readonly {{ ProjectName }}Client _client;
    public ApplicationFixture()
    {
        _server = new {{ ProjectName }}Server()
            .WithTempDb()
            .WithRandomPorts()
            .Start();
        
        _client = {{ ProjectName }}Client.Of(_server.getGrpcUrl());
    }
    
    public {{ ProjectName }}Client GetClient() => _client;
    public {{ ProjectName}}Server GetServer() => _server;

    public void Dispose()
    {
        _server.Stop();
    }
}

[CollectionDefinition("ApplicationCollection")]
public class ApplicationCollection : ICollectionFixture<ApplicationFixture>
{
    // This class has no code; it's just a marker for the test collection
}