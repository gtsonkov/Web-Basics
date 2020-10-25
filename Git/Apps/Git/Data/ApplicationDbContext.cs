namespace Git.Data
{
    using Git.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Repository> Repositories { get; set; }

        public DbSet<Commit> Commits { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DbConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Repository>()
                .HasOne(o => o.Owner)
                .WithMany(r => r.Repositories)
                .HasForeignKey(o => o.OwnerId);

            modelBuilder.Entity<Commit>()
                .HasOne(r => r.Repository)
                .WithMany(c => c.Commits)
                .HasForeignKey(r => r.RepositoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Commit>()
                .HasOne(c => c.Creator)
                .WithMany(u => u.Commits)
                .HasForeignKey(c => c.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }
    }
}