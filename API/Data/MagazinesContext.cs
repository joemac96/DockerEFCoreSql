using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MagazinesContext : DbContext
    {
        public MagazinesContext(DbContextOptions<MagazinesContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Magazine>()
                .HasData(
                    new Magazine { MagazineId = 1, Name = "MSDN Magazine" },
                    new Magazine { MagazineId = 2, Name = "Docker Magazine" },
                    new Magazine { MagazineId = 3, Name = "EFCore Magazine" });
        }

        public DbSet<Magazine>? Magazine { get; set; }
    }
}
