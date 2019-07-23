using Cell.Application.Api.Models.SettingAction;
using Cell.Model.Entities.SettingActionEntity;

namespace Cell.Application.Api.Controllers
{
    public class SettingActionController : CellController<SettingAction, SettingActionCreateModel, SettingActionUpdateModel, ISettingActionService>
    {
        public SettingActionController(ISettingActionService service) : base(service)
        {
        }
    }
}