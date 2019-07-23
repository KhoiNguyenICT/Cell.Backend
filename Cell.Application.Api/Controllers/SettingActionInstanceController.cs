using Cell.Application.Api.Models.SettingActionInstance;
using Cell.Model.Entities.SettingActionInstanceEntity;

namespace Cell.Application.Api.Controllers
{
    public class SettingActionInstanceController : CellController<SettingActionInstance, SettingActionInstanceCreateModel, SettingActionInstanceUpdateModel, ISettingActionInstanceService>
    {
        public SettingActionInstanceController(ISettingActionInstanceService service) : base(service)
        {
        }
    }
}