using System;
using System.Threading.Tasks;
using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingFieldEntity;
using Cell.Model.Entities.SettingFieldInstanceEntity;

namespace Cell.Service.Implementations
{
    public class SettingFieldInstanceService : Service<SettingFieldInstance, AppDbContext>, ISettingFieldInstanceService
    {
        public SettingFieldInstanceService(AppDbContext context) : base(context)
        {
        }

        public async Task<SettingFieldInstance> GetByFieldId(Guid fieldId)
        {
            var spec = SettingFieldInstanceSpecs.GetByFieldId(fieldId);
            var settingField = await GetSingleAsync(spec);
            return settingField;
        }
    }
}