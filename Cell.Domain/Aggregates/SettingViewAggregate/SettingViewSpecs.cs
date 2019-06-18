using System;
using Cell.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Domain.Aggregates.SettingViewAggregate
{
    public class SettingViewSpecs
    {
        public static ISpecification<SettingView> SearchByQuery(string query) =>
            new Specification<SettingView>(t =>
                string.IsNullOrEmpty(query) ||
                EF.Functions.Like(t.Name, $"%{query}%") ||
                EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<SettingView> GetByNameSpec(string name) => new Specification<SettingView>(t => t.Name == name);
    }
}