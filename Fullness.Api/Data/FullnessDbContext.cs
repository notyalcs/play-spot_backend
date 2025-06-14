using Microsoft.EntityFrameworkCore;
using Fullness.Api.Models;

namespace Fullness.Api.Data
{
    public class FullnessDbContext : DbContext
    {
        public FullnessDbContext(DbContextOptions<FullnessDbContext> options) : base(options) { }

        public DbSet<Models.Fullness> Fullness { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("fullness");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Fullness>()
                .HasKey(f => f.FullnessId);
        }
    }
}
