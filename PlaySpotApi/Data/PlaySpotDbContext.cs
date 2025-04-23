using Microsoft.EntityFrameworkCore;
using PlaySpotApi.Models;

namespace PlaySpotApi.Data
{
    public class PlaySpotDbContext : DbContext
    {
        public PlaySpotDbContext(DbContextOptions<PlaySpotDbContext> options) : base(options)
        {
        }

        public DbSet<VenueItem> VenueItems { get; set; } // DbSet for VenueItem

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Configure VenueItem table (e.g., table name, constraints)
            modelBuilder.Entity<VenueItem>(entity =>
            {
                entity.ToTable("VenueItems"); // Table name in the database
                entity.HasKey(v => v.Id); // Primary key
                entity.Property(v => v.Name).HasMaxLength(100); // Optional: Limit Name length
                entity.Property(v => v.Address).HasMaxLength(200); // Optional: Limit Location length
            });
        }
    }
}

