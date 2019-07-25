﻿using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingActionInstanceEntity;
using Cell.Model.Entities.SettingFieldInstanceEntity;
using Cell.Model.Entities.SettingViewEntity;
using Cell.Model.Models.Others;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cell.Model.Models.SettingActionInstance;
using Cell.Model.Models.SettingFieldInstance;
using Cell.Model.Models.SettingView;

namespace Cell.Application.Api.Controllers
{
    public class SettingViewController : CellController<SettingView>
    {
        private readonly ISettingViewService _settingViewService;
        private readonly ISettingFieldInstanceService _settingFieldInstanceService;
        private readonly ISettingActionInstanceService _settingActionInstanceService;

        public SettingViewController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingView> entityValidator,
            ISettingViewService settingViewService,
            ISettingFieldInstanceService settingFieldInstanceService,
            ISettingActionInstanceService settingActionInstanceService) :
            base(context, httpContextAccessor, entityValidator)
        {
            _settingViewService = settingViewService;
            _settingFieldInstanceService = settingFieldInstanceService;
            _settingActionInstanceService = settingActionInstanceService;
            AuthorizedType = ConfigurationKeys.SettingView;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingViewModel model)
        {
            var spec = SettingViewSpecs.SearchByQuery(model.Query);
            var queryable = Queryable(spec);
            var items = await queryable.OrderBy(x => x.Name).Skip(model.Skip).Take(model.Take).ToListAsync();
            return Ok(new QueryResult<SettingViewModel>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingViewModel>>()
            });
        }

        [HttpPost("settingViewSettings/{id}")]
        public async Task<IActionResult> SettingView(Guid id)
        {
            var settingFieldInstanceSpecs = SettingFieldInstanceSpecs.GetManyByParentId(id);
            var settingActionInstanceSpecs = SettingActionInstanceSpecs.GetManyByParentId(id);
            var settingView = await _settingViewService.GetByIdAsync(id);
            var settingFieldInstances = await _settingFieldInstanceService.GetManyAsync(settingFieldInstanceSpecs);
            var settingActionInstances = await _settingActionInstanceService.GetManyAsync(settingActionInstanceSpecs);
            return Ok(new
            {
                settingView = settingView.To<SettingViewModel>(),
                settingFieldInstances = settingFieldInstances.To<List<SettingFieldInstanceModel>>(),
                settingActionInstances = settingActionInstances.To<List<SettingActionInstanceModel>>()
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingViewCreateModel model)
        {
            await ValidateModel(model.To<SettingViewModel>());
            var settingView = model.To<SettingView>();
            var result = await _settingViewService.AddAsync(new SettingView
            {
                Code = settingView.Code,
                Name = settingView.Name,
                Description = settingView.Description,
                TableId = settingView.TableId,
                TableName = settingView.TableName,
                Settings = settingView.Settings
            });
            await AssignPermission(result.Id, result.Name);
            await _settingViewService.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingViewUpdateModel model)
        {
            var settingView = model.To<SettingView>();
            var entity = await _settingViewService.GetByIdAsync(model.Id);
            entity.Name = settingView.Name;
            entity.Description = settingView.Description;
            entity.Settings = settingView.Settings;
            _settingViewService.Update(entity);
            await _settingViewService.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _settingViewService.Delete(id);
            await _settingViewService.CommitAsync();
            return Ok();
        }
    }
}