using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingTableEntity;

namespace Cell.Service.Implementations
{
    public class SettingTableService : Service<SettingTable, AppDbContext>, ISettingTableService
    {
        public SettingTableService(AppDbContext context) : base(context)
        {
        }
    }
}