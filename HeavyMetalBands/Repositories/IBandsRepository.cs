using HeavyMetalBands.Models;

namespace HeavyMetalBands.Repositories
{
    public interface IBandsRepository
    {
        Task<IEnumerable<BandDAO>> GetAllAsync();
        Task<BandDAO> GetByIdAsync(int id);
        Task AddAsync(BandDAO band);
        Task UpdateAsync(BandDAO band);
        Task DeleteAsync(int id);
    }
}
