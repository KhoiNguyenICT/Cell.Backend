using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cell.Application.Api.Commands;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SettingFormAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Cell.Application.Api.Controllers
{
    public class SettingFormController : CellController<SettingForm>
    {
        private readonly ISettingFormRepository _settingFormRepository;

        public SettingFormController(
            ISettingFormRepository settingFormRepository,
            IValidator<SettingForm> entityValidator) : base(entityValidator)
        {
            _settingFormRepository = settingFormRepository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingFormCommand command)
        {
            var spec = SettingFormSpecs.SearchByQuery(command.Query);
            if (command.TableId != Guid.Empty)
                spec.And(SettingFormSpecs.SearchByTableId(command.TableId));
            var queryable = _settingFormRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
            return Ok(new QueryResult<SettingFormCommand>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingFormCommand>>()
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]SettingFormCommand command)
        {
            var spec = SettingFormSpecs.GetByNameSpec(command.Name);
            var isInvalid = await _settingFormRepository.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("Setting form name mus be unique");
            var settingForm = command.To<SettingForm>();
            _settingFormRepository.Add(new SettingForm(
                settingForm.Name, 
                settingForm.Description, 
                settingForm.LayoutId, 
                JsonConvert.SerializeObject(command.Settings),
                settingForm.TableId, 
                settingForm.TableName));
            await _settingFormRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]SettingFormCommand command)
        {
            var settingForm = await _settingFormRepository.GetByIdAsync(command.Id);
            settingForm.Update(
                command.Name,
                command.Description,
                command.LayoutId,
                JsonConvert.SerializeObject(command.Settings));
            await _settingFormRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingForm(Guid id)
        {
            var settingForm = await _settingFormRepository.GetByIdAsync(id);
            return Ok(settingForm.To<SettingFormCommand>());
        }
    }
}