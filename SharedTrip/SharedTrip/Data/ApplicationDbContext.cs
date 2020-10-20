using Microsoft.EntityFrameworkCore;
using SharedTrip.Models;

namespace SharedTrip
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Trip> Trips { get; set; }

        public DbSet<UserTrip> UserTrip { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrip>()
                .HasKey(k => new { k.UserId, k.TripId });

            modelBuilder.Entity<UserTrip>()
                .HasOne(u => u.User)
                .WithMany(t => t.UserTrips)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserTrip>()
                .HasOne(t => t.Trip)
                .WithMany(u => u.UserTrips)
                .HasForeignKey(t => t.TripId);

            base.OnModelCreating(modelBuilder);
        }
    }
}