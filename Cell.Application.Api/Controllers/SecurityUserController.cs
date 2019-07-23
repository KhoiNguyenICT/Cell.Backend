using Cell.Application.Api.Models.SecurityUser;
using Cell.Model.Entities.SecurityUserEntity;

namespace Cell.Application.Api.Controllers
{
    public class SecurityUserController : CellController<SecurityUser, SecurityUserCreateModel, SecurityUserUpdateModel, ISecurityUserService>
    {
        public SecurityUserController(ISecurityUserService service) : base(service)
        {
        }
    }
}