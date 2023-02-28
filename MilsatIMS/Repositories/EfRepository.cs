using Microsoft.EntityFrameworkCore;
using MilsatIMS.Data;
using MilsatIMS.Interfaces;
using System.Linq.Expressions;

namespace MilsatIMS.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly MilsatIMSContext _dbContext;
        public DbSet<T> Table;
        public EfRepository(MilsatIMSContext dbContext)
        {
            _dbContext = dbContext;
            Table = _dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return GetAllIncluding();
        }

        public async Task<List<T>> GetTableByOrder(Expression<Func<T, object>> orderBy = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (orderBy != null)
            {
                query = query.OrderByDescending(orderBy);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)  
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity, T entityFromDatabase = null, bool saveChanges = true)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _ = entities.Select(x => { _dbContext.Entry(x).State = EntityState.Modified; return x; });
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors)
        {
            var query = Table.AsQueryable();
            return propertySelectors.Aggregate(query, (current, propertySelectors) => current.Include(propertySelectors));
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (predicate != null)
            {
                return await query.Where(predicate).CountAsync();
            }
            return 0;
        }
    }
}
