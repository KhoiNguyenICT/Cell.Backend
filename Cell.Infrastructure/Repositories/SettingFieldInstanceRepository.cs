using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingFieldInstanceAggregate;

namespace Cell.Infrastructure.Repositories
{
    public class SettingFieldInstanceRepository: Repository<SettingFieldInstance, AppDbContext>, ISettingFieldInstanceRepository
    {
        public SettingFieldInstanceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}