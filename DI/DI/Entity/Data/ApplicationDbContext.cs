using DI.Entity.Model;
using Microsoft.EntityFrameworkCore;

namespace DI.Entity.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(p => p.Sku)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.HasIndex(p => p.Sku)
                    .IsUnique();

                entity.Property(p => p.Price)
                    .HasPrecision(18, 2);
            });
        }
    }
}
