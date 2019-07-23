using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cell.Common.Specifications;

namespace Cell.Common.SeedWork
{
    public interface IService<T> where T : class
    {
        Task CommitAsync();

        IQueryable<T> QueryAsync(ISpecification<T> spec, string[] sorts = null);

        Task<IList<T>> GetManyAsync(ISpecification<T> spec, string[] sorts = null);

        Task<T> GetSingleAsync(ISpecification<T> spec, string[] sorts = null);

        Task<T> GetByIdAsync(Guid entityId);

        Task<bool> ExistsAsync(ISpecification<T> spec);

        Task<T> AddAsync(T entity);

        void Update(T entity);

        void Delete(Guid id);
    }
}