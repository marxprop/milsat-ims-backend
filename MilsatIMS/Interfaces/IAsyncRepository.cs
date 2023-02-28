using System.Linq.Expressions;

namespace MilsatIMS.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(List<T> entities);
        Task<List<T>> GetAllTable();
        Task<T> UpdateAsync(T entity, T entityFromDatabase = null, bool saveChanges = true);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        IQueryable<T> GetAll();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    }
}
