using System;
using System.Threading.Tasks;
using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingFieldAggregate.Models;

namespace Cell.Domain.Aggregates.SettingFieldAggregate
{
    public interface ISettingFieldRepository : IRepository<SettingField>
    {
        Task<int> Count(Guid tableId);
        Task AddColumnToBasedTable(AddColumnBasedTableModel model);
    }
}