using Cell.Application.Api.Models.SettingField;
using Cell.Model.Entities.SettingFieldEntity;

namespace Cell.Application.Api.Controllers
{
    public class SettingFieldController : CellController<SettingField, SettingFieldCreateModel, SettingFieldUpdateModel, ISettingFieldService>
    {
        public SettingFieldController(ISettingFieldService service) : base(service)
        {
        }
    }
}