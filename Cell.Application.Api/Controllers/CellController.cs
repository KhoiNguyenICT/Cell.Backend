using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Common.Linq;
using Cell.Common.SeedWork;
using Cell.Common.Specifications;
using Cell.Core.Errors;
using Cell.Model;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Models.SecuritySession;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cell.Application.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CellController<TEntity> : ControllerBase
        where TEntity : Entity
    {
        protected readonly AppDbContext Context;
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
            Context = context;
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

        protected virtual async Task<QueryResult<TEntity>> Queryable(
            ISpecification<TEntity> spec, 
            string[] sorts = null, 
            int skip = 0, 
            int take = 10)
        {
            var entities = from entity in Context.Set<TEntity>()
                           join permission in Context.SecurityPermissions on entity.Id equals permission.ObjectId
                           where GroupIds().Contains(permission.AuthorizedId)
                           select entity;
            if (spec == null)
            {
                var result = await entities.ToListAsync();
                return new QueryResult<TEntity>(entities.Count(), entities.ToList());
            }
            entities = entities.Where(spec.Predicate).SortBy(sorts ?? StringExtensions.GetDefaultSorts());
            return new QueryResult<TEntity>(entities.Count(),  entities.Skip(skip).Take(take).ToList());
        }

        protected virtual async Task<QueryResult<TEntity>> Queryable(string[] sorts = null)
        {
            return await Queryable(null, sorts);
        }

        private List<Guid> GroupIds()
        {
            var sessionId = Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Session"]);
            var session = Context.SecuritySessions.Find(sessionId);
            var groupIds = session.To<SecuritySessionModel>().Settings.GroupIds;
            return groupIds;
        }

        protected async Task InitPermission(Guid objectId, string objectName)
        {
            var systemRole = Context.SecurityGroups.FirstOrDefault(t => t.Code == "SYSTEM.ROLE" && t.Name == "System");
            if (systemRole == null) return;
            var securityPermissions = new List<SecurityPermission>
            {
                new SecurityPermission
                {
                    AuthorizedId = systemRole.Id,
                    AuthorizedType = AuthorizedType,
                    ObjectId = objectId,
                    ObjectName = objectName,
                    TableName = ConfigurationKeys.SecurityGroupTableName
                },
                new SecurityPermission
                {
                    AuthorizedId = CurrentAccountId,
                    AuthorizedType = AuthorizedType,
                    ObjectId = objectId,
                    ObjectName = objectName,
                    TableName = ConfigurationKeys.SecurityUserTableName
                }
            };
            await Context.SecurityPermissions.AddRangeAsync(securityPermissions);
            await Context.SaveChangesAsync();
        }
    }
}