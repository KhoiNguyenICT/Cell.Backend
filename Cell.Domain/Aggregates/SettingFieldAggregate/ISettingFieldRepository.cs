using System;
using System.Threading.Tasks;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SettingFieldAggregate
{
    public interface ISettingFieldRepository : IRepository<SettingField>
    {
        Task<int> Count(Guid tableId);
    }
}