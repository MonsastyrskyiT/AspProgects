using FileAvatar.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FileAvatar.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } = null;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

                entity.HasIndex(u => u.Email)
                .IsUnique();

                entity.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
                entity.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(100);

                entity.HasQueryFilter(u => !u.IsDeleted);

            });
        }
    }
}
