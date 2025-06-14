using Microsoft.EntityFrameworkCore;
using Sport.Api.Models;

namespace Sport.Api.Data
{
    public class SportDbContext : DbContext
    {
        public SportDbContext(DbContextOptions<SportDbContext> options) : base(options) { }

        public DbSet<Models.Sport> Sports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("sport");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Sport>()
                .HasKey(s => s.SportId);
        }
    }
}
