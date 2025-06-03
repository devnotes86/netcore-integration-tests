using HeavyMetalBands.Models;


using AutoMapper;
using HeavyMetalBands.Maping;

namespace HeavyMetalTests.MappingTests
{
    public class BandMappingTests
    {
        private readonly IMapper _mapper;

        public BandMappingTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BandProfile>();
            });

            config.AssertConfigurationIsValid();
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Should_Map_BandDTO_To_BandDAO()
        {
            // Arrange
            var dto = new BandDTO
            {
                Id = 1,
                BandName = "Rammstein",
                YearCreated = 1994,
                BandNameUppercase = "RAMMSTEIN"
            };
             
            // Act
            var dao = _mapper.Map<BandDAO>(dto);

            // Assert
            Assert.Equal(dto.Id, dao.id);
            Assert.Equal(dto.BandName, dao.band_name);
            Assert.Equal(dto.YearCreated, dao.year_created); 

            
        }

        [Fact]
        public void Should_Map_BandDAO_To_BandDTO()
        {
            // Arrange
            var dao = new BandDAO
            {
                id = 2,
                band_name = "Saxon",
                year_created = 1976
            };

            // Act
            var dto = _mapper.Map<BandDTO>(dao);

            // Assert
            Assert.Equal(dao.id, dto.Id);
            Assert.Equal(dao.band_name, dto.BandName);
            Assert.Equal(dao.year_created, dto.YearCreated);
            Assert.Equal("SAXON", dto.BandNameUppercase);
        }
    }
}