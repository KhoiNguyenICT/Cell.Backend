using Cell.Application.Api.Commands;
using Cell.Core.Constants;
using Cell.Core.Extensions;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Cell.Domain.Aggregates.SettingFeatureAggregate;
using Cell.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cell.Application.Api.Controllers
{
    public class SettingFeatureController : CellController<SettingFeature, SettingFeatureCommand>
    {
        private readonly ISettingFeatureRepository _settingFeatureRepository;
        private readonly ISettingTreeRepository<SettingFeature> _treeRepository;

        public SettingFeatureController(
            IValidator<SettingFeature> entityValidator,
            ISettingFeatureRepository settingFeatureRepository,
            ISettingTreeRepository<SettingFeature> treeRepository,
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
            _settingFeatureRepository = settingFeatureRepository;
            _treeRepository = treeRepository;
            AuthorizedType = ConfigurationKeys.SettingFeature;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingFeatureCommand command)
        {
            await ValidateModel(command);
            var settingFeature = command.To<SettingFeature>();
            var any = await _settingFeatureRepository.AnyAsync();
            if (any)
            {
                await _treeRepository.InsertNodeBeforeAnother(settingFeature, command.Parent, ConfigurationKeys.SettingFeature);
            }
            else
            {
                await _treeRepository.InsertFirstRootNode(settingFeature, ConfigurationKeys.SettingFeature);
            }

            await AssignPermission(settingFeature.Id, settingFeature.Name);
            await _settingFeatureRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("rename")]
        public async Task<IActionResult> Rename([FromBody] SettingFeatureCommand command)
        {
            var settingFeature = await _settingFeatureRepository.GetByIdAsync(command.Id);
            settingFeature.Rename(command.Name);
            await _settingFeatureRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingFeatureCommand command)
        {
            var settingFeature = await _settingFeatureRepository.GetByIdAsync(command.Id);
            settingFeature.Update(
                command.Name,
                command.Description,
                command.Icon,
                JsonConvert.SerializeObject(command.Settings));
            await _settingFeatureRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _treeRepository.RemoveNode(id);
            await RemovePermission(id);
            await _settingFeatureRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingFeature(Guid id)
        {
            var settingFeature = await _settingFeatureRepository.GetByIdAsync(id);
            return Ok(settingFeature.To<SettingFeatureCommand>());
        }

        [HttpPost("getTree")]
        public async Task<IActionResult> GetTree()
        {
            var result = await _settingFeatureRepository.GetTreeAsync();
            return Ok(result.To<List<SettingFeatureCommand>>());
        }
    }
}