using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingAdvancedEntity;

namespace Cell.Service.Implementations
{
    public class SettingAdvancedService : Service<SettingAdvanced, AppDbContext>, ISettingAdvancedService
    {
        public SettingAdvancedService(AppDbContext context) : base(context)
        {
        }
    }
}