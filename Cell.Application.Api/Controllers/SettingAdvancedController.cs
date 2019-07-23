using Cell.Application.Api.Models.SettingAdvanced;
using Cell.Model.Entities.SettingAdvancedEntity;

namespace Cell.Application.Api.Controllers
{
    public class SettingAdvancedController : CellController<SettingAdvanced, SettingAdvancedCreateModel, SettingAdvancedUpdateModel, ISettingAdvancedService>
    {
        public SettingAdvancedController(ISettingAdvancedService service) : base(service)
        {
        }
    }
}