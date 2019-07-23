using Cell.Application.Api.Models.SettingForm;
using Cell.Model.Entities.SettingFormEntity;

namespace Cell.Application.Api.Controllers
{
    public class SettingFormController : CellController<SettingForm, SettingFormCreateModel, SettingFormUpdateModel, ISettingFormService>
    {
        public SettingFormController(ISettingFormService service) : base(service)
        {
        }
    }
}