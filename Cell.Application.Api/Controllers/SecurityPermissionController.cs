using Cell.Application.Api.Models.SecurityPermission;
using Cell.Model.Entities.SecurityPermissionEntity;

namespace Cell.Application.Api.Controllers
{
    public class SecurityPermissionController : CellController<SecurityPermission, SecurityPermissionCreateModel, object, ISecurityPermissionService>
    {
        public SecurityPermissionController(ISecurityPermissionService service) : base(service)
        {
        }
    }
}