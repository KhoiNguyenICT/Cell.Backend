using System;
using System.Collections.Generic;
using System.Linq;
using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingAdvancedEntity;
using Cell.Model.Entities.SettingFeatureEntity;

namespace Cell.Service.Implementations
{
    public class SettingFeatureService : Service<SettingFeature, AppDbContext>, ISettingFeatureService
    {
        public SettingFeatureService(AppDbContext context) : base(context)
        {
        }

        public List<SettingFeature> GetTreeAsync(List<SettingFeature> settingAdvanced)
        {
            var result = BuildTree(null, settingAdvanced);
            return result;
        }

        private List<SettingFeature> BuildTree(Guid? parentId, List<SettingFeature> source)
        {
            return source.Where(item =>
                (parentId == null && (item.Parent == Guid.Empty)) ||
                (item.Parent == parentId)).Select(settingFeature => new SettingFeature
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