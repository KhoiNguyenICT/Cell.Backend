using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingActionEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Service.Implementations
{
    public class SettingActionService : Service<SettingAction, AppDbContext>, ISettingActionService
    {
        public SettingActionService(AppDbContext context) : base(context)
        {
        }

        public async Task<int> CountAsync(Guid tableId)
        {
            var result = await Context.SettingActions.Where(x => x.TableId == tableId).CountAsync();
            return result;
        }
    }
}