using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingActionAggregate;

namespace Cell.Infrastructure.Repositories
{
    public class SettingActionRepository: Repository<SettingAction, AppDbContext>, ISettingActionRepository
    {
        public SettingActionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}