using Cell.Application.Api.Commands;
using Cell.Application.Api.Commands.Others;
using Cell.Core.Extensions;
using Cell.Domain.Aggregates.SecuritySessionAggregate;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
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

        public AuthenticationController(
            ISecurityUserRepository securityUserRepository,
            IConfiguration config, ISecuritySessionRepository securitySessionRepository)
        {
            _securityUserRepository = securityUserRepository;
            _config = config;
            _securitySessionRepository = securitySessionRepository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            if (!ModelState.IsValid) return new BadRequestObjectResult(command);

            var spec = SecurityUserSpecs.GetByAccountSpec(command.Account);
            var user = await _securityUserRepository.GetSingleAsync(spec);
            if (user == null) return new BadRequestObjectResult("Login failure");
            var result = user.EncryptedPassword == command.Password.ToSha256();
            if (!result) return new BadRequestObjectResult("Login failed");
            var userCommand = user.To<SettingUserCommand>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, userCommand.Account),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("fullName", userCommand.Settings.Information.FullName),
                new Claim("roles", string.Join(";", JsonConvert.SerializeObject(userCommand.Settings.Roles))),
                new Claim("departments", string.Join(";", JsonConvert.SerializeObject(userCommand.Settings.Departments))),
                new Claim("permissions", ""),
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
            var session = _securitySessionRepository.Add(new SecuritySession(
                DateTime.UtcNow.AddHours(int.Parse(_config["ExpiredTime"])),
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
            return Ok(session.Id);
        }
    }
}