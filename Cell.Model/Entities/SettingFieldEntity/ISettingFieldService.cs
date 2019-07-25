using System;
using System.Threading.Tasks;
using Cell.Common.SeedWork;

namespace Cell.Model.Entities.SettingFieldEntity
{
    public interface ISettingFieldService : IService<SettingField>
    {
        Task<int> CountAsync(Guid tableId);
        Task AddColumnToBasedTable(AddColumnBasedTableModel model);
    }
}