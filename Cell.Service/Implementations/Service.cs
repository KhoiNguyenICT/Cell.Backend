using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cell.Model;
using Cell.Model.Entities;
using Cell.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cell.Service.Implementations
{
    public class Service<T> : IService<T> where T : Entity
    {
        private readonly AppDbContext _context;

        public async Task CreateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = _context.Set<T>().Find(id);
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            var entity = await _context.Set<T>()
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IList<T>> GetAsync()
        {
            var entities = await _context.Set<T>()
                .AsNoTracking()
                .ToListAsync();
            return entities;
        }

        public async Task<IList<T>> GetAsync(IEnumerable<Guid> entityIds)
        {
            var entities = await _context.Set<T>()
                .AsNoTracking()
                .Where(x => entityIds.Contains(x.Id))
                .ToListAsync();
            return entities;
        }

        public IQueryable<T> Queryable(Expression<Func<T, bool>> filterExpression = null)
        {
            if (filterExpression == null) return _context.Set<T>().AsNoTracking();
            return _context.Set<T>().AsNoTracking().Where(filterExpression);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
    }
}
