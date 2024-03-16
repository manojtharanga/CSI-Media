using Microsoft.EntityFrameworkCore;
using Test1.Models.DBEntties;

namespace Test1.DataAccessLayer
{
    public class NumberDbContext : DbContext
    {
        public NumberDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Numbers> Numbers { get; set; }
    }
}
