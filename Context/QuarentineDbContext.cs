using Microsoft.EntityFrameworkCore;
using ShipOrderBack.Model;

namespace ShipOrderBack.Context
{
    public class QuarentineDbContext : DbContext
    {
        public QuarentineDbContext(DbContextOptions<QuarentineDbContext> options) : base(options)
        {
        }

        public DbSet<Quarentine> SerialCheckeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quarentine>(entity =>
            {
                // ALTERE DE "ProductQuarentine" PARA "SerialCheckeds"
                entity.ToTable("SerialCheckeds", "dbo");

                entity.HasKey(e => e.Id);

                // Mapeando datetime2
                entity.Property(e => e.DatePackout).HasColumnType("datetime2");
                entity.Property(e => e.DateRead).HasColumnType("datetime2");

                // Restante das configurações...
                entity.Property(e => e.SerialNumber).IsRequired();
                entity.Property(e => e.TimePassed).IsRequired();
                entity.Property(e => e.UserReader).IsRequired();
                entity.Property(e => e.Message).IsRequired();
                entity.Property(e => e.Status).IsRequired();
            });
        }
    }

}

