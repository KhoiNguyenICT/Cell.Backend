using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingFieldInstanceEntity;

namespace Cell.Service.Implementations
{
    public class SettingFieldInstanceService : Service<SettingFieldInstance, AppDbContext>, ISettingFieldInstanceService
    {
        public SettingFieldInstanceService(AppDbContext context) : base(context)
        {
        }
    }
}