using System;
using System.Threading.Tasks;
using Cell.Common.SeedWork;

namespace Cell.Model.Entities.SettingActionEntity
{
    public interface ISettingActionService : IService<SettingAction>
    {
        Task<int> CountAsync(Guid tableId);
    }
}