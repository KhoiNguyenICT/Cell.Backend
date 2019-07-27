using Cell.Helpers.Extensions;
using Cell.Helpers.Interfaces;
using Cell.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cell.Helpers.Providers
{
    public class SqlWriteProvider : IWriteProvider
    {
        public OutputQuery InsertQuery(WriteModel model)
        {
            model.Data.AddRange(InsertValueBase());
            var parameters = model.Data.Where(x => x.Value != null).Select(x => x.Key.ToColumnName().ToUpper())
                .ToArray().JoinString(",");
            var values = model.Data.Where(x => x.Value != null).Select(x => "@" + x.Key.ToColumnName().ToUpper()).JoinString(",");
            var query = $"INSERT INTO {model.TableName} ({parameters}) VALUES ({values})";
            var assignValuesToParams = model.Data.Where(x => x.Value != null).Select(x => x.Key).ToArray()
                .Select(parameter => new KeyValuePair<string, object>("@" + parameter.ToColumnName().ToUpper(),
                    model.Data.FirstOrDefault(x => x.Key == parameter.ToString()).Value)).ToList();
            return new OutputQuery
            {
                Query = query,
                Parameters = assignValuesToParams
            };
        }

        public OutputQuery UpdateQuery(WriteModel model)
        {
            var listParamUpdateString = new List<string>();
            var id = model.Data.FirstOrDefault(x => x.Key == "ID").Value;
            model.Data.AddRange(UpdateValueBase());
            var query = "UPDATE " + model.TableName + " SET";
            foreach (var (key, value) in model.Data)
            {
                listParamUpdateString.Add($"{key.ToColumnName().ToUpper()}=@{key.ToColumnName().ToUpper()}");
            }
            var parameters = listParamUpdateString.JoinString(",");
            var queryString = $"{query} {parameters},VERSION=(VERSION+1) WHERE ID='{id}'";
            var assignValuesToParams = model.Data.Where(x => x.Value != null).Select(x => x.Key).ToArray()
                .Select(parameter => new KeyValuePair<string, object>("@" + parameter.ToColumnName().ToUpper(),
                    model.Data.FirstOrDefault(x => x.Key == parameter.ToString()).Value)).ToList();
            return new OutputQuery
            {
                Query = queryString,
                Parameters = assignValuesToParams
            };
        }

        public OutputQuery DeleteQuery(string tableName, Guid id)
        {
            var query = $"DELETE FROM {tableName} WHERE ID = '{id}'";
            return new OutputQuery
            {
                Query = query
            };
        }

        private static Dictionary<string, object> InsertValueBase()
        {
            var items = new Dictionary<string, object>
            {
                {"ID", Guid.NewGuid()},
                {"CREATED", DateTimeOffset.Now},
                {"MODIFIED", DateTimeOffset.Now},
                {"VERSION", 0},
                {"CREATED_BY", Guid.Empty},
                {"MODIFIED_BY", Guid.Empty}
            };
            return items;
        }

        private static Dictionary<string, object> UpdateValueBase()
        {
            var items = new Dictionary<string, object>
            {
                {"MODIFIED", DateTimeOffset.Now},
                {"MODIFIED_BY", Guid.Empty}
            };
            return items;
        }
    }
}