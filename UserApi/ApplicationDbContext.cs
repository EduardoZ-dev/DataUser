using Microsoft.EntityFrameworkCore;
using UserApi.Entities;

namespace UserApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserData> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserData>(entity =>
            {
                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
