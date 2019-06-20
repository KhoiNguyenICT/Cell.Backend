using Cell.Core.Constants;
using Cell.Core.Enums;
using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Cell.Domain.Aggregates.SettingFieldAggregate.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cell.Core.Extensions;

namespace Cell.Infrastructure.Repositories
{
    public class SettingFieldRepository : Repository<SettingField, AppDbContext>, ISettingFieldRepository
    {
        private readonly string _connectionString;

        public SettingFieldRepository(AppDbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _connectionString = configuration.GetConnectionString(ConfigurationKeys.DefaultConnection);
        }

        public async Task<int> Count(Guid tableId)
        {
            var result = await _dbContext.SettingFields.Where(x => x.TableId == tableId).CountAsync();
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