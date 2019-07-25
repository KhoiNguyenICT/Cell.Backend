using Cell.Common.Specifications;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cell.Model.Models.SettingForm
{
    public static class SettingFormSpecs
    {
        public static ISpecification<Model.Entities.SettingFormEntity.SettingForm> SearchByQuery(string query) => new Specification<Model.Entities.SettingFormEntity.SettingForm>(t =>
            string.IsNullOrEmpty(query) || EF.Functions.Like(t.Name, $"%{query}%") ||
            EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<Model.Entities.SettingFormEntity.SettingForm> SearchByTableId(Guid? tableId) =>
            new Specification<Model.Entities.SettingFormEntity.SettingForm>(t => t.TableId == tableId);
    }
}