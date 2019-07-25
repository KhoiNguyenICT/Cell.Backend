using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingFieldEntity;
using System.Linq;
using Cell.Common.Constants;
using Cell.Common.Enums;
using Cell.Common.Extensions;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cell.Service.Implementations
{
    public class SettingFieldService : Service<SettingField, AppDbContext>, ISettingFieldService
    {
        private readonly string _connectionString;

        public SettingFieldService(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _connectionString = configuration.GetConnectionString(ConfigurationKeys.DefaultConnection);
        }

        public async Task<int> CountAsync(Guid tableId)
        {
            var result = await Context.SettingFields.Where(x => x.TableId == tableId).CountAsync();
            return result;
        }

        public async Task AddColumnToBasedTable(AddColumnBasedTableModel model)
        {
            string query;
            switch (model.DataType.ToLower().FirstCharToUpper())
            {
                case nameof(DataType.String):
                    query = $"ALTER TABLE {model.Table} ADD {model.Name} nvarchar({model.DataSize});";
                    break;

                case nameof(DataType.Int):
                    query = $"ALTER TABLE {model.Table} ADD {model.Name} int;";
                    break;

                case nameof(DataType.Guid):
                    query = $"ALTER TABLE {model.Table} ADD {model.Name} uniqueidentifier";
                    break;

                case nameof(DataType.DateTime):
                    query = $"ALTER TABLE {model.Table} ADD {model.Name} datetimeoffset(7)";
                    break;

                case nameof(DataType.Double):
                    query = $"ALTER TABLE {model.Table} ADD {model.Name} float";
                    break;

                default:
                    query = $"ALTER TABLE {model.Table} ADD {model.Name} nvarchar(MAX)";
                    break;
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                await connection.ExecuteAsync(query);
                connection.Close();
            }
        }
    }
}