using Cell.Application.Api.Models.SettingFilter;
using Cell.Model.Entities.SettingFilterEntity;

namespace Cell.Application.Api.Controllers
{
    public class SettingFilterController : CellController<SettingFilter, SettingFilterCreateModel, SettingFilterUpdateModel, ISettingFilterService>
    {
        public SettingFilterController(ISettingFilterService service) : base(service)
        {
        }
    }
}