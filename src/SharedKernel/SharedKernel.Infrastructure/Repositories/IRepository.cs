using System.Linq.Expressions;

namespace SharedKernel.Infrastructure.Repositories
{
    /// <summary>Generic repository interface.</summary>
    /// <typeparam name="T"> Entity class.</typeparam>
    public interface IRepository<T>
        where T: class
    {
        Task<T> AddAsync(T t, CancellationToken cancellationToken = default);

        Task<ICollection<T>> AddManyAsync(ICollection<T> t, CancellationToken cancellationToken = default);

        Task<T> UpdateAsync(T t, CancellationToken cancellationToken = default);

        Task<ICollection<T>> UpdateManyAsync(ICollection<T> t, CancellationToken cancellationToken = default);

        Task<int> DeleteAsync(T entity, CancellationToken cancellationToken = default, bool isHard = false);

        IQueryable<T> GetAll(bool tracking = false);

        Task<T> FindAsync(Expression<Func<T, bool>> match, bool tracking = false);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
