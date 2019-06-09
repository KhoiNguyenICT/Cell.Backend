using Cell.Core.Linq;
using Cell.Core.Repositories;
using Cell.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Core.SeedWork
{
    public abstract class Repository<T, TDbContext> : IRepository<T> where T : class, IAggregateRoot where TDbContext : BaseDbContext
    {
        public IUnitOfWork Uow => _dbContext;
        protected TDbContext _dbContext { get; }

        protected Repository(TDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
        }

        public async Task CommitAsync()
        {
            await Uow.SaveChangesAsync();
        }

        public IQueryable<T> QueryAsync(ISpecification<T> spec, string[] sorts = null)
        {
            return _dbContext.Set<T>().Where(spec.Predicate).SortBy(sorts ?? GetDefaultSorts());
        }

        public IQueryable<T> QueryAsync(string[] sorts = null)
        {
            return _dbContext.Set<T>().Where(t => true).SortBy(sorts ?? GetDefaultSorts());
        }

        public async Task<IList<T>> GetManyAsync(ISpecification<T> spec, string[] sorts = null)
        {
            return await _dbContext.Set<T>().Where(spec.Predicate).SortBy(sorts ?? GetDefaultSorts()).ToListAsync();
        }

        public async Task<IList<T>> GetAllAsync(string[] sorts = null)
        {
            return await _dbContext.Set<T>().Where(t => true).SortBy(sorts ?? GetDefaultSorts()).ToListAsync();
        }

        public async Task<T> GetSingleAsync(ISpecification<T> spec, string[] sorts = null)
        {
            return await _dbContext.Set<T>().Where(t => true).SortBy(sorts ?? GetDefaultSorts()).FirstOrDefaultAsync(spec.Predicate);
        }

        public async Task<T> GetByIdAsync(Guid entityId)
        {
            return await _dbContext.Set<T>().FindAsync(entityId);
        }

        public async Task<bool> ExistsAsync(ISpecification<T> spec)
        {
            return await _dbContext.Set<T>().AnyAsync(spec.Predicate);
        }

        public T Add(T entity)
        {
            return _dbContext.Set<T>().Add(entity).Entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Guid entityId)
        {
            var entity = _dbContext.Find<T>(entityId);
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        protected virtual string[] GetDefaultSorts()
        {
            return new[] { "-created" };
        }
    }
}