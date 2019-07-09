using Cell.Application.Api.Commands;
using Cell.Application.Api.Commands.Others;
using Cell.Core.Extensions;
using Cell.Domain.Aggregates.SecurityUserAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Cell.Application.Api.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly ISecurityUserRepository _securityUserRepository;
        private readonly IConfiguration _config;

        public AuthenticationController(
            ISecurityUserRepository securityUserRepository,
            IConfiguration config)
        {
            _securityUserRepository = securityUserRepository;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
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
                new Claim("roles", string.Join(";", userCommand.Settings.Roles)),
                new Claim("permissions", ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.UtcNow.AddHours(int.Parse(_config["ExpiredTime"])),
                signingCredentials: credentials);

            return new OkObjectResult(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
    }
}