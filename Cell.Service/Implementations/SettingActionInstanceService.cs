using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingActionInstanceEntity;

namespace Cell.Service.Implementations
{
    public class SettingActionInstanceService : Service<SettingActionInstance, AppDbContext>, ISettingActionInstanceService
    {
        public SettingActionInstanceService(AppDbContext context) : base(context)
        {
        }
    }
}