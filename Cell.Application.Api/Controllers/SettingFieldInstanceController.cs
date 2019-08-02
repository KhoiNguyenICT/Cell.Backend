using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Entities.SettingFieldInstanceEntity;
using Cell.Model.Models.Others;
using Cell.Model.Models.SettingFieldInstance;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class SettingFieldInstanceController : CellController<SettingFieldInstance>
    {
        private readonly ISettingFieldInstanceService _settingFieldInstanceService;

        public SettingFieldInstanceController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingFieldInstance> entityValidator,
            ISettingFieldInstanceService settingFieldInstanceService,
            ISecurityPermissionService securityPermissionService) :
            base(context, httpContextAccessor, entityValidator, securityPermissionService)
        {
            _settingFieldInstanceService = settingFieldInstanceService;
            AuthorizedType = ConfigurationKeys.SettingFieldInstanceTableName;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] List<SettingFieldInstanceCreateModel> models)
        {
            await ValidateModels(models.To<List<SettingFieldInstanceModel>>());
            foreach (var settingFieldInstanceCommand in models)
            {
                var settingFieldInstance = settingFieldInstanceCommand.To<SettingFieldInstance>();
                var result = await _settingFieldInstanceService.AddAsync(new SettingFieldInstance
                {
                    Name = settingFieldInstance.Name,
                    Description = settingFieldInstance.Description,
                    Caption = settingFieldInstance.Caption,
                    ContainerType = settingFieldInstance.ContainerType,
                    DataType = settingFieldInstance.DataType,
                    FieldId = settingFieldInstance.FieldId,
                    OrdinalPosition = settingFieldInstance.OrdinalPosition,
                    Parent = settingFieldInstance.Parent,
                    ParentText = settingFieldInstance.ParentText,
                    StorageType = settingFieldInstance.StorageType,
                    Settings = settingFieldInstance.Settings
                });
                await InitPermission(result.Id, result.Name);
            }
            await _settingFieldInstanceService.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] List<SettingFieldInstanceUpdateModel> models)
        {
            await ValidateModels(models.To<List<SettingFieldInstanceModel>>());
            foreach (var model in models)
            {
                var settingFieldInstance = model.To<SettingFieldInstance>();
                var entity = await _settingFieldInstanceService.GetByIdAsync(model.Id);
                entity.Settings = settingFieldInstance.Settings;
                _settingFieldInstanceService.Update(entity);
            }
            await _settingFieldInstanceService.CommitAsync();
            return Ok();
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingFieldInstanceModel model)
        {
            var spec = SettingFieldInstanceSpecs.SearchByQuery(model.Query)
                .And(SettingFieldInstanceSpecs.GetManyByParentId(model.ParentId));
            var queryable = await Queryable(spec, model.Sorts);
            return Ok(new QueryResult<SettingFieldInstanceModel>
            {
                Count = queryable.Count,
                Items = queryable.Items.To<List<SettingFieldInstanceModel>>()
            });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> FieldInstance(Guid id)
        {
            var settingFieldInstance = await _settingFieldInstanceService.GetByIdAsync(id);
            return Ok(settingFieldInstance.To<SettingFieldInstanceModel>());
        }

        [HttpPost("delete/{fieldId}")]
        public async Task<IActionResult> Delete(Guid fieldId)
        {
            var settingFieldInstance = await _settingFieldInstanceService.GetByFieldId(fieldId);
            _settingFieldInstanceService.Delete(settingFieldInstance.Id);
            await _settingFieldInstanceService.CommitAsync();
            return Ok();
        }
    }
}