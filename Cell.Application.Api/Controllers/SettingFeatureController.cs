using Cell.Application.Api.Models.SettingFeature;
using Cell.Model.Entities.SettingFeatureEntity;

namespace Cell.Application.Api.Controllers
{
    public class SettingFeatureController : CellController<SettingFeature, SettingFeatureCreateModel, SettingFeatureUpdateModel, ISettingFeatureService>
    {
        public SettingFeatureController(ISettingFeatureService service) : base(service)
        {
        }
    }
}