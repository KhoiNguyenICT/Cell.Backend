using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Common.SeedWork;
using Cell.Core.Errors;
using Cell.Model;
using Cell.Model.Entities.SecurityUserEntity;
using Cell.Model.Models.Others;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cell.Model.Models.SecurityGroup;
using Cell.Model.Models.SecurityUser;

namespace Cell.Application.Api.Controllers
{
    public class SecurityUserController : CellController<SecurityUser>
    {
        private readonly ISecurityUserService _securityUserService;

        public SecurityUserController(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IValidator<SecurityUser> entityValidator,
            ISecurityUserService securityUserService) :
            base(context, httpContextAccessor, entityValidator)
        {
            AuthorizedType = ConfigurationKeys.SecurityUser;
            _securityUserService = securityUserService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchModel model)
        {
            var spec = SecurityUserSpecs.SearchByQuery(model.Query);
            var queryable = Queryable(spec);
            var items = await queryable.Skip(model.Skip).Take(model.Take).ToListAsync();
            return Ok(new QueryResult<SecurityUserModel>
            {
                Count = queryable.Count(),
                Items = items.To<List<SecurityUserModel>>()
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SecurityUserCreateModel model)
        {
            await ValidateModel(model.To<SecurityUserModel>());
            var spec = SecurityUserSpecs.GetByAccountSpec(model.Account)
                .Or(SecurityUserSpecs.GetByEmailSpec(model.Email));
            var isInvalid = await _securityUserService.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("User name or account must be unique");
            var result = await _securityUserService.AddAsync(model.To<SecurityUser>());
            await AssignPermission(result.Id, result.Account);
            await _securityUserService.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SecurityGroupUpdateModel model)
        {
            await ValidateModel(model.To<SecurityUserModel>());
            var securityGroup = model.To<SecurityUser>();
            var entity = await _securityUserService.GetByIdAsync(model.Id);
            entity.Description = securityGroup.Description;
            entity.Email = securityGroup.Email;
            entity.Phone = securityGroup.Phone;
            entity.Settings = securityGroup.Settings;
            await _securityUserService.CommitAsync();
            return Ok();
        }

        [HttpPost("updateGroup")]
        public async Task<IActionResult> UpdateGroup(UpdateGroupForUserModel model)
        {
            var entity = await _securityUserService.GetByIdAsync(model.UserId);
            var securityUserModel = entity.To<SecurityUserModel>();
            securityUserModel.Settings.Departments = model.Departments;
            securityUserModel.Settings.Roles = model.Roles;
            securityUserModel.Settings.DefaultDepartmentData = model.DefaultDepartmentData;
            securityUserModel.Settings.DefaultRoleData = model.DefaultRoleData;
            _securityUserService.Update(securityUserModel.To<SecurityUser>());
            await _securityUserService.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingUser(Guid id)
        {
            var result = await _securityUserService.GetByIdAsync(id);
            return Ok(result.To<SecurityUserModel>());
        }
    }
}