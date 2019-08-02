using Cell.Common.Constants;
using Cell.Common.Specifications;

namespace Cell.Model.Entities.SettingAdvancedEntity
{
    public static class SettingAdvancedSpecs
    {

        public static ISpecification<SettingAdvanced> GetBySettingFieldBased() =>
            new Specification<SettingAdvanced>(t => t.Code == ConfigurationKeys.SettingFieldBased);

        public static ISpecification<SettingAdvanced> GetBySettingTableApiBased() =>
            new Specification<SettingAdvanced>(t => t.Code == ConfigurationKeys.SettingApiTableBased);
    }
}