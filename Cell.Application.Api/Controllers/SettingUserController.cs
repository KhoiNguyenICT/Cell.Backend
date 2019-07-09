using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cell.Application.Api.Commands;
using Cell.Core.Errors;
using Cell.Core.Extensions;
using Cell.Core.Repositories;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Cell.Application.Api.Controllers
{
    public class SettingUserController : CellController<SecurityUser, SettingUserCommand>
    {
        private readonly ISecurityUserRepository _securityUserRepository;

        public SettingUserController(IValidator<SecurityUser> entityValidator, ISecurityUserRepository securityUserRepository) : base(entityValidator)
        {
            _securityUserRepository = securityUserRepository;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchCommand command)
        {
            var spec = SecurityUserSpecs.SearchByQuery(command.Query);
            var queryable = _securityUserRepository.QueryAsync(spec, command.Sorts);
            var items = await queryable.Skip(command.Skip).Take(command.Take).ToListAsync();
            return Ok(new QueryResult<SettingUserCommand>
            {
                Count = queryable.Count(),
                Items = items.To<List<SettingUserCommand>>()
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SettingUserCommand command)
        {
            await ValidateModel(command);
            var spec = SecurityUserSpecs.GetByAccountSpec(command.Account)
                .Or(SecurityUserSpecs.GetByEmailSpec(command.Email));
            var isInvalid = await _securityUserRepository.ExistsAsync(spec);
            if (isInvalid)
                throw new CellException("User name or account must be unique");
            _securityUserRepository.Add(new SecurityUser(
                command.Code,
                command.Description,
                command.Account,
                command.Email,
                command.Password.ToSha256(),
                command.Phone,
                JsonConvert.SerializeObject(command.Settings)));
            await _securityUserRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] SettingUserCommand command)
        {
            await ValidateModel(command);
            var settingUser = await _securityUserRepository.GetByIdAsync(command.Id);
            settingUser.Update(
                command.Description,
                command.Email,
                command.Phone,
                JsonConvert.SerializeObject(command.Settings));
            await _securityUserRepository.CommitAsync();
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SettingUser(Guid id)
        {
            var result = await _securityUserRepository.GetByIdAsync(id);
            return Ok(result.To<SettingUserCommand>());
        }
    }
}