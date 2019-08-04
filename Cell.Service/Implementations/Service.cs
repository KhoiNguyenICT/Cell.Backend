using Cell.Common.SeedWork;
using Cell.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cell.Service.Implementations
{
    public class Service<T> : IService<T> where T : Entity
    {
        protected AppDbContext Context;

        public async Task CreateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = Context.Set<T>().Find(id);
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Context.Entry(entity).State = EntityState.Deleted;
            }

            await Context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            var entity = await Context.Set<T>()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<IList<T>> GetAsync()
        {
            var entities = await Context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
            return entities;
        }

        public async Task<IList<T>> GetAsync(IEnumerable<Guid> entityIds)
        {
            var entities = await Context.Set<T>()
                .AsNoTracking()
                .Where(x => entityIds.Contains(x.Id))
                .ToListAsync();
            return entities;
        }

        public IQueryable<T> Queryable(Expression<Func<T, bool>> filterExpression = null)
        {
            if (filterExpression == null) return Context.Set<T>().AsNoTracking();
            return Context.Set<T>().AsNoTracking().Where(filterExpression);
        }

        public async Task UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
            await Context.SaveChangesAsync();
        }
    }
}