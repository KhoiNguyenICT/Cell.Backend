using Cell.Application.Api.Commands;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cell.Application.Api.Controllers
{
    public class SettingFieldController : CellController
    {
        private readonly ISettingFieldRepository _settingFieldRepository;

        public SettingFieldController(ISettingFieldRepository settingFieldRepository)
        {
            _settingFieldRepository = settingFieldRepository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchCommand command)
        {
            var spec = SettingFieldSpecs.SearchByQuery(command.Query);
            var queryable = _settingFieldRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
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
            return Ok(settingField);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]SettingFieldCommand command)
        {
            var spec = SettingFieldSpecs.GetByNameSpec(command.Name);
            var isInvalid = await _settingFieldRepository.ExistsAsync(spec);
            if (isInvalid)
            {
                throw new CellException("Setting table name must be unique");
            }

            var settingField = command.To<SettingField>();
            _settingFieldRepository.Add(new SettingField(
                settingField.AllowFilter,
                settingField.AllowSummary,
                settingField.Caption,
                settingField.DataType,
                settingField.OrdinalPosition,
                settingField.PlaceHolder,
                settingField.Settings,
                settingField.StorageType,
                settingField.TableId,
                settingField.TableName));
            await _settingFieldRepository.CommitAsync();
            return Ok(settingField.To<SettingFieldCommand>());
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingFieldCommand command)
        {
            var settingField = await _settingFieldRepository.GetByIdAsync(command.Id);
            settingField.Update(
                settingField.AllowFilter,
                settingField.AllowSummary,
                settingField.Caption,
                settingField.DataType,
                settingField.OrdinalPosition,
                settingField.PlaceHolder,
                JsonConvert.SerializeObject(command.Settings),
                settingField.StorageType,
                settingField.TableId,
                settingField.TableName);
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
    }
}