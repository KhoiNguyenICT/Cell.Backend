using Cell.Application.Api.Commands;
using Cell.Core.Constants;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.SeedWork;
using Cell.Core.Specifications;
using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Cell.Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CellController<TEntity, TCommand> : ControllerBase
        where TEntity : Entity
        where TCommand : Command
    {
        private readonly IValidator<TEntity> _entityValidator;
        private readonly ISecurityPermissionRepository _securityPermissionRepository;
        private readonly ISecurityGroupRepository _securityGroupRepository;
        protected string AuthorizedType;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISecurityUserRepository _securityUserRepository;
        private Guid CurrentAccountId => Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Account"]);
        public Guid CurrentSessionId => Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Session"]);

        public CellController(IValidator<TEntity> entityValidator,
            ISecurityPermissionRepository securityPermissionRepository,
            ISecurityGroupRepository securityGroupRepository,
            IHttpContextAccessor httpContextAccessor, 
            ISecurityUserRepository securityUserRepository)
        {
            _entityValidator = entityValidator;
            _securityPermissionRepository = securityPermissionRepository;
            _securityGroupRepository = securityGroupRepository;
            _httpContextAccessor = httpContextAccessor;
            _securityUserRepository = securityUserRepository;
        }

        public override BadRequestObjectResult BadRequest(ModelStateDictionary modelState)
        {
            var formattedError = FormatError(modelState);
            return new BadRequestObjectResult(formattedError);
        }

        private CellError FormatError(ModelStateDictionary modelStateDictionary)
        {
            return new CellError(
                modelStateDictionary
                    .Where(t => !string.IsNullOrEmpty(t.Key) && t.Value.ValidationState == ModelValidationState.Invalid)
                    .SelectMany(t => t.Value.Errors.Select(x => new CellValidationError(t.Key.IndexOf('.') > 0 ? t.Key.Substring(t.Key.IndexOf('.') + 1) : t.Key, x.ErrorMessage)))
            );
        }

        protected async Task ValidateModel(Command command)
        {
            var isValidator = await _entityValidator.ValidateAsync(command.To<TEntity>());
            if (!isValidator.IsValid)
                throw new CellException(isValidator);
        }

        protected async Task ValidateModels(IEnumerable<Command> commands)
        {
            foreach (var command in commands)
            {
                var isValidator = await _entityValidator.ValidateAsync(command.To<TEntity>());
                if (!isValidator.IsValid)
                    throw new CellException(isValidator);
            }
        }

        protected async Task AssignPermission(Guid objectId, string objectName)
        {
            var systemRoleSpec = new Specification<SecurityGroup>(t => t.Code == "SYSTEM.ROLE" && t.Name == "System");
            var systemRole = await _securityGroupRepository.GetSingleAsync(systemRoleSpec);
            _securityPermissionRepository.Add(new SecurityPermission(
                systemRole.Id,
                AuthorizedType,
                objectId,
                objectName,
                null,
                ConfigurationKeys.SecurityGroup));
            _securityPermissionRepository.Add(new SecurityPermission(
                CurrentAccountId,
                AuthorizedType,
                objectId,
                objectName,
                null,
                ConfigurationKeys.SecurityUser));
        }

        protected async Task RemovePermission(Guid objectId)
        {
            var permissionSpec = new Specification<SecurityPermission>(t => t.ObjectId == objectId);
            var permission = await _securityPermissionRepository.GetSingleAsync(permissionSpec);
            _securityPermissionRepository.Delete(permission.Id);
            await _securityPermissionRepository.CommitAsync();
        }

        public async Task<List<Guid>> ObjectIds(string tableName)
        {
            var user = await _securityUserRepository.GetByIdAsync(CurrentAccountId);
            var userCommand = user.To<SettingUserCommand>();
            var authorizedIds = userCommand.Settings.Roles.Select(x => x.Id).ToList();
            authorizedIds.Add(CurrentAccountId);
            var permissions = await _securityPermissionRepository.QueryAsync()
                .Where(x => authorizedIds.Contains(x.AuthorizedId)).ToListAsync();
            return permissions.Select(x=>x.ObjectId).Distinct().ToList();
        }
    }
}