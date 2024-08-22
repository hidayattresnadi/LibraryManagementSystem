using System.Linq.Expressions;


namespace LibrarySystem.Application.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(IEnumerable<T> entities);
        bool Remove(T entity);
        bool RemoveRange(IEnumerable<T> entities);
        Task<int> SaveAsync();
    }
}
