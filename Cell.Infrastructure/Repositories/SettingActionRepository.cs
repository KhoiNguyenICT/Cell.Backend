using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingActionAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Infrastructure.Repositories
{
    public class SettingActionRepository : Repository<SettingAction, AppDbContext>, ISettingActionRepository
    {
        public SettingActionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> Count(Guid tableId)
        {
            var result = await _dbContext.SettingActions.Where(x => x.TableId == tableId).CountAsync();
            return result;
        }
    }
}