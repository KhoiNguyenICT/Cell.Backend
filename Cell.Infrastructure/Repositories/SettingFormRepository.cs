using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingFormAggregate;

namespace Cell.Infrastructure.Repositories
{
    public class SettingFormRepository : Repository<SettingForm, AppDbContext>, ISettingFormRepository
    {
        public SettingFormRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}