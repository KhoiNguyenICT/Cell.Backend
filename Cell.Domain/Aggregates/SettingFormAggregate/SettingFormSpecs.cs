using System;
using Cell.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Domain.Aggregates.SettingFormAggregate
{
    public static class SettingFormSpecs
    {
        public static ISpecification<SettingForm> SearchByQuery(string query) => new Specification<SettingForm>(t =>
            string.IsNullOrEmpty(query) || EF.Functions.Like(t.Name, $"%{query}%") ||
            EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<SettingForm> GetByNameSpec(string name) => new Specification<SettingForm>(t => t.Name == name);

        public static ISpecification<SettingForm> SearchByTableId(Guid? tableId) =>
            new Specification<SettingForm>(t => t.TableId == tableId);
    }
}