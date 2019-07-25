using Cell.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Model.Models.SettingView
{
    public static class SettingViewSpecs
    {
        public static ISpecification<Model.Entities.SettingViewEntity.SettingView> SearchByQuery(string query) =>
            new Specification<Model.Entities.SettingViewEntity.SettingView>(t =>
                string.IsNullOrEmpty(query) ||
                EF.Functions.Like(t.Name, $"%{query}%") ||
                EF.Functions.Like(t.Name, $"%{query}%"));
    }
}