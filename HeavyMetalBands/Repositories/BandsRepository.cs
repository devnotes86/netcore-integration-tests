using HeavyMetalBands.Data;
using HeavyMetalBands.Models;
using Microsoft.EntityFrameworkCore;

namespace HeavyMetalBands.Repositories
{
    public class BandsRepository : IBandsRepository
    {
        private readonly DbContext_Read _readContext;
        private readonly DbContext_Write _writeContext;

        public BandsRepository(DbContext_Read readContext, DbContext_Write writeContext)
        {
            _readContext = readContext;
            _writeContext = writeContext;
        }

        public async Task<IEnumerable<BandDAO>> GetAllAsync() =>
            await _readContext.Bands.ToListAsync();

        public async Task<BandDAO> GetByIdAsync(int id) =>
            await _readContext.Bands.FindAsync(id);

        public async Task AddAsync(BandDAO band)
        {
            _writeContext.Bands.Add(band);
            await _writeContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(BandDAO band)
        {
            _writeContext.Bands.Update(band);
            await _writeContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var band = await _writeContext.Bands.FindAsync(id);
            if (band != null)
            {
                _writeContext.Bands.Remove(band);
                await _writeContext.SaveChangesAsync();
            }
        }
    }
}
