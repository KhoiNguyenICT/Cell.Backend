using Cell.Common.Extensions;
using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingActionEntity;
using Cell.Model.Models.Others;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cell.Model.Models.SettingAction;

namespace Cell.Application.Api.Controllers
{
    public class SettingActionController : CellController<SettingAction>
    {
        private readonly ISettingActionService _settingActionService;

        public SettingActionController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingAction> entityValidator,
            ISettingActionService settingActionService) :
            base(context, httpContextAccessor, entityValidator)
        {
            _settingActionService = settingActionService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingActionModel model)
        {
            var spec = SettingActionSpecs.SearchByQuery(model.Query)
                .And(SettingActionSpecs.SearchByTableId(model.TableId));
            var queryable = Queryable(spec);
            var items = await queryable.Skip(model.Skip).Take(model.Take).ToListAsync();
            return Ok(new QueryResult<SettingActionModel>
            {
                Count = Queryable(spec).Count(),
                Items = items.To<List<SettingActionModel>>()
            });
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingTable(Guid id)
        {
            var settingField = await _settingActionService.GetByIdAsync(id);
            return Ok(settingField);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]SettingActionCreateModel model)
        {
            await ValidateModel(model.To<SettingActionModel>());
            var settingAction = model.To<SettingAction>();
            var result = await _settingActionService.AddAsync(new SettingAction
            {
                Code = settingAction.Code,
                Name = settingAction.Name,
                Description = settingAction.Description,
                ContainerType = settingAction.ContainerType,
                Settings = settingAction.Settings,
                TableId = settingAction.TableId,
                TableName = settingAction.TableName
            });
            await AssignPermission(result.Id, result.Name);
            await _settingActionService.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingActionUpdateModel model)
        {
            var settingAction = model.To<SettingAction>();
            var entity = await _settingActionService.GetByIdAsync(model.Id);
            entity.Code = settingAction.Code;
            entity.Name = settingAction.Name;
            entity.Description = settingAction.Description;
            entity.ContainerType = settingAction.ContainerType;
            entity.Settings = settingAction.Settings;
            _settingActionService.Update(entity);
            await _settingActionService.CommitAsync();
            return Ok();
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _settingActionService.Delete(id);
            await _settingActionService.CommitAsync();
            return Ok();
        }
    }
}