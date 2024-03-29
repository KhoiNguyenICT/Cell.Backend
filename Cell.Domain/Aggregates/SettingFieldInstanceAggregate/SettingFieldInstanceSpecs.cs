﻿using System;
using Cell.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Domain.Aggregates.SettingFieldInstanceAggregate
{
    public class SettingFieldInstanceSpecs
    {
        public static ISpecification<SettingFieldInstance> SearchByQuery(string query) =>
            new Specification<SettingFieldInstance>(t =>
                string.IsNullOrEmpty(query) ||
                EF.Functions.Like(t.Name, $"%{query}%") ||
                EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<SettingFieldInstance> GetByNameSpec(string name) =>
            new Specification<SettingFieldInstance>(t => t.Name == name);

        public static ISpecification<SettingFieldInstance> GetManyByParentId(Guid parentId) =>
            new Specification<SettingFieldInstance>(t => t.Parent == parentId);

        public static ISpecification<SettingFieldInstance> GetByFieldId(Guid fieldId) =>
            new Specification<SettingFieldInstance>(t => t.FieldId == fieldId);
    }
}