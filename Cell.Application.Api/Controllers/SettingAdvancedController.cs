using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Model;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Entities.SettingAdvancedEntity;
using Cell.Model.Models.SettingAdvanced;
using Cell.Service.Implementations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class SettingAdvancedController : CellController<SettingAdvanced>
    {
        private readonly ISettingAdvancedService _settingAdvancedService;
        private readonly ISettingTreeService<SettingAdvanced> _treeService;

        public SettingAdvancedController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingAdvanced> entityValidator,
            ISettingAdvancedService settingAdvancedService,
            ISettingTreeService<SettingAdvanced> treeService,
            ISecurityPermissionService securityPermissionService) :
            base(context, httpContextAccessor, entityValidator, securityPermissionService)
        {
            _settingAdvancedService = settingAdvancedService;
            _treeService = treeService;
            AuthorizedType = ConfigurationKeys.SettingAdvancedTableName;
        }

        [HttpPost("getTree")]
        public async Task<IActionResult> GetTree()
        {
            var queryResult = await Queryable();
            var result = _settingAdvancedService.GetTreeAsync(queryResult.Items.ToList());
            return Ok(result.To<List<SettingAdvancedModel>>());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingAdvancedCreateModel model)
        {
            await ValidateModel(model.To<SettingAdvancedModel>());
            var settingAdvanced = model.To<SettingAdvanced>();
            var any = await _settingAdvancedService.ExistsAsync();
            if (any)
            {
                await _treeService.InsertLastChildNode(settingAdvanced, model.Parent, ConfigurationKeys.SettingAdvancedTableName);
            }
            else
            {
                await _treeService.InsertFirstRootNode(settingAdvanced, ConfigurationKeys.SettingAdvancedTableName);
            }
            await InitPermission(settingAdvanced.Id, settingAdvanced.Name);
            await _settingAdvancedService.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingAdvancedUpdateModel model)
        {
            await ValidateModel(model.To<SettingAdvancedModel>());
            var settingAdvanced = model.To<SettingAdvanced>();
            var entity = await _settingAdvancedService.GetByIdAsync(model.Id);
            entity.Name = settingAdvanced.Name;
            entity.Description = settingAdvanced.Description;
            _settingAdvancedService.Update(entity);
            await _settingAdvancedService.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _treeService.RemoveNode(id);
            await _settingAdvancedService.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var settingAdvanced = await _settingAdvancedService.GetByIdAsync(id);
            return Ok(settingAdvanced.To<SettingAdvancedModel>());
        }
    }
}