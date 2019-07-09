using Cell.Application.Api.Commands;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SettingReportAggregate;
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
    public class SettingReportController : CellController<SettingReport, SettingReportCommand>
    {
        private readonly ISettingReportRepository _settingReportRepository;

    public SettingReportController(
        IValidator<SettingReport> entityValidator,
        ISettingReportRepository settingReportRepository) : base(entityValidator)
    {
        _settingReportRepository = settingReportRepository;
    }

    [HttpPost("search")]
    public async Task<IActionResult> Search(SearchSettingReportCommand command)
    {
        var spec = SettingReportSpecs.SearchByQuery(command.Query);
        if (command.TableId != Guid.Empty)
            spec.And(SettingReportSpecs.SearchByTableId(command.TableId));
        var queryable = _settingReportRepository.QueryAsync(spec, command.Sorts);
        var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
        return Ok(new QueryResult<SettingFormCommand>
        {
            Count = queryable.Count(),
            Items = items.To<List<SettingFormCommand>>()
        });
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] SettingReportCommand command)
    {
        var spec = SettingReportSpecs.GetByNameSpec(command.Name);
        var isInvalid = await _settingReportRepository.ExistsAsync(spec);
        if (isInvalid)
            throw new CellException("Setting form name must be unique");
        var settingForm = command.To<SettingReport>();
        _settingReportRepository.Add(new SettingReport(
            settingForm.Name,
            settingForm.Description,
            JsonConvert.SerializeObject(command.Settings),
            settingForm.TableId,
            settingForm.TableName));
        await _settingReportRepository.CommitAsync();
        return Ok();
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] SettingReportCommand command)
    {
        var spec = SettingReportSpecs.GetByNameSpec(command.Name);
        var isInvalid = await _settingReportRepository.ExistsAsync(spec);
        if (isInvalid)
            throw new CellException("Setting report name must be unique");
        var settingReport = await _settingReportRepository.GetByIdAsync(command.Id);
        settingReport.Update(
            command.Name,
            command.Description,
            JsonConvert.SerializeObject(command.Settings));
        await _settingReportRepository.CommitAsync();
        return Ok();
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> SettingReport(Guid id)
    {
        var settingReport = await _settingReportRepository.GetByIdAsync(id);
        return Ok(settingReport.To<SettingReportCommand>());
    }
}
}