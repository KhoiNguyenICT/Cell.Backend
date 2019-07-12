using Cell.Application.Api.Commands;
using Cell.Core.Constants;
using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Cell.Domain.Aggregates.SecuritySessionAggregate;
using FluentValidation;

namespace Cell.Application.Api.Controllers
{
    public class SettingSessionController : CellController<SecuritySession, SettingSessionCommand>
    {
        public SettingSessionController(
            IValidator<SecuritySession> entityValidator,
            ISecurityPermissionRepository securityPermissionRepository,
            ISecurityGroupRepository securityGroupRepository) : base(entityValidator, securityPermissionRepository, securityGroupRepository)
        {
            AuthorizedType = ConfigurationKeys.SecuritySession;
        }
    }
}