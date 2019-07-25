using Cell.Common.SeedWork;
using System.Collections.Generic;

namespace Cell.Model.Entities.SettingAdvancedEntity
{
    public interface ISettingAdvancedService : IService<SettingAdvanced>
    {
        List<SettingAdvanced> GetTreeAsync(List<SettingAdvanced> settingAdvanced);
    }
}