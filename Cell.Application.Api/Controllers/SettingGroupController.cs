using System;
using System.Collections.Generic;
using Cell.Application.Api.Commands;
using Cell.Core.Extensions;
using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Cell.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cell.Core.Constants;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Newtonsoft.Json;

namespace Cell.Application.Api.Controllers
{
    public class SettingGroupController : CellController<SecurityGroup, SettingGroupCommand>
    {
        private readonly ISecurityGroupRepository _securityGroupRepository;
        private readonly ISettingTreeRepository<SecurityGroup> _treeRepository;

        public SettingGroupController(
            IValidator<SecurityGroup> entityValidator,
            ISecurityGroupRepository securityGroupRepository,
            ISettingTreeRepository<SecurityGroup> treeRepository,
            ISecurityPermissionRepository securityPermissionRepository) : base(entityValidator, securityPermissionRepository, securityGroupRepository)
        {
            _securityGroupRepository = securityGroupRepository;
            _treeRepository = treeRepository;
            AuthorizedType = ConfigurationKeys.SecurityGroup;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingGroupCommand command)
        {
            await ValidateModel(command);
            var settingGroup = command.To<SecurityGroup>();
            var any = await _securityGroupRepository.AnyAsync();
            if (any)
            {
                await _treeRepository.InsertLastChildNode(settingGroup, command.Parent, ConfigurationKeys.SecurityGroup);
            }
            else
            {
                await _treeRepository.InsertFirstRootNode(settingGroup, ConfigurationKeys.SecurityGroup);
            }
            await AssignPermission(settingGroup.Id, settingGroup.Name);
            await _securityGroupRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("rename")]
        public async Task<IActionResult> Rename([FromBody] SettingGroupCommand command)
        {
            var settingGroup = await _securityGroupRepository.GetByIdAsync(command.Id);
            settingGroup.Rename(command.Name);
            await _securityGroupRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingGroupCommand command)
        {
            var settingGroup = await _securityGroupRepository.GetByIdAsync(command.Id);
            settingGroup.Update(
                command.Name,
                command.Description,
                JsonConvert.SerializeObject(command.Settings));
            await _securityGroupRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _treeRepository.RemoveNode(id);
            await RemovePermission(id);
            await _securityGroupRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingGroup(Guid id)
        {
            var settingGroup = await _securityGroupRepository.GetByIdAsync(id);
            return Ok(settingGroup.To<SettingGroupCommand>());
        }

        [HttpPost("getTree/{code}")]
        public async Task<IActionResult> GetTree(string code)
        {
            var result = await _securityGroupRepository.GetTreeAsync(code);
            return Ok(result.To<List<SettingGroupCommand>>());
        }
    }
}