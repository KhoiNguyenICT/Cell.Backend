using System;
using Cell.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Model.Models.SettingFieldInstance
{
    public static class SettingFieldInstanceSpecs
    {
        public static ISpecification<Model.Entities.SettingFieldInstanceEntity.SettingFieldInstance> SearchByQuery(string query) =>
            new Specification<Model.Entities.SettingFieldInstanceEntity.SettingFieldInstance>(t =>
                string.IsNullOrEmpty(query) ||
                EF.Functions.Like(t.Name, $"%{query}%") ||
                EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<Model.Entities.SettingFieldInstanceEntity.SettingFieldInstance> GetByNameSpec(string name) =>
            new Specification<Model.Entities.SettingFieldInstanceEntity.SettingFieldInstance>(t => t.Name == name);

        public static ISpecification<Model.Entities.SettingFieldInstanceEntity.SettingFieldInstance> GetManyByParentId(Guid parentId) =>
            new Specification<Model.Entities.SettingFieldInstanceEntity.SettingFieldInstance>(t => t.Parent == parentId);

        public static ISpecification<Model.Entities.SettingFieldInstanceEntity.SettingFieldInstance> GetByFieldId(Guid fieldId) =>
            new Specification<Model.Entities.SettingFieldInstanceEntity.SettingFieldInstance>(t => t.FieldId == fieldId);
    }
}