using Cell.Helpers.Interfaces;
using Cell.Helpers.Models;
using Cell.Model;
using Cell.Model.Entities.DynamicEntity;
using Cell.Model.Entities.SettingFieldEntity;
using Cell.Model.Entities.SettingTableEntity;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Service.Implementations
{
    public class DynamicService : IDynamicService
    {
        private readonly ISearchProvider _searchProvider;
        private readonly IWriteProvider _writeProvider;
        private readonly IDbConnection _connection;
        private readonly ISettingTableService _settingTableService;
        private readonly ISettingFieldService _settingFieldService;
        private readonly AppDbContext _context;

        public DynamicService(
            ISearchProvider searchProvider,
            IWriteProvider writeProvider,
            IDbConnection connection,
            AppDbContext context,
            ISettingTableService settingTableService,
            ISettingFieldService settingFieldService)
        {
            _searchProvider = searchProvider;
            _writeProvider = writeProvider;
            _connection = connection;
            _context = context;
            _settingTableService = settingTableService;
            _settingFieldService = settingFieldService;
        }

        public async Task<object> SearchAsync(DynamicSearchModel dynamicSearchModel)
        {
            var result = await _searchProvider.ExecuteSearch(dynamicSearchModel);
            return result;
        }

        public async Task<object> GetSingleAsync(Guid tableId, Guid id)
        {
            var settingTable = await _settingTableService.GetByIdAsync(tableId);
            var result = await _searchProvider.GetSingleQuery(settingTable.BasedTable, id);
            return result;
        }

        public async Task InsertAsync(WriteModel writeModel)
        {
            var settingFieldSpec = SettingFieldSpecs.SearchByTableId(writeModel.TableId);
            var settingTable = await _settingTableService.GetByIdAsync(writeModel.TableId);
            var settingFields = await _settingFieldService.GetManyAsync(settingFieldSpec);
            var settingFieldsName = settingFields.Select(x => x.Name).ToList();
            var fieldsNameExists = writeModel.Data.Where(x => settingFieldsName.Contains(x.Key)).ToList();
            writeModel.Data.Clear();
            writeModel.TableName = settingTable.BasedTable;
            var insertData = settingFields.Where(x => fieldsNameExists.Select(y => y.Key).Contains(x.Name)).ToList();
            foreach (var settingField in insertData)
            {
                writeModel.Data.Add(settingField.Name, fieldsNameExists.FirstOrDefault(x => x.Key == settingField.Name).Value);
            }
            var outputQuery = _writeProvider.InsertQuery(writeModel);
            await _connection.ExecuteAsync(outputQuery.Query, outputQuery.Parameters);
        }

        public async Task UpdateAsync(WriteModel writeModel)
        {
            var settingFieldSpec = SettingFieldSpecs.SearchByTableId(writeModel.TableId);
            var settingTable = await _settingTableService.GetByIdAsync(writeModel.TableId);
            var settingFields = await _settingFieldService.GetManyAsync(settingFieldSpec);
            var settingFieldsName = settingFields.Select(x => x.Name).ToList();
            var fieldsNameExists = writeModel.Data.Where(x => settingFieldsName.Contains(x.Key)).ToList();
            writeModel.Data.Clear();
            writeModel.TableName = settingTable.BasedTable;
            var updateData = settingFields.Where(x => fieldsNameExists.Select(y => y.Key).Contains(x.Name)).ToList();
            foreach (var settingField in updateData)
            {
                writeModel.Data.Add(settingField.Name, fieldsNameExists.FirstOrDefault(x => x.Key == settingField.Name).Value);
            }
            var outputQuery = _writeProvider.UpdateQuery(writeModel);
            await _connection.ExecuteAsync(outputQuery.Query, outputQuery.Parameters);
        }

        public async Task DeleteAsync(Guid tableId, Guid id)
        {
            var settingTable = await _context.SettingTables.FindAsync(tableId);
            var outputQuery = _writeProvider.DeleteQuery(settingTable.BasedTable, id);
            await _connection.ExecuteAsync(outputQuery.Query);
        }
    }
}