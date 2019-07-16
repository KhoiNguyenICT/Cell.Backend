using Cell.Application.Api.Commands;
using Cell.Core.Constants;
using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Cell.Domain.Aggregates.SecuritySessionAggregate;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Cell.Application.Api.Controllers
{
    public class SettingSessionController : CellController<SecuritySession, SettingSessionCommand>
    {
        public SettingSessionController(
            IValidator<SecuritySession> entityValidator,
            ISecurityPermissionRepository securityPermissionRepository,
            ISecurityGroupRepository securityGroupRepository,
            IHttpContextAccessor httpContextAccessor,
            ISecurityUserRepository securityUserRepository) : base(
            entityValidator,
            securityPermissionRepository,
            securityGroupRepository,
            httpContextAccessor,
            securityUserRepository)
        {
            AuthorizedType = ConfigurationKeys.SecuritySession;
        }
    }
}