using ShipOrderBack.Model;
using Microsoft.EntityFrameworkCore;

namespace ShipOrderBack.Context
{
    public class JemsDbContext: DbContext
    {
        public JemsDbContext(DbContextOptions<JemsDbContext> options) : base(options)
        {

        }
        public DbSet<WpWip> WpWips { get; set; }
        public DbSet<EpContainerWipLink> ContainerWipLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<EpContainerWipLink>(entity =>
            {
                entity.ToTable("EP_ContainerWipLink");

                
                entity.HasKey(c => new { c.Container_ID, c.Wip_ID });

               
                entity.HasOne(cl => cl.WpWip)
                      .WithMany(w => w.ContainerLinks) 
                      .HasForeignKey(cl => cl.Wip_ID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            
            modelBuilder.Entity<WpWip>(entity =>
            {
                entity.ToTable("WP_Wip"); 
                entity.HasKey(w => w.Wip_ID);
            });
        }
    }
}
