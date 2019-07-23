using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingViewEntity;

namespace Cell.Service.Implementations
{
    public class SettingViewService : Service<SettingView, AppDbContext>, ISettingViewService
    {
        public SettingViewService(AppDbContext context) : base(context)
        {
        }
    }
}