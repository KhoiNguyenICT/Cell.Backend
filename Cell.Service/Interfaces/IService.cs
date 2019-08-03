using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cell.Service.Interfaces
{
    public interface IService<T> where T : class
    {
        Task CreateAsync(T entity);

        Task<T> GetAsync(Guid id);

        Task<IList<T>> GetAsync();

        Task<IList<T>> GetAsync(IEnumerable<Guid> entityIds);

        Task UpdateAsync(T entity);

        Task UpdateAsync(IEnumerable<T> entities);

        Task DeleteAsync(Guid id);

        Task DeleteAsync(IEnumerable<T> entities);

        IQueryable<T> Queryable(Expression<Func<T, bool>> filterExpression = null);
    }
}
