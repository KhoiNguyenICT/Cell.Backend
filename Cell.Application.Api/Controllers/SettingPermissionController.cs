using Cell.Application.Api.Commands;
using Cell.Core.Constants;
using Cell.Core.Errors;
using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class SettingPermissionController : CellController<SecurityPermission, SettingPermissionCommand>
    {
        private readonly ISecurityPermissionRepository _securityPermissionRepository;

        public SettingPermissionController(
            IValidator<SecurityPermission> entityValidator,
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
            _securityPermissionRepository = securityPermissionRepository;
            AuthorizedType = ConfigurationKeys.SecurityPermission;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(SettingPermissionCommand command)
        {
            await ValidateModel(command);
            var spec = SecurityPermissionSpecs.GetByAuthorizedIdSpec(command.AuthorizedId)
                .And(SecurityPermissionSpecs.GetByObjectIdSpec(command.ObjectId));
            var isInvalid = await _securityPermissionRepository.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("This permission existed");
            _securityPermissionRepository.Add(new SecurityPermission(
                command.AuthorizedId,
                command.AuthorizedType,
                command.ObjectId,
                command.ObjectName,
                JsonConvert.SerializeObject(command.Settings),
                command.TableName));
            await _securityPermissionRepository.CommitAsync();
            return Ok();
        }
    }
}