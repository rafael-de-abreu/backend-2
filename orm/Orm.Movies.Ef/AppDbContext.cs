using Microsoft.EntityFrameworkCore;
using Orm.Movies.DataModels;

namespace Orm.Movies.Ef
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
