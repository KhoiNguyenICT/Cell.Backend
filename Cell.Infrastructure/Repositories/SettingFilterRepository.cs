using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingFilterAggregate;

namespace Cell.Infrastructure.Repositories
{
    public class SettingFilterRepository : Repository<SettingFilter, AppDbContext>, ISettingFilterRepository
    {
        public SettingFilterRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}