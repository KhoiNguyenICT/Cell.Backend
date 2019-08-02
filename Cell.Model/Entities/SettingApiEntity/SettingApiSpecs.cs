using System;
using Cell.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Cell.Model.Entities.SettingApiEntity
{
    public static class SettingApiSpecs
    {
        public static ISpecification<SettingApi> SearchByQuery(string query) => new Specification<SettingApi>(t =>
            string.IsNullOrEmpty(query) || EF.Functions.Like(t.Name, $"%{query}%") ||
            EF.Functions.Like(t.Name, $"%{query}%"));

        public static ISpecification<SettingApi> SearchByTableId(Guid? tableId) =>
            new Specification<SettingApi>(t => t.TableId == tableId);
    }
}