using System;
using Cell.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Model.Entities.SettingActionEntity
{
    public static class SettingActionSpecs
    {
        public static ISpecification<SettingAction> SearchByQuery(string query) => new Specification<SettingAction>(t =>
            string.IsNullOrEmpty(query) || EF.Functions.Like(t.Name, $"%{query}%") ||
            EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<SettingAction> SearchByTableId(Guid tableId) =>
            new Specification<SettingAction>(t => t.TableId == tableId);
    }
}