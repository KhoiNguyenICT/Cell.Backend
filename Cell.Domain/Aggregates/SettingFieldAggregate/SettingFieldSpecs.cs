﻿using System;
using Cell.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Domain.Aggregates.SettingFieldAggregate
{
    public static class SettingFieldSpecs
    {
        public static ISpecification<SettingField> SearchByQuery(string query) =>
            new Specification<SettingField>(t =>
                string.IsNullOrEmpty(query) ||
                EF.Functions.Like(t.Name, $"%{query}%") ||
                EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<SettingField> SearchByTableId(Guid tableId) =>
            new Specification<SettingField>(t => t.TableId == tableId);

        public static ISpecification<SettingField> GetByNameSpec(string name) => new Specification<SettingField>(t => t.Name == name);
    }
}