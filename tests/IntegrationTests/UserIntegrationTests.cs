using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using tests.Infrastructure;
using UserCrudApi.Dto;
using UserCrudApi.Users.Model;
using Xunit;

namespace tests.IntegrationTests;

public class UserIntegrationTests:IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UserIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/User/create";
        var user = new CreateUserRequest() { Name = "new name", Email = "new email", Role="new role" };
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<User>(responseString);

        Assert.NotNull(result);
        Assert.Equal(user.Name, result.Name);
        Assert.Equal(user.Email, result.Email);
        Assert.Equal(user.Role, result.Role);

    }
    
    [Fact]
    public async Task Post_Create_UserAlreadyExists_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/User/create";
        var user = new CreateUserRequest() { Name = "new name", Email = "new email", Role="new role" };
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        
    }

    [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/User/create";
        var user = new CreateUserRequest() { Name = "new name", Email = "new email", Role="new role" };
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<User>(responseString)!;

        request = "/api/v1/User/update/"+result.Id;
        var updateUser = new UpdateUserRequest()
            { Name = "updated name", Email = "updated email", Role = "updated role" };
        content = new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json");

        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<User>(responseString)!;

        Assert.Equal(updateUser.Name, result.Name);
        Assert.Equal(updateUser.Email, result.Email);
        Assert.Equal(updateUser.Role, result.Role);
    }

     [Fact]
    public async Task Put_Update_UserDoesNotExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/User/update/1";
        var updateUser = new UpdateUserRequest()
            { Name = "updated name", Email = "updated email", Role = "updated role" };
        var content = new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }

    [Fact]
    public async Task Delete_Delete_UserExists_ReturnsDeletedUser()
    {

        var request = "/api/v1/User/create";
        var user = new CreateUserRequest() { Name = "new name", Email = "new email", Role = "new role" };
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<User>(responseString)!;

        request = "/api/v1/User/delete/" + result.Id;

        response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted,response.StatusCode);
    }

    [Fact]
    public async Task Delete_Delete_UserDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/User/delete/66";

        var response = await _client.DeleteAsync(request);

        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
        
    }

    [Fact]
    public async Task Get_GetByName_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/User/create";
        var user = new CreateUserRequest() { Name = "new name", Email = "new email", Role="new role" };
        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<User>(responseString)!;

        request = "/api/v1/User/name/" + result.Name;

        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        

    }

    [Fact]
    public async Task Get_GetByName_UserDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/User/name/test";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    
    
}