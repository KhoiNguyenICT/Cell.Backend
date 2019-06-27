using Cell.Core.Extensions;
using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingAdvancedAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Infrastructure.Repositories
{
    public class SettingAdvancedRepository : Repository<SettingAdvanced, AppDbContext>, ISettingAdvancedRepository
    {
        public SettingAdvancedRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AnyAsync()
        {
            return await _dbContext.SettingAdvanceds.AnyAsync();
        }

        public async Task<List<SettingAdvanced>> GetTreeAsync()
        {
            var settingAdvancedResults = await _dbContext.SettingAdvanceds.ToListAsync();
            var result = BuildTree(null, settingAdvancedResults.To<List<SettingAdvanced>>());
            return result;
        }

        public void RemoveNode(Guid id)
        {
            throw new NotImplementedException();
        }

        private List<SettingAdvanced> BuildTree(Guid? settingAdvancedParentId, List<SettingAdvanced> source)
        {
            return source.Where(item =>
                (settingAdvancedParentId == null && (item.Parent == Guid.Empty)) ||
                (item.Parent == settingAdvancedParentId)).Select(settingFeature => new SettingAdvanced
                {
                    Id = settingFeature.Id,
                    Name = settingFeature.Name,
                    Code = settingFeature.Code,
                    Description = settingFeature.Description,
                    Created = settingFeature.Created,
                    Modified = settingFeature.Modified,
                    Children = BuildTree(settingFeature.Id, source).ToList(),
                }).ToList();
        }
    }
}