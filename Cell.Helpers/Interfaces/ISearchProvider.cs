using System;
using System.Threading.Tasks;
using Cell.Helpers.Models;

namespace Cell.Helpers.Interfaces
{
    public interface ISearchProvider
    {
        Task<object> ExecuteSearch(DynamicSearchModel searchModel);
        Task<object> GetSingleQuery(string tableName, Guid id);
    }
}