using System;
using System.Linq;
using System.Text;
using Cell.Helpers.Extensions;
using Cell.Helpers.Interfaces;
using Cell.Helpers.Models;

namespace Cell.Helpers.Providers
{
    public class SqlSearchProvider : ISearchProvider
    {
        public string GetSingleQuery(string tableName, Guid id)
        {
            return $"SELECT TOP(1) * FROM {tableName} WHERE ID = '{id}'".Trim();
        }

        public string GetSearchQuery(DynamicSearchModel dynamicSearchModel)
        {
            var query = new StringBuilder();
            query.AppendLine(GetSelectStatement(dynamicSearchModel));
            query.AppendLine(GetFromStatement(dynamicSearchModel));


            return query.ToString().Trim();
        }
        private static string GetSelectStatement(DynamicSearchModel dynamicSearchModel)
        {
            if (dynamicSearchModel.Select?.Any() != true)
                throw new Exception("Select model required");

            return "SELECT " + dynamicSearchModel
                    .Select
                    .Select(t => $"{t.Table}.[{t.Field}] {(string.IsNullOrEmpty(t.Alias) ? "" : $"[{t.Alias}]")}")
                    .JoinString(",");
        }

        private static string GetFromStatement(DynamicSearchModel dynamicSearchModel)
        {
            if (dynamicSearchModel.From?.Any() != true)
                throw new Exception("From model required");

            return "FROM " + dynamicSearchModel
                .From
                .Select((t, index) =>
                {
                    if (string.IsNullOrEmpty(t.Table))
                        throw new Exception("From table required");
                    if (index > 0 && string.IsNullOrEmpty(t.JoinTable))
                        throw new Exception("Join table required");
                    if (string.IsNullOrEmpty(t.JoinTable) && t.JoinConditions?.Any() != true)
                        throw new Exception("Join conditions required");

                    if (string.IsNullOrEmpty(t.JoinTable)) return t.Table;
                    var ret = new StringBuilder(index == 0 ? $"{t.Table} {t.JoinClause} {t.JoinTable}" : $"{t.JoinClause} {t.JoinTable}");
                    ret.AppendLine($" ON {t.JoinConditions.Select(x => $"{t.Table}.[{x.Field}]={t.JoinTable}.[{x.JoinField}]").JoinString(" AND ")}");

                    return ret.ToString().Trim();
                })
                .JoinString(Environment.NewLine);
        }
    }
}