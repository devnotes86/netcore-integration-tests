using HeavyMetalBands.Controllers;
using HeavyMetalBands.Models;
using HeavyMetalBands.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HeavyMetalTests.ControllerTests
{
    
    public class HeavyMetalBandsControllerUnitTests
    {
        [Fact]
        public async Task Details_ReturnsBandDTO_WhenBandExists()
        {
            // Arrange
            var mock_BandsService = new Mock<IBandsService>();
            var testBand = new BandDTO { Id = 1, BandName = "Test Band" };

            mock_BandsService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(testBand);
            var controller = new HeavyMetalBandsController(mock_BandsService.Object);

            // Act
            var result = await controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<BandDTO>(viewResult.Model);
            Assert.Equal("Test Band", model.BandName); 
        }

        [Fact]
        public async Task Index_ReturnsListOfBandDTO()
        {
            // Arrange
            var mock_BandsService = new Mock<IBandsService>();
            var bandsCollection = new List<BandDTO> {
                    new BandDTO { Id = 1, BandName = "Iron Maiden" },
                    new BandDTO { Id = 2, BandName = "Judas Priest" }
                };

            mock_BandsService.Setup(s => s.GetAllAsync()).ReturnsAsync(bandsCollection);
            var controller = new HeavyMetalBandsController(mock_BandsService.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<List<BandDTO>>(viewResult.Model);
            Assert.Equal(2, model.Count);
            Assert.Equal("Iron Maiden", model.Find(x => x.Id == 1).BandName);
            Assert.Equal("Judas Priest", model.Find(x => x.Id == 2).BandName);
        }


    }
}
