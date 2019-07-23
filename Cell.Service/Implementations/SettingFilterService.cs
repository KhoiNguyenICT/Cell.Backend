using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingFilterEntity;

namespace Cell.Service.Implementations
{
    public class SettingFilterService : Service<SettingFilter, AppDbContext>, ISettingFilterService
    {
        public SettingFilterService(AppDbContext context) : base(context)
        {
        }
    }
}