using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cell.Helpers.Extensions;
using Cell.Helpers.Interfaces;
using Cell.Helpers.Models;
using Dapper;

namespace Cell.Helpers.Providers
{
    public class SqlSearchProvider : ISearchProvider
    {
        private readonly IDbConnection _connection;

        public SqlSearchProvider(IDbConnection connection)
        {
            _connection = connection;
        }

        protected virtual bool ValidateModel(DynamicSearchModel searchModel, out string errorMessage)
        {
            errorMessage = "";
            return true;
        }

        public async Task<object> GetSingleQuery(string tableName, Guid id)
        {
            var query = $"SELECT TOP(1) * FROM {tableName} WHERE ID = '{id}'".Trim();
            return await _connection.QueryFirstOrDefaultAsync(query);
        }

        public async Task<object> ExecuteSearch(DynamicSearchModel searchModel)
        {
            if (!ValidateModel(searchModel, out var err))
                throw new Exception(err);

            var query = BuildQuery(searchModel, out var parameter);

            return await _connection.QueryAsync(query, parameter, commandType: CommandType.Text);
        }

        private static string BuildQuery(DynamicSearchModel searchModel, out object parameter)
        {
            var query = new StringBuilder();
            query.AppendLine(GetSelectStatement(searchModel));
            query.AppendLine(GetFromStatement(searchModel));
            query.AppendLine(GetWhereStatement(searchModel, out parameter));
            query.AppendLine(GetGroupByStatement(searchModel));
            query.AppendLine(GetOrderByStatement(searchModel));
            return query.ToString().Trim();
        }

        private static string GetSelectStatement(DynamicSearchModel searchModel)
        {
            var selectStatement = " SELECT " + searchModel
                                      .Select
                                      .Select(t =>
                                          $"{t.Table}.[{t.Field}] {(string.IsNullOrEmpty(t.Alias) ? "" : $"[{t.Alias}]")}")
                                      .JoinString(",");
            return selectStatement;
        }

        private static string GetFromStatement(DynamicSearchModel searchModel)
        {
            var fromStatement = "FROM " + searchModel
                                    .From
                                    .Select((t, index) =>
                                    {
                                        if (string.IsNullOrEmpty(t.JoinTable)) return t.Table;
                                        var ret = new StringBuilder(index == 0 ? $"{t.Table} {t.JoinClause} {t.JoinTable}" : $"{t.JoinClause} {t.JoinTable}");
                                        ret.AppendLine($" ON {t.JoinConditions.Select(x => $"{t.Table}.[{x.Field}]={t.JoinTable}.[{x.JoinField}]").JoinString(" AND ")}");

                                        return ret.ToString().Trim();
                                    })
                                    .JoinString(Environment.NewLine);
            return fromStatement;
        }

        private static string GetWhereStatement(DynamicSearchModel searchModel, out object parameter)
        {
            if (searchModel.Where?.Any() != true)
            {
                parameter = null;
                return string.Empty;
            }
            IDictionary<string, object> objParam = new ExpandoObject();

            var result = "WHERE " + searchModel
                .Where
                .Select(t =>
                {
                    var combine = (t.Combine ?? "AND").Trim().ToUpper();
                    if (combine != "AND" && combine != "OR")
                        combine = "AND";
                    t.Combine = combine;
                    return t;
                })
                .GroupBy(t => t.Combine)
                .Select((t, id1) =>
                {
                    var retValue = t.Select((x, id2) =>
                    {
                        var @operator = x.Operator?.Trim();
                        var function = x.Function?.Trim();

                        if (x.Value == null || "NULL".Equals(x.Value.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return $"{x.Table}.[{x.Field}] IS {(@operator == "=" ? "" : "NOT")} NULL";
                        }

                        var paramName = $"@P_{id1}_{id2}";

                        objParam.Add(paramName, Convert.ChangeType(x.Value, GetDataType(x.DataType)));

                        if (!string.IsNullOrEmpty(function))
                        {
                            paramName = string.Format(function, paramName);
                        }

                        if (!string.IsNullOrEmpty(@operator) && @operator.Equals("LIKE", StringComparison.OrdinalIgnoreCase))
                        {
                            paramName = $"N'%' + {paramName} + '%'";
                        }

                        return $"{x.Table}.[{x.Field}] {x.Operator} {paramName}";
                    })
                    .JoinString($" {t.Key} ");

                    return $"({retValue})";
                })
                .JoinString(" AND ");

            parameter = objParam;
            return result;
        }

        private static string GetGroupByStatement(DynamicSearchModel searchModel)
        {
            if (searchModel.GroupBy?.Any() != true)
            {
                return string.Empty;
            }

            return "GROUP BY " + searchModel
                       .GroupBy
                       .Select(t => $"{t.Table}.[{t.Field}]")
                       .JoinString(",");
        }

        private static string GetOrderByStatement(DynamicSearchModel searchModel)
        {
            if (searchModel.OrderBy?.Any() != true)
            {
                return string.Empty;
            }

            return "ORDER BY " + searchModel
                       .OrderBy
                       .Select(t =>
                       {
                           var direction = (t.Direction ?? "ASC").Trim().ToUpper();
                           return $"{t.Table}.[{t.Field}] {(direction == "ASC" ? "ASC" : "DESC")}";
                       })
                       .JoinString(",");
        }

        private static Type GetDataType(string type)
        {
            switch (type)
            {
                case "int": return typeof(int);
                case "float": return typeof(float);
                case "decimal": return typeof(decimal);
                case "double": return typeof(double);
                case "DateTime": return typeof(DateTime);
                case "Guid": return typeof(Guid);
                default: return typeof(string);
            }
        }
    }
}