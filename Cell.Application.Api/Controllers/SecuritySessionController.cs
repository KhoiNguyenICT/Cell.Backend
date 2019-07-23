using Cell.Application.Api.Models.SecuritySession;
using Cell.Model.Entities.SecuritySessionEntity;

namespace Cell.Application.Api.Controllers
{
    public class SecuritySessionController : CellController<SecuritySession, SecuritySessionCreateModel, object, ISecuritySessionService>
    {
        public SecuritySessionController(ISecuritySessionService service) : base(service)
        {
        }
    }
}