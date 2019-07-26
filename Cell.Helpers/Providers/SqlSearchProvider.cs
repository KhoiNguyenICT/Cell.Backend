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
            return await _connection.QueryAsync(query);
        }

        public async Task<object> ExecuteSearch(DynamicSearchModel searchModel)
        {
            if (!ValidateModel(searchModel, out var err))
                throw new Exception(err);

            var query = new StringBuilder();
            query.AppendLine(GetSelectStatement(searchModel));
            query.AppendLine(GetFromStatement(searchModel));
            query.AppendLine(GetWhereStatement(searchModel, out var parameter));
            return await _connection.QueryAsync(query.ToString(), parameter, commandType: CommandType.Text);
        }

        private static string GetSelectStatement(DynamicSearchModel searchModel)
        {
            return " SELECT " + searchModel
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
                .Select((t, index) =>
                {
                    var paramName = $"@P_{index}";
                    objParam.Add(paramName, Convert.ChangeType(t.Value, GetDataType(t.DataType)));
                    return $"{t.Table}.[{t.Field}] {t.Function} {paramName}";
                })
                .JoinString(" AND ");

            parameter = objParam;
            return result;
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