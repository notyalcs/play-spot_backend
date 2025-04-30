using Microsoft.EntityFrameworkCore;
using PlaySpotApi.Models;

namespace PlaySpotApi.Data
{
    public class PlaySpotDbContext : DbContext
    {
        public PlaySpotDbContext(DbContextOptions<PlaySpotDbContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<LocationSport> LocationSports { get; set; }
        public DbSet<LocationActivity> LocationActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite key for LocationSport
            modelBuilder.Entity<LocationSport>()
                .HasKey(ls => new { ls.LocationId, ls.SportId });

            // Configuring the relationships for LocationSport
            modelBuilder.Entity<LocationSport>()
                .HasOne(ls => ls.Location)
                .WithMany(l => l.LocationSports)
                .HasForeignKey(ls => ls.LocationId);

            modelBuilder.Entity<LocationSport>()
                .HasOne(ls => ls.Sport);
        }
    }
}

