using Cell.Common.Constants;
using Cell.Model;
using Cell.Model.Entities.SecuritySessionEntity;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Cell.Application.Api.Controllers
{
    public class SecuritySessionController : CellController<SecuritySession>
    {
        public SecuritySessionController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SecuritySession> entityValidator) :
            base(context, httpContextAccessor, entityValidator)
        {
            AuthorizedType = ConfigurationKeys.SecuritySessionTableName;
        }
    }
}