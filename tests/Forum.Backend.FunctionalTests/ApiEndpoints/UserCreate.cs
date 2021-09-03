using Forum.Backend.Core.Extensions;
using Forum.Backend.Web;
using Forum.Backend.Web.Endpoints.UserEndpoints;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Forum.Backend.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class UserCreate : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UserCreate(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateAndReturnUser()
        {
            var response = await _client.PostAsJsonAsync(CreateUserRequest.Route, SeedData.TestUser);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<CreateUserResponse>();

            Assert.NotNull(model);
            Assert.NotEqual(0, model.Id);
            Assert.Equal(SeedData.TestUser.Name, model.Name);
            Assert.Equal(SeedData.TestUser.Email, model.Email);
            Assert.Equal(SeedData.TestUser.TimeZone, model.TimeZone);
        }
    }
}
