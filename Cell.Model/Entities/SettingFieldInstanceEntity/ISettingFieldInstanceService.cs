using Cell.Common.SeedWork;
using System;
using System.Threading.Tasks;

namespace Cell.Model.Entities.SettingFieldInstanceEntity
{
    public interface ISettingFieldInstanceService : IService<SettingFieldInstance>
    {
        Task<SettingFieldInstance> GetByFieldId(Guid fieldId);
    }
}