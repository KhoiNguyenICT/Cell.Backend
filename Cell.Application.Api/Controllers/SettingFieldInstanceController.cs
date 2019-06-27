using Cell.Application.Api.Commands;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SettingFieldInstanceAggregate;
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
    public class SettingFieldInstanceController : CellController<SettingFieldInstance>
    {
        private readonly ISettingFieldInstanceRepository _settingFieldInstanceRepository;

        public SettingFieldInstanceController(
            IValidator<SettingFieldInstance> entityValidator,
            ISettingFieldInstanceRepository settingFieldInstanceRepository) : base(entityValidator)
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
                    settingFieldInstance.ParentText,
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
                settingFieldInstance.Update(JsonConvert.SerializeObject(settingFieldInstanceCommand.Settings));
            }
            await _settingFieldInstanceRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingFieldInstanceCommand command)
        {
            var spec = SettingFieldInstanceSpecs.SearchByQuery(command.Query)
                .And(SettingFieldInstanceSpecs.GetManyByParentId(command.ParentId));
            var queryable = _settingFieldInstanceRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
            return Ok(new QueryResult<SettingFieldInstanceCommand>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingFieldInstanceCommand>>()
            });
        }

        [HttpPost("{fieldId}")]
        public async Task<IActionResult> SettingFieldInstance(Guid fieldId)
        {
            var spec = SettingFieldInstanceSpecs.GetByFieldId(fieldId);
            var settingFieldInstance = await _settingFieldInstanceRepository.GetSingleAsync(spec);
            return Ok(settingFieldInstance.To<SettingFieldInstanceCommand>());
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _settingFieldInstanceRepository.Delete(id);
            await _settingFieldInstanceRepository.CommitAsync();
            return Ok();
        }
    }
}