using Cell.Application.Api.Models.SettingReport;
using Cell.Model.Entities.SettingReportEntity;

namespace Cell.Application.Api.Controllers
{
    public class SettingReportController : CellController<SettingReport, SettingReportCreateModel, SettingReportUpdateModel, ISettingReportService>
    {
        public SettingReportController(ISettingReportService service) : base(service)
        {
        }
    }
}