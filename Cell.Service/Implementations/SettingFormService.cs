using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingFormEntity;

namespace Cell.Service.Implementations
{
    public class SettingFormService : Service<SettingForm, AppDbContext>, ISettingFormService
    {
        public SettingFormService(AppDbContext context) : base(context)
        {
        }
    }
}