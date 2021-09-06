using Ardalis.HttpClientTestExtensions;
using Forum.Backend.Web;
using Forum.Backend.Web.Endpoints.UserEndpoints;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        private void SetJwtToken()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", MockJwtTokens.GenerateJwtToken(null));
        }

        [Fact]
        public async Task ReturnsUnauthorizedGivenNoJwt()
        {
            var result = await _client.GetAsync(GetUserByIdRequest.BuildRoute(1));

            Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async Task ReturnsSeedUserGivenId1()
        {
            SetJwtToken();

            var result = await _client.GetAndDeserialize<GetUserByIdResponse>(GetUserByIdRequest.BuildRoute(1));

            Assert.Equal(1, result.Id);
            Assert.Equal(SeedData.TestUserGet.Name, result.Name);
            Assert.Equal(SeedData.TestUserGet.Email, result.Email);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenId0()
        {
            SetJwtToken();
            string route = GetUserByIdRequest.BuildRoute(0);
            _ = await _client.GetAndEnsureNotFound(route);
        }
    }
}
