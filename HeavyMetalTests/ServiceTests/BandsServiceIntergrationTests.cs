using Autofac;
using HeavyMetalBands.Models;
using HeavyMetalBands.Services;
using AutoMapper;
using HeavyMetalBands.Maping;

namespace HeavyMetalTests.ServiceTests
{
    public class BandsServiceIntegrationTests
    {
        private readonly IContainer _container;

        public BandsServiceIntegrationTests()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<TestModule>();
            _container = builder.Build();
        }

        [Fact]
        public void BandProfile_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BandProfile>();
            });

            config.AssertConfigurationIsValid(); // throws if mappings are broken
        }

        [Fact]
        public async Task AddAndGetBand_ReturnsCorrectData()
        {
            // Resolve the real service
            using var scope = _container.BeginLifetimeScope();
            var service = scope.Resolve<IBandsService>();

            var bandDto = new BandDTO { Id = 1, BandName = "Behemoth" };
            await service.AddAsync(bandDto);

            var allBands = await service.GetAllAsync();

            Assert.Single(allBands);
            Assert.Equal("Behemoth", allBands.First().BandName);
        }
    }
}
