using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Entities.SettingActionInstanceEntity;
using Cell.Model.Models.Others;
using Cell.Model.Models.SettingActionInstance;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class SettingActionInstanceController : CellController<SettingActionInstance>
    {
        private readonly ISettingActionInstanceService _settingActionInstanceService;

        public SettingActionInstanceController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingActionInstance> entityValidator,
            ISettingActionInstanceService settingActionInstanceService,
            ISecurityPermissionService securityPermissionService) :
            base(context, httpContextAccessor, entityValidator, securityPermissionService)
        {
            _settingActionInstanceService = settingActionInstanceService;
            AuthorizedType = ConfigurationKeys.SettingActionInstanceTableName;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] List<SettingActionInstanceCreateModel> models)
        {
            await ValidateModels(models.To<List<SettingActionInstanceModel>>());
            foreach (var model in models)
            {
                var settingActionInstance = model.To<SettingActionInstance>();
                var result = await _settingActionInstanceService.AddAsync(new SettingActionInstance
                {
                    Name = settingActionInstance.Name,
                    Description = settingActionInstance.Description,
                    ContainerType = settingActionInstance.ContainerType,
                    ActionId = settingActionInstance.ActionId,
                    OrdinalPosition = settingActionInstance.OrdinalPosition,
                    Parent = settingActionInstance.Parent,
                    ParentText = settingActionInstance.ParentText,
                    Settings = settingActionInstance.Settings,
                    TableId = settingActionInstance.TableId,
                    TableName = settingActionInstance.TableName
                });
                await InitPermission(result.Id, result.Name);
            }
            await _settingActionInstanceService.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] List<SettingActionInstanceUpdateModel> models)
        {
            await ValidateModels(models.To<List<SettingActionInstanceModel>>());
            foreach (var model in models)
            {
                var entity = await _settingActionInstanceService.GetByIdAsync(model.Id);
                var settingActionInstance = model.To<SettingActionInstance>();
                entity.Name = settingActionInstance.Name;
                entity.Description = settingActionInstance.Description;
                entity.Settings = settingActionInstance.Settings;
            }
            await _settingActionInstanceService.CommitAsync();
            return Ok();
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingActionInstanceModel model)
        {
            var spec = SettingActionInstanceSpecs.SearchByQuery(model.Query)
                .And(SettingActionInstanceSpecs.GetManyByParentId(model.ParentId));
            var queryResult = await Queryable(spec, model.Sorts);
            return Ok(new QueryResult<SettingActionInstanceModel>
            {
                Count = queryResult.Count,
                Items = queryResult.Items.To<List<SettingActionInstanceModel>>()
            });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> ActionInstance(Guid id)
        {
            var settingActionInstance = await _settingActionInstanceService.GetByIdAsync(id);
            return Ok(settingActionInstance.To<SettingActionInstanceModel>());
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _settingActionInstanceService.Delete(id);
            await _settingActionInstanceService.CommitAsync();
            return Ok();
        }
    }
}