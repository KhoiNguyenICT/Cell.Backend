using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Model;
using Cell.Model.Entities.SettingFeatureEntity;
using Cell.Service.Implementations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cell.Model.Models.SettingFeature;

namespace Cell.Application.Api.Controllers
{
    public class SettingFeatureController : CellController<SettingFeature>
    {
        private readonly ISettingFeatureService _settingFeatureService;
        private readonly ISettingTreeService<SettingFeature> _treeService;

        public SettingFeatureController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingFeature> entityValidator,
            ISettingFeatureService settingFeatureService,
            ISettingTreeService<SettingFeature> treeService) :
            base(context, httpContextAccessor, entityValidator)
        {
            _settingFeatureService = settingFeatureService;
            _treeService = treeService;
            AuthorizedType = ConfigurationKeys.SettingFeatureTableName;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingFeatureCreateModel model)
        {
            await ValidateModel(model.To<SettingFeatureModel>());
            var settingFeature = model.To<SettingFeature>();
            var any = await _settingFeatureService.ExistsAsync();
            if (any)
            {
                await _treeService.InsertLastChildNode(settingFeature, model.Parent, ConfigurationKeys.SettingFeatureTableName);
            }
            else
            {
                await _treeService.InsertFirstRootNode(settingFeature, ConfigurationKeys.SettingFeatureTableName);
            }
            await InitPermission(settingFeature.Id, settingFeature.Name);
            await _settingFeatureService.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingFeatureUpdateModel model)
        {
            var entity = await _settingFeatureService.GetByIdAsync(model.Id);
            var settingFeature = model.To<SettingFeature>();
            entity.Name = settingFeature.Name;
            entity.Description = settingFeature.Description;
            entity.Icon = settingFeature.Icon;
            entity.Settings = settingFeature.Settings;
            _settingFeatureService.Update(entity);
            await _settingFeatureService.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _treeService.RemoveNode(id);
            await _settingFeatureService.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingFeature(Guid id)
        {
            var settingFeature = await _settingFeatureService.GetByIdAsync(id);
            return Ok(settingFeature.To<SettingFeatureModel>());
        }

        [HttpPost("getTree")]
        public async Task<IActionResult> GetTree()
        {
            var settingFeatures = await Queryable().ToListAsync();
            var result = _settingFeatureService.GetTreeAsync(settingFeatures);
            return Ok(result.To<List<SettingFeatureModel>>());
        }
    }
}