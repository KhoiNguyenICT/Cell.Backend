using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingTableAggregate;

namespace Cell.Infrastructure.Repositories
{
    public class SettingTableRepository : Repository<SettingTable, AppDbContext>, ISettingTableRepository
    {
        public SettingTableRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}