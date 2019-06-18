using System;
using System.Linq;
using System.Threading.Tasks;
using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingViewAggregate;
using Microsoft.EntityFrameworkCore;

namespace Cell.Infrastructure.Repositories
{
    public class SettingViewRepository : Repository<SettingView, AppDbContext>, ISettingViewRepository
    {
        public SettingViewRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> Count(Guid tableId)
        {
            return await _dbContext.SettingViews.Where(x => x.TableId == tableId).CountAsync();
        }
    }
}