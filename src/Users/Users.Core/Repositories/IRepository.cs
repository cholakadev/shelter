using System.Linq.Expressions;

namespace Users.Core.Repositories
{
    /// <summary>Generic repository interface.</summary>
    /// <typeparam name="T"> Entity class.</typeparam>
    public interface IRepository<T>
        where T: class
    {
        Task<T> AddAsync(T t);

        Task<ICollection<T>> AddManyAsync(ICollection<T> t);

        Task<T> UpdateAsync(T t);

        Task<ICollection<T>> UpdateManyAsync(ICollection<T> t);

        Task<int> DeleteAsync(T entity, bool isHard = false);

        IQueryable<T> GetAll(bool tracking = false);

        Task<T> FindAsync(Expression<Func<T, bool>> match, bool tracking = false);

        Task<int> SaveChangesAsync();
    }
}
