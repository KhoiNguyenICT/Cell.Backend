using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Model;
using Cell.Model.Entities.SecurityGroupEntity;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Models.SecurityGroup;
using Cell.Service.Implementations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class SecurityGroupController : CellController<SecurityGroup>
    {
        private readonly ISecurityPermissionService _securityPermissionService;
        private readonly ISecurityGroupService _securityGroupService;
        private readonly ISettingTreeService<SecurityGroup> _treeService;

        public SecurityGroupController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SecurityGroup> entityValidator,
            ISettingTreeService<SecurityGroup> treeService,
            ISecurityPermissionService securityPermissionService,
            ISecurityGroupService securityGroupService) :
            base(context, httpContextAccessor, entityValidator, securityPermissionService)
        {
            AuthorizedType = ConfigurationKeys.SecurityGroupTableName;
            _securityPermissionService = securityPermissionService;
            _securityGroupService = securityGroupService;
            _treeService = treeService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SecurityGroupCreateModel model)
        {
            await ValidateModel(model.To<SecurityGroupModel>());
            var settingGroup = model.To<SecurityGroup>();
            var any = await _securityPermissionService.ExistsAsync();
            if (any)
            {
                await _treeService.InsertLastChildNode(settingGroup, model.Parent, ConfigurationKeys.SecurityGroupTableName);
            }
            else
            {
                await _treeService.InsertFirstRootNode(settingGroup, ConfigurationKeys.SecurityGroupTableName);
            }
            await InitPermission(settingGroup.Id, settingGroup.Name);
            await _securityPermissionService.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SecurityGroupUpdateModel model)
        {
            var settingGroup = await _securityPermissionService.GetByIdAsync(model.Id);
            settingGroup.Name = model.Name;
            settingGroup.Description = model.Description;
            _securityPermissionService.Update(settingGroup);
            await _securityPermissionService.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _treeService.RemoveNode(id);
            await _securityPermissionService.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingGroup(Guid id)
        {
            var settingGroup = await _securityPermissionService.GetByIdAsync(id);
            return Ok(settingGroup.To<SecurityGroupModel>());
        }

        [HttpPost("getTree/{code}")]
        public async Task<IActionResult> GetTree(string code)
        {
            var result = await _securityGroupService.GetTreeAsync(code);
            return Ok(result.To<List<SecurityGroupModel>>());
        }
    }
}