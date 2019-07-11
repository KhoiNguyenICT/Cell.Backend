using System.Threading.Tasks;
using Cell.Application.Api.Commands;
using Cell.Core.Errors;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cell.Application.Api.Controllers
{
    public class SettingPermissionController : CellController<SecurityPermission, SettingPermissionCommand>
    {
        private readonly ISecurityPermissionRepository _securityPermissionRepository;

        public SettingPermissionController(
            IValidator<SecurityPermission> entityValidator, 
            ISecurityPermissionRepository securityPermissionRepository) : base(entityValidator)
        {
            _securityPermissionRepository = securityPermissionRepository;
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