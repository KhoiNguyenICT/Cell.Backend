using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingFieldAggregate;

namespace Cell.Infrastructure.Repositories
{
    public class SettingFieldRepository : Repository<SettingField, AppDbContext>, ISettingFieldRepository
    {
        public SettingFieldRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}