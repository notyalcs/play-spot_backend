using Microsoft.EntityFrameworkCore;
using PlaySpotApi.Models;

namespace PlaySpotApi.Data
{
    public class PlaySpotDbContext : DbContext
    {
        public PlaySpotDbContext(DbContextOptions<PlaySpotDbContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Fullness> Fullness { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Sports)
                .WithMany();

            modelBuilder.Entity<Fullness>()
                .HasOne(f => f.Location)
                .WithMany(l => l.Fullness);
        }
    }
}

