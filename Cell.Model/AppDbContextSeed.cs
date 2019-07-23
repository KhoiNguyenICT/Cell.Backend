using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cell.Common.Extensions;
using Cell.Model.Entities.SecurityGroupEntity;
using Cell.Model.Entities.SecurityUserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Cell.Model
{
    public class AppDbContextSeed : IWebHostInitializer
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AppDbContextSeed(
            AppDbContext context, 
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task InitAsync()
        {
            await _context.Database.MigrateAsync();
            await InitAccount();
            await InitGroup();
        }

        private static string CreatePath(string jsonFile)
        {
            return "Setup/" + jsonFile;
        }

        private async Task InitAccount()
        {
            if (!await _context.SecurityUsers.AnyAsync())
            {
                var input = File.ReadAllText(CreatePath("setting-user-data.json"));
                var user = JsonConvert.DeserializeObject<SecurityUser>(input);
                _context.SecurityUsers.Add(user);
                await _context.SaveChangesAsync();
            }
        }

        private async Task InitGroup()
        {
            if (!await _context.SecurityGroups.AnyAsync())
            {
                var input = File.ReadAllText(CreatePath("setting-group-data.json"));
                var settingGroups = JsonConvert.DeserializeObject<List<SecurityGroup>>(input);
                _context.SecurityGroups.AddRange(settingGroups);
                await _context.SaveChangesAsync();
            }
        }
    }
}