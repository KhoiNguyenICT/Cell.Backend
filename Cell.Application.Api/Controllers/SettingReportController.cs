using Cell.Common.Constants;
using Cell.Model;
using Cell.Model.Entities.SettingReportEntity;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Cell.Application.Api.Controllers
{
    public class SettingReportController : CellController<SettingReport>
    {
        public SettingReportController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingReport> entityValidator) :
            base(context, httpContextAccessor, entityValidator)
        {
            AuthorizedType = ConfigurationKeys.SettingReport;
        }
    }
}