using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Common.SeedWork;
using Cell.Core.Errors;
using Cell.Model;
using Cell.Model.Entities.SettingActionEntity;
using Cell.Model.Entities.SettingAdvancedEntity;
using Cell.Model.Entities.SettingFieldEntity;
using Cell.Model.Entities.SettingTableEntity;
using Cell.Model.Models.Others;
using Cell.Model.Models.SettingField;
using Cell.Model.Models.SettingTable;
using Cell.Service.Implementations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class SettingTableController : CellController<SettingTable>
    {
        private readonly ISettingFieldService _settingFieldService;
        private readonly ISettingActionService _settingActionService;
        private readonly ISettingTableService _settingTableService;
        private readonly IBasedTableService _basedTableService;
        private readonly ISettingAdvancedService _settingAdvancedService;

        public SettingTableController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingTable> entityValidator,
            ISettingFieldService settingFieldService,
            ISettingActionService settingActionService,
            ISettingTableService settingTableService,
            IBasedTableService basedTableService,
            ISettingAdvancedService settingAdvancedService) :
            base(context, httpContextAccessor, entityValidator)
        {
            _settingFieldService = settingFieldService;
            _settingActionService = settingActionService;
            _settingTableService = settingTableService;
            _basedTableService = basedTableService;
            _settingAdvancedService = settingAdvancedService;
            AuthorizedType = ConfigurationKeys.SettingTableTableName;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchModel model)
        {
            var settingTableItems = new List<SettingTableModel>();
            var spec = SettingTableSpecs.SearchByQuery(model.Query);
            var queryable = await Queryable(spec, model.Sorts, model.Skip, model.Take);
            foreach (var settingTable in queryable.Items)
            {
                var countFieldItems = await _settingFieldService.CountAsync(settingTable.Id);
                var countActionItems = await _settingActionService.CountAsync(settingTable.Id);
                var settingTableModel = settingTable.To<SettingTableModel>();
                settingTableModel.CountFieldItems = countFieldItems;
                settingTableModel.CountActionItems = countActionItems;
                settingTableItems.Add(settingTableModel);
            }
            return Ok(new QueryResult<SettingTableModel>
            {
                Count = queryable.Count,
                Items = settingTableItems
            });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Table(Guid id)
        {
            var settingTable = await _settingTableService.GetByIdAsync(id);
            return Ok(settingTable.To<SettingTableModel>());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]SettingTableCreateModel model)
        {
            var settingTableModel = model.To<SettingTableModel>();
            await ValidateModel(settingTableModel);
            var spec = SettingTableSpecs.GetByNameSpec(model.Name);
            var isInvalid = await _settingTableService.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("Setting table name must be unique");
            var settingTable = settingTableModel.To<SettingTable>();
            var result = await _settingTableService.AddAsync(new SettingTable
            {
                Name = settingTable.Name,
                Description = settingTable.Description,
                Code = settingTable.Code,
                BasedTable = settingTable.BasedTable,
                Settings = settingTable.Settings
            });
            await _settingTableService.CommitAsync();
            var settingAdvancedFieldBasedSpec = SettingAdvancedSpecs.GetBySettingFieldBased();
            var settingAdvancedFieldsBased = await _settingAdvancedService.GetManyAsync(settingAdvancedFieldBasedSpec);
            foreach (var advanced in settingAdvancedFieldsBased)
            {
                var input = advanced.SettingValue.Replace("###TABLE_NAME###", result.BasedTable)
                    .Replace("###TABLE_ID###", result.Id.ToString());
                var settingFieldModel = JsonConvert.DeserializeObject<SettingFieldModel>(input);
                var settingField = await _settingFieldService.AddAsync(settingFieldModel.To<SettingField>());
                await InitPermission(settingField.Id, settingField.Name);
            }
            await InitPermission(result.Id, result.Name);
            await _settingFieldService.CommitAsync();
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingTableUpdateModel model)
        {
            var entity = await _settingTableService.GetByIdAsync(model.Id);
            var settingTable = model.To<SettingTable>();
            entity.Name = settingTable.Name;
            entity.Description = settingTable.Description;
            entity.Settings = settingTable.Settings;
            _settingTableService.Update(entity);
            await _settingTableService.CommitAsync();
            return Ok();
        }

        [HttpPost("searchBasedTable")]
        public async Task<IActionResult> BasedTables()
        {
            var result = await _basedTableService.SearchBasedTable();
            return Ok(result);
        }

        [HttpPost("createBasedTable")]
        public async Task<IActionResult> CreateBasedTable(CreateBasedTableModel model)
        {
            var result = await _basedTableService.CreateBasedTable(model);
            return Ok(new { tableName = result });
        }

        [HttpPost("searchColumnFromBasedTable/{tableName}")]
        public async Task<IActionResult> SearchColumnFromBasedTable(string tableName)
        {
            var result = await _basedTableService.SearchColumnFromBasedTable(tableName);
            return Ok(result);
        }

        [HttpPost("searchUnusedColumnFromBasedTable/{tableId}")]
        public async Task<IActionResult> SearchUnusedColumnFromBasedTable(Guid tableId)
        {
            var result = await _basedTableService.SearchUnusedColumnFromBasedTable(tableId);
            return Ok(result);
        }
    }
}