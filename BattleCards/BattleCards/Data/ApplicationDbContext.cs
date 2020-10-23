using BattleCards.Models;
using Microsoft.EntityFrameworkCore;

namespace BattleCards.Data
{

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<User> UsersCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserCard>()
                .HasKey(x => new { x.UserId, x.CardId });

            modelBuilder.Entity<UserCard>()
                .HasOne(u => u.User)
                .WithMany(uc => uc.UserCards)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserCard>()
                .HasOne(c => c.Card)
                .WithMany(uc => uc.UsersCard)
                .HasForeignKey(c => c.CardId);
        }
    }
}