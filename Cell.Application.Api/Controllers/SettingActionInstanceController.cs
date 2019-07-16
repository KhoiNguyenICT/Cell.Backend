using Cell.Application.Api.Commands;
using Cell.Core.Constants;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Cell.Domain.Aggregates.SettingActionInstanceAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using Microsoft.AspNetCore.Http;

namespace Cell.Application.Api.Controllers
{
    public class SettingActionInstanceController : CellController<SettingActionInstance, SettingActionInstanceCommand>
    {
        private readonly ISettingActionInstanceRepository _settingActionInstanceRepository;

        public SettingActionInstanceController(
            IValidator<SettingActionInstance> entityValidator,
            ISettingActionInstanceRepository settingActionInstanceRepository,
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
            _settingActionInstanceRepository = settingActionInstanceRepository;
            AuthorizedType = ConfigurationKeys.SettingActionInstance;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] List<SettingActionInstanceCommand> command)
        {
            foreach (var settingActionInstanceCommand in command)
            {
                await ValidateModel(settingActionInstanceCommand);
                var settingActionInstance = settingActionInstanceCommand.To<SettingActionInstance>();
                var result = _settingActionInstanceRepository.Add(new SettingActionInstance(
                    settingActionInstance.Name,
                    settingActionInstance.Description,
                    settingActionInstance.ContainerType,
                    settingActionInstance.ActionId,
                    settingActionInstance.OrdinalPosition,
                    settingActionInstance.Parent,
                    settingActionInstance.ParentText,
                    settingActionInstance.Settings,
                    settingActionInstance.TableId,
                    settingActionInstance.TableName));
                await AssignPermission(result.Id, result.Name);
            }
            await _settingActionInstanceRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] List<SettingActionInstanceCommand> command)
        {
            await ValidateModels(command);
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
        public async Task<IActionResult> Search(SearchSettingActionInstanceCommand command)
        {
            var spec = SettingActionInstanceSpecs.SearchByQuery(command.Query)
                .And(SettingActionInstanceSpecs.GetManyByParentId(command.ParentId));
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

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _settingActionInstanceRepository.Delete(id);
            await RemovePermission(id);
            await _settingActionInstanceRepository.CommitAsync();
            return Ok();
        }
    }
}