using {{ ProjectName }}.API;
using {{ ProjectName }}.Client;
using Xunit.Abstractions;
using Xunit;

namespace {{ ProjectName }}.IntegrationTests;

[Collection("ApplicationCollection")]
public class {{ ProjectName }}GrpcIt(ITestOutputHelper testOutputHelper, ApplicationFixture applicationFixture)
{
    private readonly ApplicationFixture _applicationFixture = applicationFixture;
    private readonly {{ ProjectName }}Client _client = applicationFixture.GetClient();

{%- for entity_key in model.entities -%}
{%- set EntityName = entity_key | pascal_case -%}
{%- set entityName = entity_key | camel_case %}
    [Fact]
    public async void Test_Create{{ EntityName}}()
    {
        //Arrange
    
        //Act
        var createRequest = new {{ EntityName }}Dto { Name = Guid.NewGuid().ToString() };
        var response = await _client.Create{{ EntityName }}(createRequest);
        
        //Assert
        var dto = response.{{ EntityName }};
        Assert.NotNull(dto.Id);
        Assert.Equal(createRequest.Name, dto.Name);
    }
    
    [Fact]
    public async void Test_Get{{ EntityName }}s()
    {
        testOutputHelper.WriteLine("Test_Get{{ EntityName }}s");
        
        //Arrange
        var beforeTotal = (await _client.Get{{ EntityName }}s(new Get{{ EntityName }}sRequest {StartPage = 1, PageSize = 4})).TotalElements;
        
        //Act
        var createRequest = new {{ EntityName }}Dto { Name = Guid.NewGuid().ToString() };
        await _client.Create{{ EntityName }}(createRequest);
        var response = await _client.Get{{ EntityName }}s(new Get{{ EntityName }}sRequest {StartPage = 1, PageSize = 4});
        
        //Assert
        
        Assert.Equal(beforeTotal + 1, response.{{ EntityName }}s.Count);
    }
    
    [Fact]
    public async void Test_Get{{ EntityName }}()
    {
        //Arrange
        var request = new {{ EntityName }}Dto { Name = Guid.NewGuid().ToString() };
        var createResponse = await _client.Create{{ EntityName }}(request);
    
        //Act
        var response = await _client.Get{{ EntityName }}(new Get{{ EntityName }}Request {Id = createResponse.{{ EntityName }}.Id});
        
        //Assert
        var dto = response.{{ EntityName }};
        Assert.NotNull(dto.Id);
        Assert.Equal(request.Name, dto.Name);
    }
{% endfor %}
}