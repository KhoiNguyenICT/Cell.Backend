using Cell.Core.Constants;
using Cell.Core.Extensions;
using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingFeatureAggregate;
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
    public class SettingFeatureRepository : Repository<SettingFeature, AppDbContext>, ISettingFeatureRepository
    {
        private readonly string _connectionString;

        public SettingFeatureRepository(AppDbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _connectionString = configuration.GetConnectionString(ConfigurationKeys.DefaultConnection);
        }

        public async Task<bool> AnyAsync()
        {
            return await _dbContext.SettingFeatures.AnyAsync();
        }

        public async Task<List<SettingFeature>> GetTreeAsync()
        {
            var settingFeatureResults = await _dbContext.SettingFeatures.ToListAsync();
            var result = BuildTree(null, settingFeatureResults.To<List<SettingFeature>>());
            return result;
        }

        public async Task InsertFirstRootNode(SettingFeature entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = Add(entity);
                await CommitAsync();
                var query = string.Format(
                    "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2, INDEX_RIGHT = INDEX_RIGHT + 2; " +
                    "Update {0} Set INDEX_LEFT = 1, INDEX_RIGHT = 2, PARENT = '{1}', NODE_LEVEL = 1, IS_LEAF = 1, PATH_ID = @ID, PATH_CODE = Name Where ID = @ID;",
                    "T_SETTING_FEATURE", Guid.Empty);
                await connection.ExecuteAsync(query, new { ID = result.Id });
                connection.Close();
            }
        }

        public async Task InsertLastRootNode(SettingFeature entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = Add(entity);
                await CommitAsync();
                var query = string.Format(
                    "Declare @MaxIndex Int; Select @MaxIndex = IsNull(Max(INDEX_RIGHT), 0) From {0}; " +
                    "Update {0} Set INDEX_LEFT = IsNull(@MaxIndex, 0) + 1, INDEX_RIGHT = IsNull(@MaxIndex, 0) + 2, PARENT = '{1}', NODE_LEVEL = 1, IS_LEAF = 1, PATH_ID = @ID, PATH_CODE = Name Where ID = @ID;",
                    "T_SETTING_FEATURE", Guid.Empty);

                await connection.ExecuteAsync(query, new { ID = result.Id });
                connection.Close();
            }
        }

        public async Task InsertRootNodeBeforeAnother(SettingFeature entity, Guid referenceNodeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = Add(entity);
                await CommitAsync();
                var query = string.Format("Declare @MinLeftIndex Int; Declare @INDEX_LEFT Int; Declare @INDEX_RIGHT Int; " +
                                          "Select @INDEX_LEFT = INDEX_LEFT, @INDEX_RIGHT = INDEX_RIGHT From {0} Where ID = '{2}'; " +
                                          "Select @MinLeftIndex = Min(INDEX_LEFT) From {0} Where INDEX_LEFT <= @INDEX_LEFT And INDEX_RIGHT >= @INDEX_RIGHT; " +
                                          "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2 Where INDEX_LEFT >= @MinLeftIndex;" +
                                          "Update {0} Set INDEX_RIGHT = INDEX_RIGHT + 2 Where INDEX_RIGHT >= @MinLeftIndex;" +
                                          "Update {0} Set INDEX_LEFT = @MinLeftIndex, INDEX_RIGHT = @MinLeftIndex + 1, " +
                                          "PARENT = '{1}', NODE_LEVEL = 1, IS_LEAF = 1, PATH_ID = @ID, PATH_CODE = Name Where ID = @ID;", "T_SETTING_FEATURE", Guid.Empty, referenceNodeId);

                await connection.ExecuteAsync(query, new { ID = result.Id });
            }
        }

        public async Task InsertRootNodeAfterAnother(SettingFeature entity, Guid referenceNodeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = Add(entity);
                await CommitAsync();
                var query = string.Format("Declare @INDEX_LEFT Int; Declare @INDEX_RIGHT Int; " +
                                          "Select @INDEX_LEFT = INDEX_LEFT, @INDEX_RIGHT = INDEX_RIGHT From {0} Where ID = '{2}'; " +
                                          "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2 Where INDEX_LEFT >= @INDEX_RIGHT;" +
                                          "Update {0} Set INDEX_RIGHT = INDEX_RIGHT + 2 Where INDEX_RIGHT > @INDEX_RIGHT;" +
                                          "Update {0} Set INDEX_LEFT = @INDEX_RIGHT + 1, INDEX_RIGHT = @INDEX_RIGHT + 2, " +
                                          "PARENT = '{1}', NODE_LEVEL = 1, IS_LEAF = 1, PATH_ID = @ID, PATH_CODE = Name Where ID = @ID;", "T_SETTING_FEATURE", Guid.Empty, referenceNodeId);

                await connection.ExecuteAsync(query, new { ID = result.Id });
            }
        }

        public async Task InsertFirstChildNode(SettingFeature entity, Guid referenceNodeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = Add(entity);
                await CommitAsync();
                var query = string.Format("Declare @INDEX_LEFT Int; Declare @Level Int; Declare @PATH_ID NVarchar(Max); Declare @PATH_CODE NVarchar(Max); " +
                                          "Select @INDEX_LEFT = INDEX_LEFT, @Level = NODE_LEVEL, @PATH_ID = PATH_ID, @PATH_CODE = PATH_CODE From {0} Where ID = '{1}'; " +
                                          "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2 Where INDEX_LEFT > @INDEX_LEFT;" +
                                          "Update {0} Set INDEX_RIGHT = INDEX_RIGHT + 2 Where INDEX_RIGHT > @INDEX_LEFT;" +
                                          "Update {0} Set INDEX_LEFT = @INDEX_LEFT + 1, INDEX_RIGHT = @INDEX_LEFT + 2, " +
                                          "PARENT = '{1}', NODE_LEVEL = @Level + 1, IS_LEAF = 1, PATH_ID = @PATH_ID + '\\' + Cast(@ID As NVarchar(50)), " +
                                          "PATH_CODE = @PATH_CODE + '\\' + Name Where ID = @ID; " +
                                          "Update {0} Set IS_LEAF = 0 Where INDEX_LEFT < @INDEX_LEFT + 1 And INDEX_RIGHT > @INDEX_LEFT + 2;", "T_SETTING_FEATURE", referenceNodeId);

                await connection.ExecuteAsync(query, new { ID = result.Id });
            }
        }

        public async Task InsertLastChildNode(SettingFeature entity, Guid referenceNodeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = Add(entity);
                await CommitAsync();
                var query = string.Format("Declare @INDEX_RIGHT Int; Declare @Level Int; Declare @PATH_ID NVarchar(Max); Declare @PATH_CODE NVarchar(Max); " +
                                          "Select @INDEX_RIGHT = INDEX_RIGHT, @Level = NODE_LEVEL, @PATH_ID = PATH_ID, @PATH_CODE = PATH_CODE From {0} Where ID = '{1}'; " +
                                          "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2 Where INDEX_LEFT >= @INDEX_RIGHT;" +
                                          "Update {0} Set INDEX_RIGHT = INDEX_RIGHT + 2 Where INDEX_RIGHT >= @INDEX_RIGHT;" +
                                          "Update {0} Set INDEX_LEFT = @INDEX_RIGHT, INDEX_RIGHT = @INDEX_RIGHT + 1, " +
                                          "PARENT = '{1}', NODE_LEVEL = @Level + 1, IS_LEAF = 1, PATH_ID = @PATH_ID + '\\' + Cast(@ID As NVarchar(50)), " +
                                          "PATH_CODE = @PATH_CODE + '\\' + Name Where ID = @ID; " +
                                          "Update {0} Set IS_LEAF = 0 Where INDEX_LEFT < @INDEX_RIGHT And INDEX_RIGHT > @INDEX_RIGHT + 1;", "T_SETTING_FEATURE", referenceNodeId);
                await connection.ExecuteAsync(query, new { ID = result.Id });
            }
        }

        public async Task InsertNodeBeforeAnother(SettingFeature entity, Guid referenceNodeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = Add(entity);
                await CommitAsync();
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
                await connection.ExecuteAsync(query, new { ID = result.Id });
            }
        }

        public async Task InsertNodeAfterAnother(SettingFeature entity, Guid referenceNodeId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var id = Add(entity);
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
                await connection.ExecuteAsync(query, new { ID = id });
            }
        }

        public void RemoveNode(Guid parent)
        {
            
        }

        private List<SettingFeature> BuildTree(Guid? settingFeatureParentId, List<SettingFeature> source)
        {
            return source.Where(item =>
                (settingFeatureParentId == null && (item.Parent == Guid.Empty)) ||
                (item.Parent == settingFeatureParentId)).Select(settingFeature => new SettingFeature
                {
                    Id = settingFeature.Id,
                    Name = settingFeature.Name,
                    Code = settingFeature.Code,
                    Description = settingFeature.Description,
                    Created = settingFeature.Created,
                    Modified = settingFeature.Modified,
                    Settings = settingFeature.Settings,
                    Children = BuildTree(settingFeature.Id, source).ToList(),
                }).ToList();
        }
    }
}