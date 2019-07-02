using System;
using Cell.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Domain.Aggregates.SettingFilterAggregate
{
    public class SettingFilterSpecs
    {
        public static ISpecification<SettingFilter> SearchByQuery(string query) => new Specification<SettingFilter>(t =>
            string.IsNullOrEmpty(query) || EF.Functions.Like(t.Name, $"%{query}%") ||
            EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<SettingFilter> GetByNameSpec(string name) => new Specification<SettingFilter>(t => t.Name == name);

        public static ISpecification<SettingFilter> SearchByTableId(Guid? tableId) =>
            new Specification<SettingFilter>(t => t.TableId == tableId);
    }
}