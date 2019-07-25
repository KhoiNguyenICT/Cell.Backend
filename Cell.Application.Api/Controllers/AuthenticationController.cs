using Cell.Common.Extensions;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Entities.SecuritySessionEntity;
using Cell.Model.Entities.SecurityUserEntity;
using Cell.Model.Models.Others;
using Cell.Model.Models.SecuritySession;
using Cell.Model.Models.SecurityUser;
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
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISecurityUserService _securityUserService;
        private readonly ISecuritySessionService _securitySessionService;
        private readonly IConfiguration _config;

        public AuthenticationController(
            ISecurityUserService securityUserService,
            IConfiguration config, ISecuritySessionService securitySessionService)
        {
            _securityUserService = securityUserService;
            _config = config;
            _securitySessionService = securitySessionService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid) return new BadRequestObjectResult(model);

            var spec = SecurityUserSpecs.GetByAccountSpec(model.Account);
            var user = await _securityUserService.GetSingleAsync(spec);
            if (user == null) return new BadRequestObjectResult("Login failure");
            var result = user.EncryptedPassword == model.Password.ToSha256();
            if (!result) return new BadRequestObjectResult("Login failed");
            var userCommand = user.To<SecurityUserModel>();
            var groups = userCommand.Settings.Roles.Concat(userCommand.Settings.Departments);
            var groupsIds = groups.Select(x => x.Id).ToList();
            var roles = userCommand.Settings.Roles;
            var claims = new[]
            {
                new Claim("email", user.Email),
                new Claim("account", userCommand.Account),
                new Claim("id", user.Id.ToString()),
                new Claim("fullName", userCommand.Settings.Information.FullName),
                new Claim("roles", string.Join(";", JsonConvert.SerializeObject(roles))),
                new Claim("departments", string.Join(";", JsonConvert.SerializeObject(userCommand.Settings.Departments))),
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
            var securitySession = new SecuritySession
            {
                ExpiredTime = DateTimeOffset.Now + TimeSpan.FromHours(int.Parse(_config["ExpiredTime"])),
                SigninTime = DateTimeOffset.Now,
                UserId = userCommand.Id,
                UserAccount = userCommand.Account,
                Settings = JsonConvert.SerializeObject(new SecuritySessionSettingsModel
                {
                    Token = token,
                    UserAgent = model.UserAgent,
                    Browser = model.Browser,
                    BrowserVersion = model.BrowserVersion,
                    Device = model.Device,
                    IsDesktop = model.IsDesktop,
                    IsMobile = model.IsMobile,
                    IsTablet = model.IsTablet,
                    Os = model.Os,
                    OsVersion = model.OsVersion,
                    GroupIds = groupsIds
                })
            };
            var session = await _securitySessionService.AddAsync(securitySession);
            await _securitySessionService.CommitAsync();
            return Ok(new { token = token, session = session.Id });
        }

        [HttpPost("verifySession/{sessionId}")]
        public async Task<IActionResult> VerifySession(Guid sessionId)
        {
            var result = await _securitySessionService.VerifySession(sessionId);
            return Ok(result);
        }
    }
}