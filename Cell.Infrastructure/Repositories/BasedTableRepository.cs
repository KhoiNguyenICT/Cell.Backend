using Cell.Core.Constants;
using Cell.Domain.Aggregates.BasedTableAggregate;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Infrastructure.Repositories
{
    public class BasedTableRepository : IBasedTableRepository
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;

        public BasedTableRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString(ConfigurationKeys.DefaultConnection);
        }

        public async Task<string> CreateBasedTable(CreateBasedTable model)
        {
            var queryNotTree = @"CREATE TABLE " + model.TableName + @" (
                            ID uniqueidentifier,
	                        CODE nvarchar(200),
	                        NAME nvarchar(200),
	                        DATA xml,
                            CREATED datetimeoffset,
	                        CREATED_BY uniqueidentifier,
                            MODIFIED datetimeoffset,
	                        MODIFIED_BY uniqueidentifier,
	                        DESCRIPTION nvarchar(1000),
	                        VERSION int,
                        );";
            var queryIsTree = @"CREATE TABLE " + model.TableName + @" (
                            ID uniqueidentifier,
	                        CODE nvarchar(200),
	                        NAME nvarchar(200),
	                        DATA xml,
                            CREATED datetimeoffset,
	                        CREATED_BY uniqueidentifier,
                            MODIFIED datetimeoffset,
	                        MODIFIED_BY uniqueidentifier,
	                        DESCRIPTION nvarchar(1000),
	                        VERSION int,
                            INDEX_LEFT int,
                            INDEX_RIGHT int,
                            IS_LEAF int,
                            NODE_LEVEL int,
                            PARENT uniqueidentifier,
                            PATH_CODE nvarchar(1000),
                            PATH_ID nvarchar(1000)
                        );";
            var queryCreateBasedTable = model.IsTree ? queryIsTree : queryNotTree;
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                await connection.ExecuteAsync(queryCreateBasedTable);
                connection.Close();
                return model.TableName;
            }
        }

        public async Task<List<SearchBasedTable>> SearchBasedTable()
        {
            const string query = @"SELECT TABLE_NAME as NAME,
                          (select count(ID) from T_SETTING_TABLE where BASED_TABLE = TABLE_NAME) as Used
                          FROM information_schema.tables
                          WHERE table_type = 'base table'
                          AND table_name LIKE 'T_DATA_%'
                          ORDER BY table_name";
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var result = await conn.QueryAsync<SearchBasedTable>(query);
                var searchBasedTables = result.ToList();
                var dataSearchBasedTable = new List<SearchBasedTable>();
                foreach (var searchBasedTable in searchBasedTables)
                {
                    var settingTable = await _context.SettingTables.FirstOrDefaultAsync(x => x.BasedTable == searchBasedTable.Name);
                    searchBasedTable.Id = settingTable.Id;
                    searchBasedTable.Code = settingTable.Code;
                    dataSearchBasedTable.Add(searchBasedTable);
                }
                return searchBasedTables.ToList();
            }
        }

        public async Task<List<string>> SearchUnusedColumnFromBasedTable(Guid tableId)
        {
            var settingTable = await _context.SettingTables.FindAsync(tableId);
            var query = @"SELECT COLUMN_NAME
                          FROM INFORMATION_SCHEMA.COLUMNS
                          WHERE TABLE_NAME = '" + settingTable.BasedTable + @"' and COLUMN_NAME not in (
                          SELECT Name FROM T_SETTING_FIELD where TABLE_NAME = '" + settingTable.BasedTable + "') AND COLUMN_NAME not in ('DATA')";
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var result = await conn.QueryAsync<string>(query);
                conn.Close();
                return result.ToList();
            }
        }

        public async Task DropBasedTable(string table)
        {
            var query = $"DROP TABLE {table};";
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                await conn.ExecuteAsync(query);
                conn.Close();
            }
        }

        public async Task<List<string>> SearchColumnFromBasedTable(string tableName)
        {
            var query = @"SELECT COLUMN_NAME
                        FROM INFORMATION_SCHEMA.COLUMNS
                        WHERE TABLE_NAME = '" + tableName + @"'
                        ORDER BY ORDINAL_POSITION";
            using (var conn = new SqlConnection(_connectionString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                var result = await conn.QueryAsync<string>(query);
                return result.ToList();
            }
        }
    }
}