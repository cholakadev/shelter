using Microsoft.EntityFrameworkCore;
using SharedKernel.Infrastructure.Database;
using Users.Infrastructure.Domain;

namespace Users.Infrastructure.Database
{
    public sealed class UsersDbContext : BaseDbContext<UsersDbContext>
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserCredentials> UserCredentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
