using System;
using System.Threading.Tasks;
using Cell.Helpers.Models;

namespace Cell.Model.Entities.DynamicEntity
{
    public interface IDynamicService
    {
        Task<object> SearchAsync(DynamicSearchModel dynamicSearchModel);
        Task<object> GetSingleAsync(Guid tableId, Guid id);
        Task InsertAsync(WriteModel writeModel);
        Task UpdateAsync(WriteModel writeModel);
        Task DeleteAsync(Guid tableId, Guid id);
    }
}