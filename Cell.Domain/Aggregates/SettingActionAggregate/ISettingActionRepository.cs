using System;
using System.Threading.Tasks;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SettingActionAggregate
{
    public interface ISettingActionRepository : IRepository<SettingAction>
    {
        Task<int> Count(Guid tableId);
    }
}