using Microsoft.EntityFrameworkCore;
using ProductApi.Data.Models;

namespace ProductApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }   // ← новая строка

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(p => p.Description).HasMaxLength(500);
                entity.Property(p => p.Quantity).IsRequired();
                entity.Property(p => p.CreatedDate).IsRequired();
                entity.Property(p => p.IsDeleted).IsRequired();
            });

            // PRACTIC
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.CustomerName).IsRequired().HasMaxLength(100);
                entity.Property(o => o.Status).IsRequired().HasMaxLength(20);
                entity.Property(o => o.TotalPrice).HasPrecision(18, 2);
            });
        }
    }
}