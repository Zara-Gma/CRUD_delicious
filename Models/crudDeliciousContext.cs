using Microsoft.EntityFrameworkCore;

namespace crudDelicious.Models
{
    public class crudDeliciousContext : DbContext
    {
        public crudDeliciousContext(DbContextOptions options) : base(options) { }
        public DbSet<Dish> Dishes { get; set; }
    }
}