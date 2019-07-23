using Cell.Application.Api.Models.SettingView;
using Cell.Model.Entities.SettingViewEntity;

namespace Cell.Application.Api.Controllers
{
    public class SettingViewController : CellController<SettingView, SettingViewCreateModel, SettingViewUpdateModel, ISettingViewService>
    {
        public SettingViewController(ISettingViewService service) : base(service)
        {
        }
    }
}