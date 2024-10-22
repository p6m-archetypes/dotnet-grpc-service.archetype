using {{ ProjectName }}.API;
using {{ ProjectName }}.Client;
using Xunit.Abstractions;
using Xunit;

namespace {{ ProjectName }}.IntegrationTests;

public class {{ ProjectName }}GrpcIT
{
    private readonly ITestOutputHelper _testOutputHelper;
    // private  {{ ProjectName }}Client _client = {{ ProjectName }}Client.Of("http://localhost:5030");

    public {{ ProjectName }}GrpcIT(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    // [Fact]
    // public async void Test_GetUsers()
    // {
    //     _testOutputHelper.WriteLine("Start");
    //     
    //
    //     var reply = await _client.GetUsers(new GetUsersRequest {StartPage = 1, PageSize = 4});
    //     _testOutputHelper.WriteLine("reply: " + reply.Users);
    //     
    //     Assert.Equal(4, reply.Users.Count);
    // }
    //
    // [Fact]
    // public async void Test_CreateUser()
    // {
    //     //Arrange
    //     var client = {{ ProjectName }}Client.Of("http://localhost:5030");
    //
    //     //Act
    //     var request = new UserDto { Name = Guid.NewGuid().ToString() };
    //     var response = await client.CreateUser(request);
    //     
    //     //Assert
    //     var userDto = response.User;
    //     Assert.NotNull(userDto.Id);
    //     Assert.Equal(request.Name, userDto.Name);
    // }
    //
    // [Fact]
    // public async void Test_GetUser()
    // {
    //     //Arrange
    //     var client = {{ ProjectName }}Client.Of("http://localhost:5030");
    //
    //     //Act
    //     var request = new UserDto { Name = Guid.NewGuid().ToString() };
    //     var createResponse = await client.CreateUser(request);
    //     var response = await client.GetUser(new GetUserRequest {Id = createResponse.User.Id});
    //     
    //     //Assert
    //     var userDto = response.User;
    //     Assert.NotNull(userDto.Id);
    //     Assert.Equal(request.Name, userDto.Name);
    // }
    
}