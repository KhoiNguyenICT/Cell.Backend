using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingFeatureEntity;

namespace Cell.Service.Implementations
{
    public class SettingFeatureService : Service<SettingFeature, AppDbContext>, ISettingFeatureService
    {
        public SettingFeatureService(AppDbContext context) : base(context)
        {
        }
    }
}