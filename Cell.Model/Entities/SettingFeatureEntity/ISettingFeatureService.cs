using System.Collections.Generic;
using Cell.Common.SeedWork;

namespace Cell.Model.Entities.SettingFeatureEntity
{
    public interface ISettingFeatureService : IService<SettingFeature>
    {
        List<SettingFeature> GetTreeAsync(List<SettingFeature> settingAdvanced);
    }
}