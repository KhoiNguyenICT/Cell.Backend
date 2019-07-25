using Cell.Common.Specifications;
using Microsoft.EntityFrameworkCore;
using Cell.Model.Entities;

namespace Cell.Model.Models.SettingTable
{
    public static class SettingTableSpecs
    {
        public static ISpecification<Model.Entities.SettingTableEntity.SettingTable> SearchByQuery(string query) => new Specification<Model.Entities.SettingTableEntity.SettingTable>(t =>
            string.IsNullOrEmpty(query) || EF.Functions.Like(t.Name, $"%{query}%") ||
            EF.Functions.Like(t.Description, $"%{query}%"));

        public static ISpecification<Model.Entities.SettingTableEntity.SettingTable> GetByNameSpec(string name) => new Specification<Model.Entities.SettingTableEntity.SettingTable>(t => t.Name == name);
    }
} 