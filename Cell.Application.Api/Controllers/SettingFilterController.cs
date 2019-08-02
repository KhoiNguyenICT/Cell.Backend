using Cell.Common.Constants;
using Cell.Model;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Entities.SettingFilterEntity;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Cell.Application.Api.Controllers
{
    public class SettingFilterController : CellController<SettingFilter>
    {
        public SettingFilterController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingFilter> entityValidator,
            ISecurityPermissionService securityPermissionService) :
            base(context, httpContextAccessor, entityValidator, securityPermissionService)
        {
            AuthorizedType = ConfigurationKeys.SettingFilterTableName;
        }
    }
}