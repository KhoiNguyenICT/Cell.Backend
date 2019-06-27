using Cell.Application.Api.Commands;
using Cell.Application.Api.Commands.Others;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class SettingFieldController : CellController<SettingField>
    {
        private readonly ISettingFieldRepository _settingFieldRepository;

        public SettingFieldController(
            IValidator<SettingField> entityValidator,
            ISettingFieldRepository settingFieldRepository) : base(entityValidator)
        {
            _settingFieldRepository = settingFieldRepository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingFieldCommand command)
        {
            var spec = SettingFieldSpecs.SearchByQuery(command.Query).And(SettingFieldSpecs.SearchByTableId(command.TableId));
            var queryable = _settingFieldRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.OrderBy(x => x.OrdinalPosition).ThenBy(x => x.Caption).ThenBy(x => x.Name)
                .Skip(command.Skip).Take(command.Take).ToListAsync();
            return Ok(new QueryResult<SettingFieldCommand>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingFieldCommand>>()
            });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingTable(Guid id)
        {
            var settingField = await _settingFieldRepository.GetByIdAsync(id);
            return Ok(settingField.To<SettingFieldCommand>());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]SettingFieldCommand command)
        {
            var spec = SettingFieldSpecs.GetByNameSpec(command.Name);
            var isInvalid = await _settingFieldRepository.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("Setting field name must be unique");

            var settingField = command.To<SettingField>();
            _settingFieldRepository.Add(new SettingField(
                settingField.Name,
                settingField.Description,
                settingField.Code,
                settingField.AllowFilter,
                settingField.AllowSummary,
                settingField.Caption,
                settingField.DataType,
                settingField.OrdinalPosition,
                settingField.PlaceHolder,
                JsonConvert.SerializeObject(command.Settings),
                settingField.StorageType,
                settingField.TableId,
                settingField.TableName));
            await _settingFieldRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingFieldCommand command)
        {
            var settingFieldResult = await _settingFieldRepository.GetByIdAsync(command.Id);
            var settingField = command.To<SettingField>();
            settingFieldResult.Update(
                settingField.Name,
                settingField.Description,
                settingField.AllowFilter,
                settingField.AllowSummary,
                settingField.Caption,
                settingField.OrdinalPosition,
                settingField.PlaceHolder,
                JsonConvert.SerializeObject(command.Settings));
            await _settingFieldRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _settingFieldRepository.Delete(id);
            await _settingFieldRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("addColumnToBasedTable")]
        public async Task<IActionResult> AddColumnToBasedTable(AddColumnToBasedTableCommand command)
        {
            await _settingFieldRepository.AddColumnToBasedTable(command);
            return Ok();
        }
    }
}