using Cell.Application.Api.Commands;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SettingFieldInstanceAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class SettingFieldInstanceController : CellController
    {
        private readonly ISettingFieldInstanceRepository _settingFieldInstanceRepository;

        public SettingFieldInstanceController(ISettingFieldInstanceRepository settingFieldInstanceRepository)
        {
            _settingFieldInstanceRepository = settingFieldInstanceRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] List<SettingFieldInstanceCommand> command)
        {
            foreach (var settingFieldInstanceCommand in command)
            {
                var settingFieldInstance = settingFieldInstanceCommand.To<SettingFieldInstance>();
                _settingFieldInstanceRepository.Add(new SettingFieldInstance(
                    settingFieldInstance.Name,
                    settingFieldInstance.Description,
                    settingFieldInstance.Caption,
                    settingFieldInstance.ContainerType,
                    settingFieldInstance.DataType,
                    settingFieldInstance.FieldId,
                    settingFieldInstance.OrdinalPosition,
                    settingFieldInstance.Parent,
                    JsonConvert.SerializeObject(settingFieldInstanceCommand.Settings),
                    settingFieldInstance.StorageType));
            }
            await _settingFieldInstanceRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] List<SettingFieldInstanceCommand> command)
        {
            foreach (var settingFieldInstanceCommand in command)
            {
                var settingFieldInstance = await _settingFieldInstanceRepository.GetByIdAsync(settingFieldInstanceCommand.Id);
                settingFieldInstance.Update(
                    settingFieldInstanceCommand.Name,
                    settingFieldInstanceCommand.Description,
                    settingFieldInstanceCommand.Caption,
                    settingFieldInstanceCommand.ContainerType,
                    settingFieldInstanceCommand.DataType,
                    settingFieldInstanceCommand.FieldId,
                    settingFieldInstanceCommand.OrdinalPosition,
                    settingFieldInstanceCommand.Parent,
                    JsonConvert.SerializeObject(settingFieldInstanceCommand.Settings),
                    settingFieldInstanceCommand.StorageType);
            }
            await _settingFieldInstanceRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchCommand command)
        {
            var spec = SettingFieldInstanceSpecs.SearchByQuery(command.Query);
            var queryable = _settingFieldInstanceRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
            return Ok(new QueryResult<SettingFieldInstanceCommand>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingFieldInstanceCommand>>()
            });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingFieldInstance(Guid id)
        {
            var settingField = await _settingFieldInstanceRepository.GetByIdAsync(id);
            return Ok(settingField.To<SettingFieldInstanceCommand>());
        }
    }
}