using Cell.Application.Api.Commands;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SettingActionAggregate;
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
    public class SettingActionController : CellController<SettingAction>
    {
        private readonly ISettingActionRepository _settingActionRepository;

        public SettingActionController(
            ISettingActionRepository settingActionRepository,
            IValidator<SettingAction> entityValidator) : base(entityValidator)
        {
            _settingActionRepository = settingActionRepository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingActionCommand command)
        {
            var spec = SettingActionSpecs.SearchByQuery(command.Query).And(SettingActionSpecs.SearchByTableId(command.TableId));
            var queryable = _settingActionRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
            return Ok(new QueryResult<SettingActionCommand>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingActionCommand>>()
            });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingTable(Guid id)
        {
            var settingField = await _settingActionRepository.GetByIdAsync(id);
            return Ok(settingField);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]SettingActionCommand command)
        {
            var spec = SettingActionSpecs.GetByNameSpec(command.Name);
            var isInvalid = await _settingActionRepository.ExistsAsync(spec);
            if (isInvalid)
            {
                throw new CellException("Setting action name must be unique");
            }

            _settingActionRepository.Add(new SettingAction(
                command.Code,
                command.Name,
                command.Description,
                command.ContainerType,
                JsonConvert.SerializeObject(command.Settings),
                command.TableId,
                command.TableName));
            await _settingActionRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingActionCommand command)
        {
            var settingAction = await _settingActionRepository.GetByIdAsync(command.Id);
            settingAction.Update(
                command.Code,
                command.Name,
                command.Description,
                command.ContainerType,
                JsonConvert.SerializeObject(command.Settings));
            await _settingActionRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _settingActionRepository.Delete(id);
            await _settingActionRepository.CommitAsync();
            return Ok();
        }
    }
}