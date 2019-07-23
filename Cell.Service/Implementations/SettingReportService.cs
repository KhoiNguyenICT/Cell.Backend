using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingReportEntity;

namespace Cell.Service.Implementations
{
    public class SettingReportService : Service<SettingReport, AppDbContext>, ISettingReportService
    {
        public SettingReportService(AppDbContext context) : base(context)
        {
        }
    }
}