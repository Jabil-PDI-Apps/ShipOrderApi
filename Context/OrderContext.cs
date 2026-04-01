using Microsoft.EntityFrameworkCore;
using ShipOrderBack.Model;

namespace ShipOrderBack.Context
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Models> Models { get; set; }
        public DbSet<OrderModel> OrderModels { get; set; }
        public DbSet<OrderModelSerial> OrderModelSerials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. Configuração da Tabela Models
            modelBuilder.Entity<Models>(entity =>
            {
                entity.ToTable("Models");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("PK_Models");
                entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
                entity.Property(e => e.LabelId).HasColumnName("Label_ID");
            });

            // 2. Configuração da Tabela OrderModel
            modelBuilder.Entity<OrderModel>(entity =>
            {
                entity.ToTable("OrderModel");
                entity.HasKey(e => e.PK_OrderModel);

                // Relacionamento com Models (FK_Model na classe OrderModel)
                entity.HasOne(d => d.Model)
                    .WithMany(p => p.OrderModels)
                    .HasForeignKey(d => d.FK_Model)
                    .HasConstraintName("FK_OrderModel_Models")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // 3. Configuração da Tabela OrderModelSerial
            modelBuilder.Entity<OrderModelSerial>(entity =>
            {
                entity.ToTable("OrderModelSerial");
                entity.HasKey(e => e.OrderModelSerial_ID);

                // Mapeamento explícito de tipos
                entity.Property(e => e.IsStuffed).HasColumnType("bit");
                entity.Property(e => e.DateCreation).HasColumnType("datetime");

                // Relacionamento com OrderModel
                entity.HasOne(d => d.OrderModel)
                    .WithMany(p => p.OrderModelSerials)
                    .HasForeignKey(d => d.FK_OrderModel)
                    .HasConstraintName("FK_OrderModelSerial_OrderModel")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}