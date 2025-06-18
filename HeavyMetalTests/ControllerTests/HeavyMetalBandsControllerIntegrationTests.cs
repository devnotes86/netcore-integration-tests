using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace HeavyMetalTests.ControllerTests
{
    
    public class HeavyMetalBandsControlleIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public HeavyMetalBandsControlleIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetById_Returns200_WhenBandExists()
        {
            var response = await _client.GetAsync("/HeavyMetalBands/Details/1");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
