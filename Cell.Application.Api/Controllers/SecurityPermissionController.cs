using Cell.Common.Constants;
using Cell.Model;
using Cell.Model.Entities.SecurityPermissionEntity;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Cell.Application.Api.Controllers
{
    public class SecurityPermissionController : CellController<SecurityPermission>
    {
        public SecurityPermissionController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SecurityPermission> entityValidator) :
            base(context, httpContextAccessor, entityValidator)
        {
            AuthorizedType = ConfigurationKeys.SecurityPermissionTableName;
        }
    }
}