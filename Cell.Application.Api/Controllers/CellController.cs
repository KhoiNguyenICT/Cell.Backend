using Cell.Common.Extensions;
using Cell.Common.Linq;
using Cell.Common.SeedWork;
using Cell.Common.Specifications;
using Cell.Core.Errors;
using Cell.Model;
using Cell.Model.Entities.SecurityPermissionEntity;
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
        private readonly ISecurityPermissionService _securityPermissionService;
        protected string AuthorizedType;

        private Guid CurrentSessionId => Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Session"] ?? throw new InvalidOperationException());

        private Guid CurrentAccountId => Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Account"] ?? throw new InvalidOperationException());

        public CellController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<TEntity> entityValidator,
            ISecurityPermissionService securityPermissionService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _entityValidator = entityValidator;
            _securityPermissionService = securityPermissionService;
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
            var groupIds = await _securityPermissionService.GetGroupIdsByAccountId(CurrentAccountId);
            var entities = from entity in _context.Set<TEntity>()
                           join permission in _context.SecurityPermissions on entity.Id equals permission.ObjectId
                           where groupIds.Contains(permission.AuthorizedId)
                           select entity;
            if (spec == null)
            {
                return new QueryResult<TEntity>(entities.Count(), entities.ToList());
            }
            entities = entities.Where(spec.Predicate).SortBy(sorts ?? StringExtensions.GetDefaultSorts());
            var result = await entities.Skip(skip).Take(take).ToListAsync();
            return new QueryResult<TEntity>(entities.Count(), result);
        }

        protected virtual async Task<QueryResult<TEntity>> Queryable(string[] sorts = null)
        {
            return await Queryable(null, sorts);
        }

        protected async Task InitPermission(Guid objectId, string objectName)
        {
            await _securityPermissionService.InitPermission(objectId, objectName, AuthorizedType);
        }
    }
}