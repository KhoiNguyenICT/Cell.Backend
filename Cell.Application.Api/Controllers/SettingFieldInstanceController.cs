using Cell.Application.Api.Models.SettingFieldInstance;
using Cell.Model.Entities.SettingFieldInstanceEntity;

namespace Cell.Application.Api.Controllers
{
    public class SettingFieldInstanceController : CellController<SettingFieldInstance, SettingFieldInstanceCreateModel, SettingFieldInstanceUpdateModel, ISettingFieldInstanceService>
    {
        public SettingFieldInstanceController(ISettingFieldInstanceService service) : base(service)
        {
        }
    }
}