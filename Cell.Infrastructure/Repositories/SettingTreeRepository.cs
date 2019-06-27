﻿using Cell.Core.Constants;
using Cell.Core.SeedWork;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Cell.Infrastructure.Repositories
{
    public interface ISettingTreeRepository<T>
    {
        Task<bool> AnyAsync();

        Task InsertFirstRootNode(T entity);

        Task InsertLastRootNode(T entity);

        Task InsertRootNodeBeforeAnother(T entity, Guid referenceNodeId);

        Task InsertRootNodeAfterAnother(T entity, Guid referenceNodeId);

        Task InsertFirstChildNode(T entity, Guid referenceNodeId);

        Task InsertLastChildNode(T entity, Guid referenceNodeId);

        Task InsertNodeBeforeAnother(T entity, Guid referenceNodeId);

        Task InsertNodeAfterAnother(T entity, Guid referenceNodeId);

        void RemoveNode(Guid id);
    }

    public class SettingTreeRepository<T> : ISettingTreeRepository<T> where T : Entity
    {
        private readonly AppDbContext _dbContext;
        private readonly string _connectionString;

        public SettingTreeRepository(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString(ConfigurationKeys.DefaultConnection);
        }

        public async Task<bool> AnyAsync()
        {
            var result = await _dbContext.Set<T>().AnyAsync();
            return result;
        }

        public async Task InsertFirstRootNode(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                var query = string.Format(
                    "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2, INDEX_RIGHT = INDEX_RIGHT + 2; " +
                    "Update {0} Set INDEX_LEFT = 1, INDEX_RIGHT = 2, PARENT = '{1}', NODE_LEVEL = 1, IS_LEAF = 1, PATH_ID = @ID, PATH_CODE = Name Where ID = @ID;",
                    "T_SETTING_FEATURE", Guid.Empty);
                await connection.ExecuteAsync(query, new { ID = result.Entity.Id });
                connection.Close();
            }
        }

        public async Task InsertLastRootNode(T entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                var query = string.Format(
                    "Declare @MaxIndex Int; Select @MaxIndex = IsNull(Max(INDEX_RIGHT), 0) From {0}; " +
                    "Update {0} Set INDEX_LEFT = IsNull(@MaxIndex, 0) + 1, INDEX_RIGHT = IsNull(@MaxIndex, 0) + 2, PARENT = '{1}', NODE_LEVEL = 1, IS_LEAF = 1, PATH_ID = @ID, PATH_CODE = Name Where ID = @ID;",
                    "T_SETTING_FEATURE", Guid.Empty);

                await connection.ExecuteAsync(query, new { ID = result.Entity.Id });
                connection.Close();
            }
        }

        public async Task InsertRootNodeBeforeAnother(T entity, Guid referenceNodeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync();
                var query = string.Format("Declare @MinLeftIndex Int; Declare @INDEX_LEFT Int; Declare @INDEX_RIGHT Int; " +
                                          "Select @INDEX_LEFT = INDEX_LEFT, @INDEX_RIGHT = INDEX_RIGHT From {0} Where ID = '{2}'; " +
                                          "Select @MinLeftIndex = Min(INDEX_LEFT) From {0} Where INDEX_LEFT <= @INDEX_LEFT And INDEX_RIGHT >= @INDEX_RIGHT; " +
                                          "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2 Where INDEX_LEFT >= @MinLeftIndex;" +
                                          "Update {0} Set INDEX_RIGHT = INDEX_RIGHT + 2 Where INDEX_RIGHT >= @MinLeftIndex;" +
                                          "Update {0} Set INDEX_LEFT = @MinLeftIndex, INDEX_RIGHT = @MinLeftIndex + 1, " +
                                          "PARENT = '{1}', NODE_LEVEL = 1, IS_LEAF = 1, PATH_ID = @ID, PATH_CODE = Name Where ID = @ID;", "T_SETTING_FEATURE", Guid.Empty, referenceNodeId);

                await connection.ExecuteAsync(query, new { ID = result.Entity.Id });
            }
        }

        public async Task InsertRootNodeAfterAnother(T entity, Guid referenceNodeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                var query = string.Format("Declare @INDEX_LEFT Int; Declare @INDEX_RIGHT Int; " +
                                          "Select @INDEX_LEFT = INDEX_LEFT, @INDEX_RIGHT = INDEX_RIGHT From {0} Where ID = '{2}'; " +
                                          "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2 Where INDEX_LEFT >= @INDEX_RIGHT;" +
                                          "Update {0} Set INDEX_RIGHT = INDEX_RIGHT + 2 Where INDEX_RIGHT > @INDEX_RIGHT;" +
                                          "Update {0} Set INDEX_LEFT = @INDEX_RIGHT + 1, INDEX_RIGHT = @INDEX_RIGHT + 2, " +
                                          "PARENT = '{1}', NODE_LEVEL = 1, IS_LEAF = 1, PATH_ID = @ID, PATH_CODE = Name Where ID = @ID;", "T_SETTING_FEATURE", Guid.Empty, referenceNodeId);

                await connection.ExecuteAsync(query, new { ID = result.Entity.Id });
            }
        }

        public async Task InsertFirstChildNode(T entity, Guid referenceNodeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                var query = string.Format("Declare @INDEX_LEFT Int; Declare @Level Int; Declare @PATH_ID NVarchar(Max); Declare @PATH_CODE NVarchar(Max); " +
                                          "Select @INDEX_LEFT = INDEX_LEFT, @Level = NODE_LEVEL, @PATH_ID = PATH_ID, @PATH_CODE = PATH_CODE From {0} Where ID = '{1}'; " +
                                          "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2 Where INDEX_LEFT > @INDEX_LEFT;" +
                                          "Update {0} Set INDEX_RIGHT = INDEX_RIGHT + 2 Where INDEX_RIGHT > @INDEX_LEFT;" +
                                          "Update {0} Set INDEX_LEFT = @INDEX_LEFT + 1, INDEX_RIGHT = @INDEX_LEFT + 2, " +
                                          "PARENT = '{1}', NODE_LEVEL = @Level + 1, IS_LEAF = 1, PATH_ID = @PATH_ID + '\\' + Cast(@ID As NVarchar(50)), " +
                                          "PATH_CODE = @PATH_CODE + '\\' + Name Where ID = @ID; " +
                                          "Update {0} Set IS_LEAF = 0 Where INDEX_LEFT < @INDEX_LEFT + 1 And INDEX_RIGHT > @INDEX_LEFT + 2;", "T_SETTING_FEATURE", referenceNodeId);

                await connection.ExecuteAsync(query, new { ID = result.Entity.Id });
            }
        }

        public async Task InsertLastChildNode(T entity, Guid referenceNodeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                var query = string.Format("Declare @INDEX_RIGHT Int; Declare @Level Int; Declare @PATH_ID NVarchar(Max); Declare @PATH_CODE NVarchar(Max); " +
                                          "Select @INDEX_RIGHT = INDEX_RIGHT, @Level = NODE_LEVEL, @PATH_ID = PATH_ID, @PATH_CODE = PATH_CODE From {0} Where ID = '{1}'; " +
                                          "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2 Where INDEX_LEFT >= @INDEX_RIGHT;" +
                                          "Update {0} Set INDEX_RIGHT = INDEX_RIGHT + 2 Where INDEX_RIGHT >= @INDEX_RIGHT;" +
                                          "Update {0} Set INDEX_LEFT = @INDEX_RIGHT, INDEX_RIGHT = @INDEX_RIGHT + 1, " +
                                          "PARENT = '{1}', NODE_LEVEL = @Level + 1, IS_LEAF = 1, PATH_ID = @PATH_ID + '\\' + Cast(@ID As NVarchar(50)), " +
                                          "PATH_CODE = @PATH_CODE + '\\' + Name Where ID = @ID; " +
                                          "Update {0} Set IS_LEAF = 0 Where INDEX_LEFT < @INDEX_RIGHT And INDEX_RIGHT > @INDEX_RIGHT + 1;", "T_SETTING_FEATURE", referenceNodeId);
                await connection.ExecuteAsync(query, new { ID = result.Entity.Id });
            }
        }

        public async Task InsertNodeBeforeAnother(T entity, Guid referenceNodeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                var query = string.Format(
                    "Declare @INDEX_LEFT Int; Declare @Level Int; Declare @PARENT UniqueIdentifier; Declare @PATH_ID NVarchar(Max); Declare @PATH_CODE NVarchar(Max); " +
                    "Select @INDEX_LEFT = INDEX_LEFT, @Level = NODE_LEVEL, @PARENT = PARENT From {0} Where ID = '{1}'; " +
                    "Select @PATH_ID = PATH_ID, @PATH_CODE = PATH_CODE From {0} Where ID = @PARENT; " +
                    "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2 Where INDEX_LEFT >= @INDEX_LEFT;" +
                    "Update {0} Set INDEX_RIGHT = INDEX_RIGHT + 2 Where INDEX_RIGHT >= @INDEX_LEFT;" +
                    "Update {0} Set INDEX_LEFT = @INDEX_LEFT, INDEX_RIGHT = @INDEX_LEFT + 1, " +
                    "PARENT = '{1}', NODE_LEVEL = @Level, IS_LEAF = 1, PATH_ID = @PATH_ID + '\\' + Cast(@ID As NVarchar(50)), " +
                    "PATH_CODE = @PATH_CODE + '\\' + Name Where ID = @ID; " +
                    "Update {0} Set IS_LEAF = 0 Where INDEX_LEFT < @INDEX_LEFT And INDEX_RIGHT > @INDEX_LEFT + 1;", "T_SETTING_FEATURE", referenceNodeId);
                await connection.ExecuteAsync(query, new { ID = result.Entity.Id });
            }
        }

        public async Task InsertNodeAfterAnother(T entity, Guid referenceNodeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                var query = string.Format(
                    "Declare @INDEX_RIGHT Int; Declare @Level Int; Declare @PARENT UniqueIdentifier; Declare @PATH_ID NVarchar(Max); Declare @PATH_CODE NVarchar(Max); " +
                    "Select @INDEX_RIGHT = INDEX_RIGHT, @Level = NODE_LEVEL, @PARENT = PARENT From {0} Where Id = '{1}'; " +
                    "Select @PATH_ID = PATH_ID, @PATH_CODE = PATH_CODE From {0} Where ID = @PARENT; " +
                    "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2 Where INDEX_LEFT > @INDEX_RIGHT;" +
                    "Update {0} Set INDEX_RIGHT = INDEX_RIGHT + 2 Where INDEX_RIGHT > @INDEX_RIGHT;" +
                    "Update {0} Set INDEX_LEFT = @INDEX_RIGHT + 1, INDEX_RIGHT = @INDEX_RIGHT + 2, " +
                    "PARENT = '{1}', NODE_LEVEL = @Level, IS_LEAF = 1, PATH_ID = @PATH_ID + '\\' + Cast(@ID As NVarchar(50)), " +
                    "PATH_CODE = @PATH_CODE + '\\' + Name Where ID = @ID; " +
                    "Update {0} Set IS_LEAF = 0 Where INDEX_LEFT < @INDEX_RIGHT + 1 And INDEX_RIGHT > @INDEX_RIGHT + 2;",
                    "T_SETTING_FEATURE", referenceNodeId);
                await connection.ExecuteAsync(query, new { ID = result.Entity.Id });
            }
        }

        public void RemoveNode(Guid parent)
        {
        }
    }
}