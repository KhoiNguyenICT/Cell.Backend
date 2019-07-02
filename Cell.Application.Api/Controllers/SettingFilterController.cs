using Cell.Application.Api.Commands;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SettingFilterAggregate;
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
    public class SettingFilterController : CellController<SettingFilter>
    {
        private readonly ISettingFilterRepository _settingFilterRepository;

        public SettingFilterController(
            IValidator<SettingFilter> entityValidator,
            ISettingFilterRepository settingFilterRepository) : base(entityValidator)
        {
            _settingFilterRepository = settingFilterRepository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingFilterCommand command)
        {
            var spec = SettingFilterSpecs.SearchByQuery(command.Query);
            if (command.TableId != Guid.Empty)
                spec.And(SettingFilterSpecs.SearchByTableId(command.TableId));
            var queryable = _settingFilterRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
            return Ok(new QueryResult<SettingFilterCommand>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingFilterCommand>>()
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingFilterCommand command)
        {
            var spec = SettingFilterSpecs.GetByNameSpec(command.Name);
            var isInvalid = await _settingFilterRepository.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("Setting filter name must be unique");
            var settingFilter = command.To<SettingFilter>();
            _settingFilterRepository.Add(new SettingFilter(
                settingFilter.Name,
                settingFilter.Description,
                JsonConvert.SerializeObject(command.Settings),
                settingFilter.TableId,
                settingFilter.TableName));
            await _settingFilterRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingFilterCommand command)
        {
            var spec = SettingFilterSpecs.GetByNameSpec(command.Name);
            var isInvalid = await _settingFilterRepository.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("Setting filter name must be unique");
            var settingFilter = await _settingFilterRepository.GetByIdAsync(command.Id);
            settingFilter.Update(
                command.Name,
                command.Description,
                JsonConvert.SerializeObject(command.Settings));
            await _settingFilterRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingFilter(Guid id)
        {
            var settingFilter = await _settingFilterRepository.GetByIdAsync(id);
            return Ok(settingFilter.To<SettingFilterCommand>());
        }
    }
}