using Cell.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Domain.Aggregates.SettingFeatureAggregate
{
    public static class SettingFeatureSpecs
    {
        public static ISpecification<SettingFeature> SearchByQuery(string query) => new Specification<SettingFeature>(t =>
            string.IsNullOrEmpty(query) || EF.Functions.Like(t.Name, $"%{query}%") ||
            EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<SettingFeature> GetByNameSpec(string name) => new Specification<SettingFeature>(t => t.Name == name);
    }
}