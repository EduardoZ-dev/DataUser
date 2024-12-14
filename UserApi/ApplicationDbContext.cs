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
                    .HasMaxLength(50)
                    .HasComment("El nombre debe contener solo letras y no puede estar vacío.");
                    


                entity.Property(p => p.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasComment("El número de teléfono debe contener solo números, '+' y '-'.");


                /*entity.ToTable("Users", t =>
                {
                    // Aquí se define la restricción de formato utilizando ToTable
                    t.HasCheckConstraint("CHK_PhoneNumber_Format", 
                        "PhoneNumber NOT LIKE '%[^0-9+ -]%'");
                });*/

                // Índice para garantizar unicidad en el número de teléfono
                entity.HasIndex(p => p.PhoneNumber)
                    .IsUnique()
                    .HasDatabaseName("IX_UserData_PhoneNumber");

            });
        }
    }
}
