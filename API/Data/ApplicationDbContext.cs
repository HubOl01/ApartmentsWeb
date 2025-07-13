using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<City> Cities { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Apartment> Apartments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Street>()
                .HasOne(s => s.City)
                .WithMany(c => c.Streets)
                .HasForeignKey(s => s.CityId);

            modelBuilder.Entity<House>()
                .HasOne(h => h.Street)
                .WithMany(s => s.Houses)
                .HasForeignKey(h => h.StreetId);

            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.House)
                .WithMany(h => h.Apartments)
                .HasForeignKey(a => a.HouseId);
        }
    }
}
