using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Common.SeedWork;
using Cell.Core.Errors;
using Cell.Model;
using Cell.Model.Entities.SettingFieldEntity;
using Cell.Model.Models.Others;
using Cell.Model.Models.SettingField;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class SettingFieldController : CellController<SettingField>
    {
        private readonly ISettingFieldService _settingFieldService;

        public SettingFieldController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingField> entityValidator,
            ISettingFieldService settingFieldService) :
            base(context, httpContextAccessor, entityValidator)
        {
            _settingFieldService = settingFieldService;
            AuthorizedType = ConfigurationKeys.SettingFieldTableName;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingFieldModel model)
        {
            var spec = SettingFieldSpecs.SearchByQuery(model.Query)
                .And(SettingFieldSpecs.SearchByTableId(model.TableId));
            var queryable = await Queryable(spec, model.Sorts, model.Skip, model.Take);
            var items = queryable.Items.OrderBy(x => x.OrdinalPosition).ThenBy(x => x.Caption)
                .ThenBy(x => x.Name).ToList();
            return Ok(new QueryResult<SettingFieldModel>
            {
                Count = queryable.Count,
                Items = items.To<List<SettingFieldModel>>()
            });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingField(Guid id)
        {
            var settingField = await _settingFieldService.GetByIdAsync(id);
            return Ok(settingField.To<SettingFieldModel>());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]SettingFieldCreateModel model)
        {
            await ValidateModel(model.To<SettingFieldModel>());
            var spec = SettingFieldSpecs.GetByNameSpec(model.Name);
            var isInvalid = await _settingFieldService.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("Setting field name must be unique");
            var settingField = model.To<SettingField>();
            var result = await _settingFieldService.AddAsync(new SettingField
            {
                Name = settingField.Name,
                Description = settingField.Description,
                Code = settingField.Code,
                AllowFilter = settingField.AllowFilter,
                AllowSummary = settingField.AllowSummary,
                Caption = settingField.Caption,
                DataType = settingField.DataType,
                OrdinalPosition = settingField.OrdinalPosition,
                PlaceHolder = settingField.PlaceHolder,
                Settings = settingField.Settings,
                StorageType = settingField.StorageType,
                TableId = settingField.TableId,
                TableName = settingField.TableName
            });
            await InitPermission(result.Id, result.Name);
            await _settingFieldService.CommitAsync();
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingFieldUpdateModel model)
        {
            var entity = await _settingFieldService.GetByIdAsync(model.Id);
            var settingField = model.To<SettingField>();
            entity.Name = settingField.Name;
            entity.Description = settingField.Description;
            entity.AllowFilter = settingField.AllowFilter;
            entity.AllowSummary = settingField.AllowSummary;
            entity.Caption = settingField.Caption;
            entity.OrdinalPosition = settingField.OrdinalPosition;
            entity.PlaceHolder = settingField.PlaceHolder;
            entity.Settings = settingField.Settings;
            await _settingFieldService.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _settingFieldService.Delete(id);
            await _settingFieldService.CommitAsync();
            return Ok();
        }

        [HttpPost("addColumnToBasedTable")]
        public async Task<IActionResult> AddColumnToBasedTable(AddColumnBasedTableModel model)
        {
            await _settingFieldService.AddColumnToBasedTable(model);
            return Ok();
        }

        [HttpPost("searchFieldName/{tableId}")]
        public async Task<IActionResult> SearchFieldName(Guid tableId)
        {
            var spec = SettingFieldSpecs.SearchByTableId(tableId);
            var result = await Queryable(spec);
            return Ok(result.Items.Select(x => x.Name));
        }
    }
}