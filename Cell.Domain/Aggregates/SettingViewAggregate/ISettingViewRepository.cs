using System;
using System.Threading.Tasks;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SettingViewAggregate
{
    public interface ISettingViewRepository : IRepository<SettingView>
    {
        Task<int> Count(Guid tableId);
    }
}