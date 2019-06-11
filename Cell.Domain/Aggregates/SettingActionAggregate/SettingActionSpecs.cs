using System;
using Cell.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Domain.Aggregates.SettingActionAggregate
{
    public static class SettingActionSpecs
    {
        public static ISpecification<SettingAction> SearchByQuery(string query) => new Specification<SettingAction>(t =>
            string.IsNullOrEmpty(query) || EF.Functions.Like(t.Name, $"%{query}%") ||
            EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<SettingAction> SearchByTableId(Guid tableId) =>
            new Specification<SettingAction>(t => t.TableId == tableId);

        public static ISpecification<SettingAction> GetByNameSpec(string name) => new Specification<SettingAction>(t => t.Name == name);
    }
}