using Ardalis.HttpClientTestExtensions;
using Forum.Backend.Core.Extensions;
using Forum.Backend.Web;
using Forum.Backend.Web.Endpoints.AccountEndpoints;
using Forum.Backend.Web.Endpoints.UserEndpoints;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Forum.Backend.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class AccountLogin : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public AccountLogin(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsTokenGivenValidCredential()
        {
            _ = await _client.PostAsJsonAsync(CreateUserRequest.Route, SeedData.TestUser);

            var loginRequest = new LoginRequest
            {
                Email = SeedData.TestUser.Email,
                Password = SeedData.TestUser.Password
            };

            var response = await _client.PostAsJsonAsync(LoginRequest.Route, loginRequest);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<LoginResponse>();

            Assert.NotNull(model);
            Assert.NotNull(model.Token);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenInvalidEmailCredential()
        {
            var loginRequest = new LoginRequest
            {
                Email = "testnotfound@email.com",
                Password = SeedData.TestUser.Password
            };
            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

            _ = await _client.PostAndEnsureNotFound(LoginRequest.Route, content);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenInvalidPasswordCredential()
        {
            _ = await _client.PostAsJsonAsync(CreateUserRequest.Route, SeedData.TestUser);

            var loginRequest = new LoginRequest
            {
                Email = SeedData.TestUser.Email,
                Password = "badpassword"
            };

            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

            _ = await _client.PostAndEnsureNotFound(LoginRequest.Route, content);
        }
    }
}
