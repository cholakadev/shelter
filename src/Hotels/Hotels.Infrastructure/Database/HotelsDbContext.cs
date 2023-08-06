using Microsoft.EntityFrameworkCore;
using SharedKernel.Infrastructure.Database;

namespace Hotels.Infrastructure.Database
{
    public class HotelsDbContext : BaseDbContext<HotelsDbContext>
    {
        public HotelsDbContext(DbContextOptions<HotelsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
