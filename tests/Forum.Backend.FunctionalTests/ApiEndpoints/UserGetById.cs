using Ardalis.HttpClientTestExtensions;
using Forum.Backend.Web;
using Forum.Backend.Web.Endpoints.UserEndpoints;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Forum.Backend.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class UserGetById : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public UserGetById(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsSeedProjectGivenId1()
        {
            var result = await _client.GetAndDeserialize<GetUserByIdResponse>(GetUserByIdRequest.BuildRoute(1));

            Assert.Equal(1, result.Id);
            Assert.Equal(SeedData.TestUser.Name, result.Name);
            Assert.Equal(SeedData.TestUser.Email, result.Email);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenId0()
        {
            string route = GetUserByIdRequest.BuildRoute(0);
            _ = await _client.GetAndEnsureNotFound(route);
        }
    }
}
