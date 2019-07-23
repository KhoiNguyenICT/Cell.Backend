using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingActionEntity;

namespace Cell.Service.Implementations
{
    public class SettingActionService : Service<SettingAction, AppDbContext>, ISettingActionService
    {
        public SettingActionService(AppDbContext context) : base(context)
        {
        }
    }
}