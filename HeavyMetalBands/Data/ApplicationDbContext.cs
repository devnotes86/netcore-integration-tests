using HeavyMetalBands.Models;
using Microsoft.EntityFrameworkCore;

namespace HeavyMetalBands.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<BandDAO> Bands { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
    }
}
