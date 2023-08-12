using Microsoft.EntityFrameworkCore;
using RedisCache.Entities;

namespace RedisCache.Infra.Caching
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }
        public DbSet<MovieEntity> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MovieEntity>()
                .HasKey(t => t.Id);
        }
    }
}
