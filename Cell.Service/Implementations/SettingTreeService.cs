using Cell.Common.SeedWork;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cell.Common.Constants;
using Cell.Core.Errors;
using Cell.Model;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Cell.Service.Implementations
{
    public interface ISettingTreeService<in T> where T : TreeEntity
    {
        Task RemoveNode(Guid id);

        Task InsertLastChildNode(T entity, Guid referenceNodeId, string table);

        Task InsertFirstRootNode(T entity, string table);
    }

    public class SettingTreeService<T> : ISettingTreeService<T> where T : TreeEntity
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;

        public SettingTreeService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString(ConfigurationKeys.DefaultConnection);
        }

        public async Task RemoveNode(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity.Code == ConfigurationKeys.SystemRole || entity.Code == ConfigurationKeys.SystemDeleted || entity.Code == ConfigurationKeys.SystemDepartment)
                throw new CellException("Cannot delete system group");
            var removeNodes = await _context.Set<T>()
                .Where(x => x.IndexLeft >= entity.IndexLeft && x.IndexRight <= entity.IndexRight).ToListAsync();
            _context.Set<T>().RemoveRange(removeNodes);
        }

        public async Task InsertLastChildNode(T entity, Guid referenceNodeId, string table)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                var query = string.Format("Declare @INDEX_RIGHT Int; Declare @Level Int; Declare @PATH_ID NVarchar(Max); Declare @PATH_CODE NVarchar(Max); " +
                                          "Select @INDEX_RIGHT = INDEX_RIGHT, @Level = NODE_LEVEL, @PATH_ID = PATH_ID, @PATH_CODE = PATH_CODE From {0} Where ID = '{1}'; " +
                                          "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2 Where INDEX_LEFT >= @INDEX_RIGHT;" +
                                          "Update {0} Set INDEX_RIGHT = INDEX_RIGHT + 2 Where INDEX_RIGHT >= @INDEX_RIGHT;" +
                                          "Update {0} Set INDEX_LEFT = @INDEX_RIGHT, INDEX_RIGHT = @INDEX_RIGHT + 1, " +
                                          "PARENT = '{1}', NODE_LEVEL = @Level + 1, IS_LEAF = 1, PATH_ID = @PATH_ID + '\\' + Cast(@ID As NVarchar(50)), " +
                                          "PATH_CODE = @PATH_CODE + '\\' + Name Where ID = @ID; " +
                                          "Update {0} Set IS_LEAF = 0 Where INDEX_LEFT < @INDEX_RIGHT And INDEX_RIGHT > @INDEX_RIGHT + 1;", table, referenceNodeId);
                await connection.ExecuteAsync(query, new { ID = result.Entity.Id });
            }
        }

        public async Task InsertFirstRootNode(T entity, string table)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                var result = await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                var query = string.Format(
                    "Update {0} Set INDEX_LEFT = INDEX_LEFT + 2, INDEX_RIGHT = INDEX_RIGHT + 2; " +
                    "Update {0} Set INDEX_LEFT = 1, INDEX_RIGHT = 2, PARENT = '{1}', NODE_LEVEL = 1, IS_LEAF = 1, PATH_ID = @ID, PATH_CODE = Name Where ID = @ID;",
                    table, Guid.Empty);
                await connection.ExecuteAsync(query, new { ID = result.Entity.Id });
                connection.Close();
            }
        }
    }
}