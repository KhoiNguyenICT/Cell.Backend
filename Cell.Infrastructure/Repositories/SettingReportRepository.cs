using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingReportAggregate;

namespace Cell.Infrastructure.Repositories
{
    public class SettingReportRepository : Repository<SettingReport, AppDbContext>, ISettingReportRepository
    {
        public SettingReportRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}