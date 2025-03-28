using Grpc.Core;
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
    public async Task Test_Create{{ EntityName}}()
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
    public async Task Test_Get{{ EntityName | pluralize }}()
    {
        testOutputHelper.WriteLine("Test_Get{{ EntityName | pluralize }}");
        
        //Arrange
        var beforeTotal = (await _client.Get{{ EntityName | pluralize }}(new Get{{ EntityName | pluralize }}Request {StartPage = 1, PageSize = 4})).TotalElements;
        
        //Act
        var createRequest = new {{ EntityName }}Dto { Name = Guid.NewGuid().ToString() };
        await _client.Create{{ EntityName }}(createRequest);
        var response = await _client.Get{{ EntityName | pluralize }}(new Get{{ EntityName | pluralize }}Request {StartPage = 1, PageSize = 4});
        
        //Assert
        
        Assert.Equal(beforeTotal + 1, response.TotalElements);
    }
    
    [Fact]
    public async Task Test_Get{{ EntityName }}()
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

    [Fact]
    public async Task Test_Update{{ EntityName }}()
    {
        //Arrange
        var request = new {{ EntityName }}Dto { Name = Guid.NewGuid().ToString() };
        var createResponse = await _client.Create{{ EntityName }}(request);
    
        //Act
        var response = await _client.Update{{ EntityName }}(new {{ EntityName }}Dto() {Id = createResponse.{{ EntityName }}.Id, Name = "Updated"});
        
        //Assert
        var dto = response.{{ EntityName }};
        Assert.NotNull(dto.Id);
        Assert.Equal("Updated", response.{{ EntityName }}.Name);
    }
    
    [Fact]
    public async Task Test_Delete{{ EntityName }}()
    {
        //Arrange
        var request = new {{ EntityName }}Dto { Name = Guid.NewGuid().ToString() };
        var createResponse = await _client.Create{{ EntityName }}(request);
    
        //Act
        var response = await _client.Delete{{ EntityName }}(new Delete{{ EntityName }}Request{Id = createResponse.{{ EntityName }}.Id});
        
        //Assert
        Assert.True(response.Deleted);
    }

    [Fact]
    public async Task Test_Delete{{ EntityName }}_NotFound()
    {
        //Arrange

        //Act
        var exception = await Assert.ThrowsAsync<RpcException>(async () => 
        {
            await _client.Delete{{ EntityName }}(new Delete{{ EntityName }}Request{Id = Guid.NewGuid().ToString()});
        });
       
        //Assert
        Assert.Equal(StatusCode.NotFound, exception.StatusCode);
        Assert.Equal("{{ EntityName }} not found", exception.Status.Detail);
    }
{% endfor %}
}