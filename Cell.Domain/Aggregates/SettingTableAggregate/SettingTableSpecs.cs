
using System.Text.RegularExpressions;
using Cell.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Domain.Aggregates.SettingTableAggregate
{
    public static class SettingTableSpecs
    {
        public static ISpecification<SettingTable> SearchByQuery(string query) => new Specification<SettingTable>(t =>
            string.IsNullOrEmpty(query) || EF.Functions.Like(t.Name, $"%{query}%") ||
            EF.Functions.Like(t.Description, $"%{query}%"));
        public static ISpecification<SettingTable> GetByNameSpec(string name) => new Specification<SettingTable>(t => t.Name == name);
    }
}