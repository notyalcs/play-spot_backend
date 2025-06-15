using Microsoft.EntityFrameworkCore;
using Location.Api.Models;

namespace Location.Api.Data
{
    public class LocationDbContext : DbContext
    {
        public LocationDbContext(DbContextOptions<LocationDbContext> options) : base(options) { }

        public DbSet<Models.Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("location");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Location>()
                .HasKey(l => l.LocationId);
        }
    }
}
