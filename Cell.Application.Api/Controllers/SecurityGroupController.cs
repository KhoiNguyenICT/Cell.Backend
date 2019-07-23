using Cell.Application.Api.Models.SecurityGroup;
using Cell.Model.Entities.SecurityGroupEntity;

namespace Cell.Application.Api.Controllers
{
    public class SecurityGroupController : CellController<SecurityGroup, SecurityGroupCreateModel, SecurityGroupUpdateModel, ISecurityGroupService>
    {
        public SecurityGroupController(ISecurityGroupService service) : base(service)
        {
        }
    }
}