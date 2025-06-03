using HeavyMetalBands.Models;
using Microsoft.EntityFrameworkCore;

namespace HeavyMetalBands.Data
{
    public class DbContext_Write : ApplicationDbContext
    {
        public DbContext_Write(DbContextOptions<DbContext_Write> options) : base(options) { } 
    }
}
