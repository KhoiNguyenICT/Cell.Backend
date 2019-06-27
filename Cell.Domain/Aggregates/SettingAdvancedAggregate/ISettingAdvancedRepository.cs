using System;
using Cell.Core.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cell.Domain.Aggregates.SettingAdvancedAggregate
{
    public interface ISettingAdvancedRepository : IRepository<SettingAdvanced>
    {
        Task<bool> AnyAsync();
        Task<List<SettingAdvanced>> GetTreeAsync();
        void RemoveNode(Guid id);
    }
}