using Microsoft.EntityFrameworkCore;

namespace HeavyMetalBands.Data
{
    public class DbContext_Read : ApplicationDbContext
    {
        public DbContext_Read(DbContextOptions<DbContext_Read> options) : base(options) { } 
    }
}
