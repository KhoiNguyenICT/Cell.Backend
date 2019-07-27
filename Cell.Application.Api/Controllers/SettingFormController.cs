using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SettingActionEntity;
using Cell.Model.Entities.SettingFieldEntity;
using Cell.Model.Entities.SettingFormEntity;
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
using Cell.Model.Models.SettingField;
using Cell.Model.Models.SettingForm;

namespace Cell.Application.Api.Controllers
{
    public class SettingFormController : CellController<SettingForm>
    {
        private readonly ISettingFormService _settingFormService;
        private readonly ISettingActionService _settingActionService;
        private readonly ISettingFieldService _settingFieldService;

        public SettingFormController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingForm> entityValidator,
            ISettingFormService settingFormService,
            ISettingActionService settingActionService,
            ISettingFieldService settingFieldService) :
            base(context, httpContextAccessor, entityValidator)
        {
            _settingFormService = settingFormService;
            _settingActionService = settingActionService;
            _settingFieldService = settingFieldService;
            AuthorizedType = ConfigurationKeys.SettingFormTableName;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchSettingFormModel model)
        {
            var spec = SettingFormSpecs.SearchByQuery(model.Query);
            if (model.TableId != Guid.Empty)
                spec.And(SettingFormSpecs.SearchByTableId(model.TableId));
            var queryable = Queryable(spec);
            var items = await queryable.Skip(model.Skip).Take(model.Take).ToListAsync();
            return Ok(new QueryResult<SettingFormModel>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingFormModel>>()
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody]SettingFormCreateModel model)
        {
            await ValidateModel(model.To<SettingFormModel>());
            var settingForm = model.To<SettingForm>();
            var result = await _settingFormService.AddAsync(new SettingForm
            {
                Name = settingForm.Name,
                Description = settingForm.Description,
                LayoutId = settingForm.LayoutId,
                Settings = settingForm.Settings,
                TableId = settingForm.TableId,
                TableName = settingForm.TableName
            });
            await InitPermission(result.Id, result.Name);
            await _settingFormService.CommitAsync();
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]SettingFormUpdateModel model)
        {
            var entity = await _settingFormService.GetByIdAsync(model.Id);
            var settingForm = model.To<SettingForm>();
            entity.Name = settingForm.Name;
            entity.Description = settingForm.Description;
            entity.LayoutId = settingForm.LayoutId;
            entity.Settings = settingForm.Settings;
            _settingFormService.Update(entity);
            await _settingFormService.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingForm(Guid id)
        {
            var settingForm = await _settingFormService.GetByIdAsync(id);
            return Ok(settingForm.To<SettingFormModel>());
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _settingFormService.Delete(id);
            await _settingFormService.CommitAsync();
            return Ok();
        }

        [HttpPost("searchSettingByForm/{id}")]
        public async Task<IActionResult> SearchSettingByForm(Guid id)
        {
            var settingForm = await _settingFormService.GetByIdAsync(id);
            var settingFormCommand = settingForm.To<SettingFormModel>();
            var settingFieldSpec = SettingFieldSpecs.SearchByTableId(settingForm.TableId);
            var settingFields = await _settingFieldService.GetManyAsync(settingFieldSpec);
            var settingFieldsCommand = settingFields.To<List<SettingFieldModel>>();
            var settingActionSpec = SettingActionSpecs.SearchByTableId(settingForm.TableId);
            var settingActions = await _settingActionService.GetManyAsync(settingActionSpec);
            var settingActionsCommand = settingActions.To<List<SettingActionModel>>();
            return Ok(new
            {
                settingForm = settingFormCommand,
                settingFields = settingFieldsCommand,
                settingActions = settingActionsCommand
            });
        }
    }
}