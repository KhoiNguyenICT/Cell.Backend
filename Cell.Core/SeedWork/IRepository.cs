using Cell.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Core.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork Uow { get; }

        Task CommitAsync();

        IQueryable<T> QueryAsync(ISpecification<T> spec, string[] sorts = null);

        IQueryable<T> QueryAsync(string[] sorts = null);

        Task<IList<T>> GetManyAsync(ISpecification<T> spec, string[] sorts = null);

        Task<IList<T>> GetAllAsync(string[] sorts = null);

        Task<T> GetSingleAsync(ISpecification<T> spec, string[] sorts = null);

        Task<T> GetByIdAsync(Guid entityId);

        Task<bool> ExistsAsync(ISpecification<T> spec);

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(Guid entityId);
    }
}