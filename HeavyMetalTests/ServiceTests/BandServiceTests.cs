using HeavyMetalBands.Services;
using Moq;
using HeavyMetalBands.Repositories;
using AutoMapper;
using FluentAssertions;
using HeavyMetalBands.Models;

namespace HeavyMetalTests.ServiceTests
{
    public class BandServiceTests
    {
        private readonly Mock<IBandsRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly BandsService _service;

        public BandServiceTests()
        {
            _mockRepo = new Mock<IBandsRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new BandsService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedDTOs()
        {
            // Arrange
            var bandEntities = new List<BandDAO>
        {
            new BandDAO { id = 1, band_name = "Rammstein" },
            new BandDAO { id = 2, band_name = "Helloween" }
        };

            var bandDTOs = new List<BandDTO>
        {
            new BandDTO { Id = 1, BandName = "Rammstein" },
            new BandDTO { Id = 2, BandName = "Helloween" }
        };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(bandEntities);
            _mockMapper.Setup(m => m.Map<List<BandDTO>>(bandEntities)).Returns(bandDTOs);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(bandDTOs);
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryWithMappedEntity()
        {
            // Arrange
            var bandDTO = new BandDTO { Id = 1, BandName = "Gamma Ray" };
            var bandDAO = new BandDAO { id = 1, band_name = "Gamma Ray" };

            _mockMapper.Setup(m => m.Map<BandDAO>(bandDTO)).Returns(bandDAO);

            // Act
            await _service.AddAsync(bandDTO);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(bandDAO), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedDTO()
        {
            var bandDAO = new BandDAO { id = 1, band_name = "Iron MaIden" };
            var bandDTO = new BandDTO { Id = 1, BandName = "Iron MaIden" };

            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(bandDAO);
            _mockMapper.Setup(m => m.Map<BandDTO>(bandDAO)).Returns(bandDTO);

            var result = await _service.GetByIdAsync(1);

            result.Should().BeEquivalentTo(bandDTO);
        }

        [Fact]
        public async Task UpdateAsync_CallsRepositoryWithMappedEntity()
        {
            var bandDTO = new BandDTO { Id = 2, BandName = "DIO" };
            var bandDAO = new BandDAO { id = 2, band_name = "DIO" };

            _mockMapper.Setup(m => m.Map<BandDAO>(bandDTO)).Returns(bandDAO);

            await _service.UpdateAsync(bandDTO);

            _mockRepo.Verify(r => r.UpdateAsync(bandDAO), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_DeletesBandById()
        {
            await _service.DeleteAsync(42);
            _mockRepo.Verify(r => r.DeleteAsync(42), Times.Once);
        }
    }
}
