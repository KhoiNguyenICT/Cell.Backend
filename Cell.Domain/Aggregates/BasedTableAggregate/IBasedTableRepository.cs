using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cell.Domain.Aggregates.BasedTableAggregate
{
    public interface IBasedTableRepository
    {
        Task<string> CreateBasedTable(CreateBasedTable model);

        Task<List<SearchBasedTable>> SearchBasedTable();

        Task<List<string>> SearchUnusedColumnFromBasedTable(Guid tableId);

        Task DropBasedTable(string table);

        Task<List<string>> SearchColumnFromBasedTable(string tableName);
    }
}