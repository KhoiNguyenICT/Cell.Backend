using System;
using Cell.Core.Extensions;
using Cell.Domain.Aggregates.SettingActionInstanceAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cell.Application.Api.Commands;
using Cell.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cell.Application.Api.Controllers
{
    public class SettingActionInstanceController : CellController
    {
        private readonly ISettingActionInstanceRepository _settingActionInstanceRepository;

        public SettingActionInstanceController(ISettingActionInstanceRepository settingActionInstanceRepository)
        {
            _settingActionInstanceRepository = settingActionInstanceRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] List<SettingActionInstance> command)
        {
            foreach (var settingActionInstanceCommand in command)
            {
                var settingActionInstance = settingActionInstanceCommand.To<SettingActionInstance>();
                _settingActionInstanceRepository.Add(new SettingActionInstance(
                    settingActionInstance.Name,
                    settingActionInstance.Description,
                    settingActionInstance.ContainerType,
                    settingActionInstance.ActionId,
                    settingActionInstance.OrdinalPosition,
                    settingActionInstance.Parent,
                    settingActionInstance.Settings,
                    settingActionInstance.TableId,
                    settingActionInstance.TableName));
            }
            await _settingActionInstanceRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] List<SettingActionInstanceCommand> command)
        {
            foreach (var settingActionInstanceCommand in command)
            {
                var settingActionInstance = await _settingActionInstanceRepository.GetByIdAsync(settingActionInstanceCommand.Id);
                settingActionInstance.Update(
                    settingActionInstanceCommand.Name,
                    settingActionInstance.Description,
                    settingActionInstance.Settings);
            }
            await _settingActionInstanceRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchCommand command)
        {
            var spec = SettingActionInstanceSpecs.SearchByQuery(command.Query);
            var queryable = _settingActionInstanceRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
            return Ok(new QueryResult<SettingActionInstanceCommand>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingActionInstanceCommand>>()
            });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingActionInstance(Guid id)
        {
            var settingActionInstance = await _settingActionInstanceRepository.GetByIdAsync(id);
            return Ok(settingActionInstance.To<SettingActionInstanceCommand>());
        }
    }
}