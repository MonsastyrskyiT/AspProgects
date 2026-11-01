using Microsoft.EntityFrameworkCore;
using UserManagerShop_practic.Entities;

namespace UserManagerShop_practic.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(256);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
                entity.HasQueryFilter(u => !u.IsDeleted);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(150);
                entity.Property(p => p.Sku).IsRequired().HasMaxLength(12);
                entity.HasIndex(p => p.Sku).IsUnique();
                entity.Property(p => p.Price).HasPrecision(18, 2);
                entity.HasQueryFilter(p => !p.IsDeleted);
            });
        }
    }
}
