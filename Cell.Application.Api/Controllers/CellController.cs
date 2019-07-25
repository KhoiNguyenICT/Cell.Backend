using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Common.Linq;
using Cell.Common.SeedWork;
using Cell.Common.Specifications;
using Cell.Core.Errors;
using Cell.Model;
using Cell.Model.Entities.SecurityGroupEntity;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Models.SecuritySession;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CellController<TEntity> : ControllerBase
        where TEntity : Entity
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<TEntity> _entityValidator;
        protected string AuthorizedType;

        private Guid CurrentSessionId => Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Session"]);

        private Guid CurrentAccountId => Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Account"]);

        public CellController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<TEntity> entityValidator)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _entityValidator = entityValidator;
        }

        protected async Task ValidateModel(BaseModel model)
        {
            var isValidator = await _entityValidator.ValidateAsync(model.To<TEntity>());
            if (!isValidator.IsValid)
                throw new CellException(isValidator);
        }

        protected async Task ValidateModels(IEnumerable<BaseModel> models)
        {
            foreach (var command in models)
            {
                var isValidator = await _entityValidator.ValidateAsync(command.To<TEntity>());
                if (!isValidator.IsValid)
                    throw new CellException(isValidator);
            }
        }

        protected async Task AssignPermission(Guid objectId, string objectName)
        {
            var systemRoleSpec = new Specification<SecurityGroup>(t => t.Code == "SYSTEM.ROLE" && t.Name == "System");
            var systemRole = await _context.SecurityGroups.FirstOrDefaultAsync(systemRoleSpec.Predicate);
            var securityPermissions = new List<SecurityPermission>
            {
                new SecurityPermission
                {
                    AuthorizedId = systemRole.Id,
                    AuthorizedType = AuthorizedType,
                    ObjectId = objectId,
                    ObjectName = objectName,
                    TableName = ConfigurationKeys.SecurityGroup
                },
                new SecurityPermission
                {
                    AuthorizedId = CurrentAccountId,
                    AuthorizedType = AuthorizedType,
                    ObjectId = objectId,
                    ObjectName = objectName,
                    TableName = ConfigurationKeys.SecurityUser
                }
            };
            await _context.SecurityPermissions.AddRangeAsync(securityPermissions);
            await _context.SaveChangesAsync();
        }

        protected virtual IQueryable<TEntity> Queryable(ISpecification<TEntity> spec, string[] sorts = null)
        {
            var entities = from entity in _context.Set<TEntity>()
                           join permission in _context.SecurityPermissions on entity.Id equals permission.ObjectId
                           where GroupIds().Contains(permission.AuthorizedId)
                           select entity;
            entities = entities.Where(spec.Predicate).SortBy(sorts ?? StringExtensions.GetDefaultSorts());
            return entities;
        }

        protected virtual IQueryable<TEntity> Queryable(string[] sorts = null)
        {
            return Queryable(null, sorts);
        }

        private List<Guid> GroupIds()
        {
            var sessionId = Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Session"]);
            var session = _context.SecuritySessions.Find(sessionId);
            var groupIds = session.To<SecuritySessionModel>().Settings.GroupIds;
            return groupIds;
        }
    }
}