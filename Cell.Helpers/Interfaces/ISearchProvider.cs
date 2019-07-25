using System;
using Cell.Helpers.Models;

namespace Cell.Helpers.Interfaces
{
    public interface ISearchProvider
    {
        string GetSearchQuery(DynamicSearchModel dynamicSearchModel);
        string GetSingleQuery(string tableId, Guid id);
    }
}