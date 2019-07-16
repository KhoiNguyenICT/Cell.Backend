using Cell.Application.Api.Commands;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Cell.Domain.Aggregates.SettingActionAggregate;
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
    public class SettingActionController : CellController<SettingAction, SettingActionCommand>
    {
        private readonly ISettingActionRepository _settingActionRepository;

        public SettingActionController(
            ISettingActionRepository settingActionRepository,
            IValidator<SettingAction> entityValidator,
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
            _settingActionRepository = settingActionRepository;
            AuthorizedType = ConfigurationKeys.SettingAction;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingActionCommand command)
        {
            var spec = SettingActionSpecs.SearchByQuery(command.Query)
                .And(SettingActionSpecs.SearchByTableId(command.TableId));
            var objectIds = await ObjectIds(AuthorizedType);
            var queryable = _settingActionRepository.QueryAsync(spec, command.Sorts)
                .Where(x => objectIds.Contains(x.Id));
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
            await ValidateModel(command);
            var result = _settingActionRepository.Add(new SettingAction(
                command.Code,
                command.Name,
                command.Description,
                command.ContainerType,
                JsonConvert.SerializeObject(command.Settings),
                command.TableId,
                command.TableName));
            await AssignPermission(result.Id, result.Name);
            await _settingActionRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingActionCommand command)
        {
            await ValidateModel(command);
            var spec = SettingActionSpecs.GetByNameSpec(command.Name);
            var isInvalid = await _settingActionRepository.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("Setting action name must be unique");
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
            await RemovePermission(id);
            await _settingActionRepository.CommitAsync();
            return Ok();
        }
    }
}