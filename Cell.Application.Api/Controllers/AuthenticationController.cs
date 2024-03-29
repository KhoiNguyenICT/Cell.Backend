﻿using Cell.Application.Api.Commands;
using Cell.Application.Api.Commands.Others;
using Cell.Core.Extensions;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using Cell.Domain.Aggregates.SecuritySessionAggregate;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISecurityUserRepository _securityUserRepository;
        private readonly ISecuritySessionRepository _securitySessionRepository;
        private readonly IConfiguration _config;
        private readonly ISecurityPermissionRepository _securityPermissionRepository;

        public AuthenticationController(
            ISecurityUserRepository securityUserRepository,
            IConfiguration config, ISecuritySessionRepository securitySessionRepository,
            ISecurityPermissionRepository securityPermissionRepository)
        {
            _securityUserRepository = securityUserRepository;
            _config = config;
            _securitySessionRepository = securitySessionRepository;
            _securityPermissionRepository = securityPermissionRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            if (!ModelState.IsValid) return new BadRequestObjectResult(command);

            var spec = SecurityUserSpecs.GetByAccountSpec(command.Account);
            var user = await _securityUserRepository.GetSingleAsync(spec);
            if (user == null) return new BadRequestObjectResult("Login failure");
            var result = user.EncryptedPassword == command.Password.ToSha256();
            if (!result) return new BadRequestObjectResult("Login failed");
            var userCommand = user.To<SettingUserCommand>();
            var roles = userCommand.Settings.Roles;
            var roleIds = roles.Select(x => x.Id).ToList();
            var permission = _securityPermissionRepository
                .QueryAsync()
                .Where(x => roleIds.Contains(x.AuthorizedId))
                .Select(x => x.ObjectId);
            var claims = new[]
            {
                new Claim("email", user.Email),
                new Claim("account", userCommand.Account),
                new Claim("id", user.Id.ToString()),
                new Claim("fullName", userCommand.Settings.Information.FullName),
                new Claim("roles", string.Join(";", JsonConvert.SerializeObject(roles))),
                new Claim("departments", string.Join(";", JsonConvert.SerializeObject(userCommand.Settings.Departments))),
                new Claim("permissions", string.Join(";", permission)),
                new Claim("defaultDepartment", JsonConvert.SerializeObject(userCommand.Settings.DefaultDepartmentData)),
                new Claim("defaultRole", JsonConvert.SerializeObject(userCommand.Settings.DefaultRoleData)),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenNotEncrypt = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_config["ExpiredTime"])),
                signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenNotEncrypt);
            var tmp = DateTimeOffset.Now + TimeSpan.FromHours(int.Parse(_config["ExpiredTime"]));
            var session = _securitySessionRepository.Add(new SecuritySession(
                DateTimeOffset.Now + TimeSpan.FromHours(int.Parse(_config["ExpiredTime"])),
                DateTimeOffset.Now, 
                user.Id,
                user.Account,
                JsonConvert.SerializeObject(new SettingSessionSettingCommand
                {
                    Token = token,
                    UserAgent = command.UserAgent,
                    Browser = command.Browser,
                    BrowserVersion = command.BrowserVersion,
                    Device = command.Device,
                    IsDesktop = command.IsDesktop,
                    IsMobile = command.IsMobile,
                    IsTablet = command.IsTablet,
                    Os = command.Os,
                    OsVersion = command.OsVersion
                })));
            await _securitySessionRepository.CommitAsync();
            return Ok(new {token = token, session = session.Id});
        }

        [HttpPost("verifySession/{sessionId}")]
        public async Task<IActionResult> VerifySession(Guid sessionId)
        {
            var result = await _securitySessionRepository.VerifySession(sessionId);
            return Ok(result);
        }
    }
}