using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Infrastructure.Database
{
    public abstract class BaseDbContext<T> : DbContext
        where T : DbContext
    {
        protected BaseDbContext(DbContextOptions<T> options)
            : base(options)
        {
        }
    }
}
