using SharedKernel.Infrastructure.Repositories;
using System.Linq.Expressions;
using Users.Infrastructure.Domain;

namespace Users.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmail(Expression<Func<User, bool>> match);
    }
}
