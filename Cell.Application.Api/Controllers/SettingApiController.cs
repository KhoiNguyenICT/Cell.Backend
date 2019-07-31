using Cell.Model;
using Cell.Model.Entities.SettingApiEntity;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Cell.Application.Api.Controllers
{
    public class SettingApiController : CellController<SettingApi>
    {
        public SettingApiController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SettingApi> entityValidator) :
            base(context, httpContextAccessor, entityValidator)
        {
        }
    }
}