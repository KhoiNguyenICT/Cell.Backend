using System;
using Cell.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Domain.Aggregates.SettingActionInstanceAggregate
{
    public class SettingActionInstanceSpecs
    {
        public static ISpecification<SettingActionInstance> SearchByQuery(string query) =>
            new Specification<SettingActionInstance>(t =>
                string.IsNullOrEmpty(query) ||
                EF.Functions.Like(t.Name, $"%{query}%") ||
                EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<SettingActionInstance> GetManyByParentId(Guid parentId) =>
            new Specification<SettingActionInstance>(t => t.Parent == parentId);
    }
}