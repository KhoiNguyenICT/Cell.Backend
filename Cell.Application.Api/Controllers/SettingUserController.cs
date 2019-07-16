using Cell.Application.Api.Commands;
using Cell.Application.Api.Commands.Others;
using Cell.Core.Constants;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class SettingUserController : CellController<SecurityUser, SettingUserCommand>
    {
        private readonly ISecurityUserRepository _securityUserRepository;

        public SettingUserController(
            IValidator<SecurityUser> entityValidator,
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
            _securityUserRepository = securityUserRepository;
            AuthorizedType = ConfigurationKeys.SecurityUser;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchCommand command)
        {
            var spec = SecurityUserSpecs.SearchByQuery(command.Query);
            var objectIds = await ObjectIds(AuthorizedType);
            var queryable = _securityUserRepository.QueryAsync(spec, command.Sorts)
                .Where(x => objectIds.Contains(x.Id));
            var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
            return Ok(new QueryResult<SettingUserCommand>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingUserCommand>>()
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingUserCommand command)
        {
            await ValidateModel(command);
            var spec = SecurityUserSpecs.GetByAccountSpec(command.Account)
                .Or(SecurityUserSpecs.GetByEmailSpec(command.Email));
            var isInvalid = await _securityUserRepository.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("User name or account must be unique");
            var result = _securityUserRepository.Add(new SecurityUser(
                command.Code,
                command.Description,
                command.Account,
                command.Email,
                command.Password.ToSha256(),
                command.Phone,
                JsonConvert.SerializeObject(command.Settings)));
            await AssignPermission(result.Id, result.Account);
            await _securityUserRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingUserCommand command)
        {
            await ValidateModel(command);
            var settingUser = await _securityUserRepository.GetByIdAsync(command.Id);
            settingUser.Update(
                command.Description,
                command.Email,
                command.Phone,
                JsonConvert.SerializeObject(command.Settings));
            await _securityUserRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("updateGroup")]
        public async Task<IActionResult> UpdateGroup(UpdateGroupForUserCommand command)
        {
            var settingUser = await _securityUserRepository.GetByIdAsync(command.UserId);
            var settingUserCommand = settingUser.To<SettingUserCommand>();
            settingUserCommand.Settings.Departments = command.Departments;
            settingUserCommand.Settings.Roles = command.Roles;
            settingUserCommand.Settings.DefaultDepartmentData = command.DefaultDepartmentData;
            settingUserCommand.Settings.DefaultRoleData = command.DefaultRoleData;
            settingUser.UpdateGroup(
                command.DefaultRole,
                command.DefaultDepartment,
                JsonConvert.SerializeObject(settingUserCommand.Settings));
            await _securityUserRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingUser(Guid id)
        {
            var result = await _securityUserRepository.GetByIdAsync(id);
            return Ok(result.To<SettingUserCommand>());
        }
    }
}