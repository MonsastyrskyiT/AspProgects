using PracticAsp11102026.Models;
namespace PracticAsp11102026.Data
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<AppDbContext> options) : base(options) { }

        public Microsoft.EntityFrameworkCore.DbSet<Project> Projects { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<TaskItem> TaskItems { get; set; }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasMany(p => p.Tasks)
                    .WithOne(t => t.Project)
                    .HasForeignKey(t => t.ProjectId)
                    .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

                entity.HasQueryFilter(p => !p.IsDeleted);
            });

            modelBuilder.Entity<TaskItem>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                // enum хранится в БД строкой (Low, High...), а не числом
                entity.Property(t => t.Priority).HasConversion<string>();
                entity.Property(t => t.Status).HasConversion<string>();

                entity.HasQueryFilter(t => !t.IsDeleted);
            });
        }
    }
}
