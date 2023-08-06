using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Users.Core.Domain.Contracts;
using Users.Core.Repositories;
using Users.Infrastructure.Database;

namespace Users.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected UsersDbContext _context;

        public Repository(UsersDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> AddAsync(TEntity t, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Add(t);
            await _context.SaveChangesAsync(cancellationToken);
            return t;
        }

        public virtual async Task<ICollection<TEntity>> AddManyAsync(ICollection<TEntity> t, CancellationToken cancellationToken = default)
        {
            await _context.Set<TEntity>().AddRangeAsync(t);
            await _context.SaveChangesAsync(cancellationToken);
            return t;
        }

        public virtual async Task<int> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default, bool isHard = false)
        {
            if (entity is IActivatable activatable && !isHard)
            {
                activatable.Active = false;
                _context.Set<TEntity>().Update(entity);
            }
            else
            {
                _context.Set<TEntity>().Remove(entity);
            }

            return await SaveChangesAsync(cancellationToken);
        }

        public virtual Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, bool tracking = false)
        {
            return tracking ?
                _context.Set<TEntity>().SingleOrDefaultAsync(match) :
                _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(match);
        }

        public IQueryable<TEntity> GetAll(bool tracking = false)
            => tracking ? _context.Set<TEntity>() : _context.Set<TEntity>().AsNoTracking();

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);

        public virtual async Task<TEntity> UpdateAsync(TEntity t, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().Update(t);
            await _context.SaveChangesAsync(cancellationToken);
            return t;
        }

        public virtual async Task<ICollection<TEntity>> UpdateManyAsync(ICollection<TEntity> t, CancellationToken cancellationToken = default)
        {
            _context.Set<TEntity>().UpdateRange(t);
            await _context.SaveChangesAsync(cancellationToken);
            return t;
        }
    }
}
