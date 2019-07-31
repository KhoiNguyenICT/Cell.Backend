using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingApiEntity;

namespace Cell.Service.Implementations
{
    public class SettingApiService : Service<SettingApi, AppDbContext>, ISettingApiService
    {
        public SettingApiService(AppDbContext context) : base(context)
        {
        }
    }
}