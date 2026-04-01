using ShipOrderBack.Model;
using Microsoft.EntityFrameworkCore;

namespace ShipOrderBack.Context
{
    public class EpsDbContext: DbContext
    {
        public EpsDbContext(DbContextOptions<EpsDbContext> options) : base(options)
        {
        }

        public DbSet<ValidationLabelHistoryArcon> LabelHistories { get; set; }
    }
}
