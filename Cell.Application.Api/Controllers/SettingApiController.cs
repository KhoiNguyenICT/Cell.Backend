using System;
using System.Collections.Generic;
using Cell.Model;
using Cell.Model.Entities.SettingApiEntity;
using Cell.Model.Models.Others;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Cell.Common.Extensions;
using Cell.Common.SeedWork;
using Cell.Model.Entities.SecurityGroupEntity;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Models.SettingApi;

namespace Cell.Application.Api.Controllers
{
    public class SettingApiController : CellController<SettingApi>
    {
        private readonly ISettingApiService _settingApiService;

        public SettingApiController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingApi> entityValidator, 
            ISettingApiService settingApiService,
            ISecurityPermissionService securityPermissionService) :
            base(context, httpContextAccessor, entityValidator, securityPermissionService)
        {
            _settingApiService = settingApiService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] SearchSettingApiModel model)
        {
            var spec = SettingApiSpecs.SearchByQuery(model.Query);
            if (model.TableId != Guid.Empty && model.TableId != null)
                spec.And(SettingApiSpecs.SearchByTableId(model.TableId));
            var queryable = await Queryable(spec, model.Sorts, model.Skip, model.Take);
            return Ok(new QueryResult<SettingApiModel>
            {
                Count = queryable.Count,
                Items = queryable.Items.To<List<SettingApiModel>>()
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingApiCreateModel model)
        {
            await ValidateModel(model.To<SettingApiModel>());
            var settingApi = model.To<SettingApi>();
            var result = await _settingApiService.AddAsync(new SettingApi
            {
                Code = settingApi.Code,
                Name = settingApi.Name,
                Description = settingApi.Description,
                Library = settingApi.Library,
                Method = settingApi.Method,
                Settings = settingApi.Settings,
                TableId = settingApi.TableId,
                TableName = settingApi.TableName
            });
            await InitPermission(result.Id, result.Name);
            await _settingApiService.CommitAsync();
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingApiUpdateModel model)
        {
            var entity = await _settingApiService.GetByIdAsync(model.Id);
            var settingApi = model.To<SettingApi>();
            entity.Name = settingApi.Name;
            entity.Description = settingApi.Description;
            entity.Settings = settingApi.Settings;
            _settingApiService.Update(entity);
            await _settingApiService.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Api(Guid id)
        {
            var settingApi = await _settingApiService.GetByIdAsync(id);
            return Ok(settingApi.To<SettingApiModel>());
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _settingApiService.Delete(id);
            await _settingApiService.CommitAsync();
            return Ok();
        }

        [HttpPost("execute")]
        public async Task<IActionResult> Execute()
        {
            return Ok();
        }
    }
}