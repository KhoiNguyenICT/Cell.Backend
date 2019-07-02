using Cell.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cell.Domain.Aggregates.SettingReportAggregate
{
    public class SettingReportSpecs
    {
        public static ISpecification<SettingReport> SearchByQuery(string query) => new Specification<SettingReport>(t =>
            string.IsNullOrEmpty(query) || EF.Functions.Like(t.Name, $"%{query}%") ||
            EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<SettingReport> GetByNameSpec(string name) => new Specification<SettingReport>(t => t.Name == name);

        public static ISpecification<SettingReport> SearchByTableId(Guid? tableId) =>
            new Specification<SettingReport>(t => t.TableId == tableId);
    }
}