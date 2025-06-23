using HeavyMetalBands.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace HeavyMetalTests.ControllerTests
{
    
    public class HeavyMetalBandsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public HeavyMetalBandsControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Details_Test1()
        {
            var response = await _client.GetAsync("/HeavyMetalBands/Details/1"); 
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Status: {response.StatusCode}");
            Console.WriteLine($"Body: {content}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Index_Test1()
        {
            var response = await _client.GetAsync("/HeavyMetalBands");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_Test1()
        {
            // Arrange: Create form data for a valid BandDTO
            var formData = new Dictionary<string, string>
                {
                    { "BandName", "Test Band" }, 
                    { "YearCreated", "1980" },
                    { "BandNameUppercase", "TEST BAND" }
                };

             
            var content = new FormUrlEncodedContent(formData);

            // Act
            var response = await _client.PostAsync("/HeavyMetalBands/Create", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); 
        }
    }
}
