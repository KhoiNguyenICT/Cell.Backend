using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cell.Application.Api.Commands;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SettingActionAggregate;
using Cell.Domain.Aggregates.SettingActionInstanceAggregate;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Cell.Domain.Aggregates.SettingFieldInstanceAggregate;
using Cell.Domain.Aggregates.SettingViewAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Cell.Application.Api.Controllers
{
    public class SettingViewController : CellController
    {
        private readonly ISettingViewRepository _settingViewRepository;
        private readonly ISettingFieldInstanceRepository _settingFieldInstanceRepository;
        private readonly ISettingActionInstanceRepository _settingActionInstanceRepository;
        private readonly ISettingFieldRepository _settingFieldRepository;
        private readonly ISettingActionRepository _settingActionRepository;

        public SettingViewController(
            ISettingViewRepository settingViewRepository, 
            ISettingFieldInstanceRepository settingFieldInstanceRepository, 
            ISettingActionInstanceRepository settingActionInstanceRepository, 
            ISettingFieldRepository settingFieldRepository, 
            ISettingActionRepository settingActionRepository)
        {
            _settingViewRepository = settingViewRepository;
            _settingFieldInstanceRepository = settingFieldInstanceRepository;
            _settingActionInstanceRepository = settingActionInstanceRepository;
            _settingFieldRepository = settingFieldRepository;
            _settingActionRepository = settingActionRepository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingViewCommand command)
        {
            var spec = SettingViewSpecs.SearchByQuery(command.Query);
            var queryable = _settingViewRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.OrderBy(x => x.Name).Skip(command.Skip).Take(command.Take).ToListAsync();
            return Ok(new QueryResult<SettingViewCommand>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingViewCommand>>()
            });
        }

        [HttpPost("settingViewSettings/{id}")]
        public async Task<IActionResult> SettingView(Guid id)
        {
            var settingFieldInstanceSpecs = SettingFieldInstanceSpecs.GetManyByParentId(id);
            var settingActionInstanceSpecs = SettingActionInstanceSpecs.GetManyByParentId(id);
            var settingView = await _settingViewRepository.GetByIdAsync(id);
            var settingFieldInstances = await _settingFieldInstanceRepository.GetManyAsync(settingFieldInstanceSpecs);
            var settingActionInstances = await _settingActionInstanceRepository.GetManyAsync(settingActionInstanceSpecs);
            return Ok(new
            {
                settingView = settingView.To<SettingViewCommand>(),
                settingFieldInstances = settingFieldInstances.To<List<SettingFieldInstanceCommand>>(),
                settingActionInstances = settingActionInstances.To<List<SettingActionInstanceCommand>>()
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingViewCommand command)
        {
            var specs = SettingViewSpecs.GetByNameSpec(command.Name);
            var isInvalid = await _settingViewRepository.ExistsAsync(specs);
            if (isInvalid)
                throw new CellException("Setting view name must be unique");
            var result = _settingViewRepository.Add(new SettingView(
                command.Code,
                command.Name,
                command.Description,
                command.TableId,
                command.TableName,
                JsonConvert.SerializeObject(command.Settings)));
            await _settingViewRepository.CommitAsync();
            return Ok(result.To<SettingViewCommand>());
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingViewCommand command)
        {
            var settingView = await _settingViewRepository.GetByIdAsync(command.Id);
            settingView.Update(
                settingView.Name,
                settingView.Description,
                JsonConvert.SerializeObject(command.Settings));
            await _settingViewRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _settingViewRepository.Delete(id);
            await _settingViewRepository.CommitAsync();
            return Ok();
        }
    }
}