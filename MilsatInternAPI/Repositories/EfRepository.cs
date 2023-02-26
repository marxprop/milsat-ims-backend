using Microsoft.EntityFrameworkCore;
using MilsatInternAPI.Data;
using MilsatInternAPI.Interfaces;
using System.Linq.Expressions;

namespace MilsatInternAPI.Repositories
{
    public class EfRepository<T> : IAsyncRepository<T> where T : class
    {
        private readonly MilsatInternAPIContext _dbContext;
        public DbSet<T> Table;
        public EfRepository(MilsatInternAPIContext dbContext)
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
    }
}
