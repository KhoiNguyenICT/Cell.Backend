using System;
using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingAdvancedEntity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Service.Implementations
{
    public class SettingAdvancedService : Service<SettingAdvanced, AppDbContext>, ISettingAdvancedService
    {
        public SettingAdvancedService(AppDbContext context) : base(context)
        {
        }

        public List<SettingAdvanced> GetTreeAsync(List<SettingAdvanced> settingAdvanced)
        {
            var result = BuildTree(null, settingAdvanced);
            return result;
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