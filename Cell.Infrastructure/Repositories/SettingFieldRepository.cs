using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Infrastructure.Repositories
{
    public class SettingFieldRepository : Repository<SettingField, AppDbContext>, ISettingFieldRepository
    {
        public SettingFieldRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> Count(Guid tableId)
        {
            var result = await _dbContext.SettingFields.Where(x => x.TableId == tableId).CountAsync();
            return result;
        }
    }
}