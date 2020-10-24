using Microsoft.EntityFrameworkCore;
using SULS.Models;

namespace SULS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Problem> Problems { get; set; }

        public DbSet<Submission> Submissions { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Submission>()
                .HasOne(u => u.User)
                .WithMany(us => us.UserSubmissions)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Submission>()
                .HasOne(p => p.Problem)
                .WithMany(ps => ps.ProblemSubmissions)
                .HasForeignKey(p => p.ProblemId);
        }
    }
}