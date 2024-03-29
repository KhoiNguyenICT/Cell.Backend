﻿using Cell.Application.Api.Commands;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Cell.Domain.Aggregates.SettingFieldInstanceAggregate;
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
    public class SettingFieldInstanceController : CellController<SettingFieldInstance, SettingFieldInstanceCommand>
    {
        private readonly ISettingFieldInstanceRepository _settingFieldInstanceRepository;

        public SettingFieldInstanceController(
            IValidator<SettingFieldInstance> entityValidator,
            ISettingFieldInstanceRepository settingFieldInstanceRepository,
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
            _settingFieldInstanceRepository = settingFieldInstanceRepository;
            AuthorizedType = ConfigurationKeys.SettingFieldInstance;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] List<SettingFieldInstanceCommand> command)
        {
            await ValidateModels(command);
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
                await AssignPermission(settingFieldInstance.Id, settingFieldInstance.Name);
            }
            await _settingFieldInstanceRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] List<SettingFieldInstanceCommand> command)
        {
            await ValidateModels(command);
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
            await RemovePermission(id);
            await _settingFieldInstanceRepository.CommitAsync();
            return Ok();
        }
    }
}