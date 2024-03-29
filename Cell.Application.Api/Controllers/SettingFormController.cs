﻿using Cell.Application.Api.Commands;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Cell.Domain.Aggregates.SettingFormAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cell.Core.Constants;
using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cell.Application.Api.Controllers
{
    public class SettingFormController : CellController<SettingForm, SettingFormCommand>
    {
        private readonly ISettingFormRepository _settingFormRepository;

        public SettingFormController(
            ISettingFormRepository settingFormRepository,
            IValidator<SettingForm> entityValidator,
            ISecurityPermissionRepository securityPermissionRepository,
            ISecurityGroupRepository securityGroupRepository,
            IHttpContextAccessor httpContextAccessor,
            ISecurityUserRepository securityUserRepository) : base(
            entityValidator,
            securityPermissionRepository,
            securityGroupRepository,
            httpContextAccessor,
            securityUserRepository)
        {
            _settingFormRepository = settingFormRepository;
            AuthorizedType = ConfigurationKeys.SettingForm;
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
            await ValidateModel(command);
            var spec = SettingFormSpecs.GetByNameSpec(command.Name);
            var isInvalid = await _settingFormRepository.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("Setting form name must be unique");
            var settingForm = command.To<SettingForm>();
            var result = _settingFormRepository.Add(new SettingForm(
                settingForm.Name,
                settingForm.Description,
                settingForm.LayoutId,
                JsonConvert.SerializeObject(command.Settings),
                settingForm.TableId,
                settingForm.TableName));
            await AssignPermission(result.Id, result.Name);
            await _settingFormRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]SettingFormCommand command)
        {
            var spec = SettingFormSpecs.GetByNameSpec(command.Name);
            var isInvalid = await _settingFormRepository.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("Setting form name must be unique");
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

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _settingFormRepository.Delete(id);
            await RemovePermission(id);
            await _settingFormRepository.CommitAsync();
            return Ok();
        }
    }
}