using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingFieldEntity;

namespace Cell.Service.Implementations
{
    public class SettingFieldService : Service<SettingField, AppDbContext>, ISettingFieldService
    {
        public SettingFieldService(AppDbContext context) : base(context)
        {
        }
    }
}