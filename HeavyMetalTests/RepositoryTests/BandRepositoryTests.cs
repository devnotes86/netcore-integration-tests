using HeavyMetalBands.Models;
using HeavyMetalBands.Data;
using Microsoft.EntityFrameworkCore;
using HeavyMetalBands.Repositories;

namespace HeavyMetalTests.ServiceTests
{
    public class BandsRepositoryTests
    {
        private DbContext_Read CreateReadContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DbContext_Read>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new DbContext_Read(options);
            context.Bands.AddRange(
                new BandDAO { id = 1, band_name = "Metallica" },
                new BandDAO { id = 2, band_name = "Iron Maiden" }
            );
            context.SaveChanges();
            return context;
        }

        private DbContext_Write CreateWriteContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<DbContext_Write>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new DbContext_Write(options);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllBands()
        {
            var dbName = nameof(GetAllAsync_ReturnsAllBands);
            var readContext = CreateReadContext(dbName);
            var writeContext = CreateWriteContext(dbName); // same db name

            var repo = new BandsRepository(readContext, writeContext);

            var bands = await repo.GetAllAsync();

            Assert.Equal(2, bands.Count());
            Assert.Contains(bands, b => b.band_name == "Metallica");
        }

        [Fact]
        public async Task AddAsync_AddsBandToDatabase()
        {
            var dbName = nameof(AddAsync_AddsBandToDatabase);
            var readContext = CreateReadContext(dbName);
            var writeContext = CreateWriteContext(dbName);

            var repo = new BandsRepository(readContext, writeContext);

            var newBand = new BandDAO { id = 3, band_name = "Slayer" };
            await repo.AddAsync(newBand);

            var bandInDb = await writeContext.Bands.FindAsync(3);
            Assert.NotNull(bandInDb);
            Assert.Equal("Slayer", bandInDb.band_name);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesBand()
        {
            var dbName = nameof(UpdateAsync_UpdatesBand);
            var readContext = CreateReadContext(dbName);
            var writeContext = CreateWriteContext(dbName); 

              
            writeContext.Bands.Add(new BandDAO { id = 3, band_name = "KAT" });
            await writeContext.SaveChangesAsync();

            var repo = new BandsRepository(readContext, writeContext);

            var updatedBand = new BandDAO { id = 3, band_name = "KAT z Romanem" };
            await repo.UpdateAsync(updatedBand);


            var band = await readContext.Bands.FindAsync(3);
            Assert.Equal("KAT z Romanem", band.band_name);
            Assert.Equal(3, band.id);

             
        }

        [Fact]
        public async Task DeleteAsync_RemovesBand()
        {
            var dbName = nameof(DeleteAsync_RemovesBand);
            var readContext = CreateReadContext(dbName);
            var writeContext = CreateWriteContext(dbName);

            writeContext.Bands.Add(new BandDAO { id = 5, band_name = "Britney Spears" });
            await writeContext.SaveChangesAsync();

            var repo = new BandsRepository(readContext, writeContext);
            await repo.DeleteAsync(5);

            var deletedBandById = await writeContext.Bands.FindAsync(5);
            Assert.Null(deletedBandById);


            var allBands = await readContext.Bands.ToListAsync();
            var bandByName = allBands.Find(x => x.band_name == "Britney Spears"); 
            Assert.Null(bandByName);



        }
    }
}
