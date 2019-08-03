using Cell.Helpers.Extensions;
using Cell.Helpers.Interfaces;
using Cell.Helpers.Models;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Helpers.Providers
{
    public class SqlSearchProvider : ISearchProvider
    {
        private readonly IDbConnection _connection;

        public SqlSearchProvider(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<object> GetSingleQuery(string tableName, Guid id)
        {
            var query = $"SELECT TOP(1) * FROM {tableName} WHERE ID = '{id}'".Trim();
            return await _connection.QueryFirstOrDefaultAsync(query);
        }

        public virtual bool ValidateModel(DynamicSearchModel searchModel, out string errorMessage)
        {
            errorMessage = "";
            return true;
        }

        public async Task<object> ExecuteSearch(DynamicSearchModel searchModel)
        {
            if (!ValidateModel(searchModel, out var err))
                throw new Exception(err);

            if (searchModel.Paging == null)
            {
                var query = BuildQuery(searchModel, out var parameter);

                return await _connection.QueryAsync(query.ToString(), parameter, commandType: CommandType.Text);
            }
            else
            {
                var query = BuildPagingQuery(searchModel, out var parameter);

                var result = await _connection.QueryAsync(query.ToString(), parameter, commandType: CommandType.Text);

                return new PagingResult<dynamic>
                {
                    PageSize = searchModel.Paging.PageSize,
                    TotalRecord = parameter.Get<int>("@TotalRecord"),
                    Items = result
                };
            }
        }


        private static string BuildQuery(DynamicSearchModel searchModel, out DynamicParameters parameter)
        {
            var query = new StringBuilder()
                .AppendLine(GetSelectStatement(searchModel))
                .AppendLine(GetFromStatement(searchModel))
                .AppendLine(GetWhereStatement(searchModel, out parameter))
                .AppendLine(GetGroupByStatement(searchModel))
                .AppendLine(GetOrderByStatement(searchModel))
                .ToString()
                .Trim();
            return query;
        }

        private static string BuildPagingQuery(DynamicSearchModel searchModel, out DynamicParameters parameter)
        {
            var query = new StringBuilder()
                .AppendLine(GetFromStatement(searchModel))
                .AppendLine(GetWhereStatement(searchModel, out parameter))
                .AppendLine(GetGroupByStatement(searchModel))
                .ToString();

            parameter.Add("@CurrentPage", searchModel.Paging.CurrentPage);
            parameter.Add("@PageSize", searchModel.Paging.PageSize);
            parameter.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);

            return new StringBuilder("SELECT @TotalRecord = COUNT(*) ")
                .AppendLine(query)
                .AppendLine(GetSelectStatement(searchModel))
                .AppendLine(query)
                .AppendLine(GetOrderByStatement(searchModel))
                .AppendLine("OFFSET (@CurrentPage - 1) * @PageSize ROWS")
                .AppendLine("FETCH NEXT @PageSize ROWS ONLY")
                .ToString()
                .Trim();
        }

        private static string GetSelectStatement(DynamicSearchModel searchModel)
        {
            return "SELECT " + searchModel
                    .Select
                    .Select(t => $"{t.Table}.[{t.Field}] {(string.IsNullOrEmpty(t.Alias) ? "" : $"[{t.Alias}]")}")
                    .JoinString(",");
        }

        private static string GetFromStatement(DynamicSearchModel searchModel)
        {
            return "FROM " + searchModel
                .From
                .Select((t, index) =>
                {
                    if (string.IsNullOrEmpty(t.JoinTable)) return t.Table;
                    var ret = new StringBuilder(index == 0 ? $"{t.Table} {t.JoinClause} {t.JoinTable}" : $"{t.JoinClause} {t.JoinTable}");
                    ret.AppendLine($" ON {t.JoinConditions.Select(x => $"{t.Table}.[{x.Field}]={t.JoinTable}.[{x.JoinField}]").JoinString(" AND ")}");

                    return ret.ToString().Trim();
                })
                .JoinString(Environment.NewLine);
        }

        private static string GetWhereStatement(DynamicSearchModel searchModel, out DynamicParameters parameter)
        {
            if (searchModel.Where?.Any() != true)
            {
                parameter = null;
                return string.Empty;
            }

            DynamicParameters objParam = new DynamicParameters();

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