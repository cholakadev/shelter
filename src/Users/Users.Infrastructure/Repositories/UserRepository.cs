using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Users.Core.Repositories;
using Users.Infrastructure.Database;
using Users.Infrastructure.Domain;

namespace Users.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(UsersDbContext context)
            : base(context)
        {
        }

        public async Task<User?> GetUserByEmail(Expression<Func<User, bool>> match)
            => await _context.Set<User>()
                             .Include(x => x.Credentials)
                             .SingleOrDefaultAsync(match);
    }
}
