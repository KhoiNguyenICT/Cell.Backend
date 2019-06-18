using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingActionInstanceAggregate;

namespace Cell.Infrastructure.Repositories
{
    public class SettingActionInstanceRepository : Repository<SettingActionInstance, AppDbContext>, ISettingActionInstanceRepository, IAggregateRoot
    {
        public SettingActionInstanceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}