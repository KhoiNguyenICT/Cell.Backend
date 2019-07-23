using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cell.Common.Linq;
using Cell.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Common.SeedWork
{
    public class Service<T, TDbContext> : IService<T> where T : class where TDbContext : DbContext
    {
        private TDbContext Context { get; }

        public Service(TDbContext context)
        {
            Context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await Context.Set<T>().AddAsync(entity);
            return result.Entity;
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Delete(Guid id)
        {
            var entity = Context.Find<T>(id);
            Context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task<bool> ExistsAsync(ISpecification<T> spec)
        {
            return await Context.Set<T>().AnyAsync(spec.Predicate);
        }

        public async Task<T> GetByIdAsync(Guid entityId)
        {
            return await Context.Set<T>().FindAsync(entityId);
        }

        public async Task<IList<T>> GetManyAsync(ISpecification<T> spec, string[] sorts = null)
        {
            return await Context.Set<T>().Where(spec.Predicate).SortBy(sorts ?? GetDefaultSorts()).ToListAsync();
        }

        public async Task<T> GetSingleAsync(ISpecification<T> spec, string[] sorts = null)
        {
            return await Context.Set<T>().Where(t => true).SortBy(sorts ?? GetDefaultSorts()).FirstOrDefaultAsync(spec.Predicate);
        }

        public IQueryable<T> QueryAsync(ISpecification<T> spec, string[] sorts = null)
        {
            return Context.Set<T>().Where(t => true).SortBy(sorts ?? GetDefaultSorts());
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        private string[] GetDefaultSorts()
        {
            return new[] { "-created" };
        }
    }
}