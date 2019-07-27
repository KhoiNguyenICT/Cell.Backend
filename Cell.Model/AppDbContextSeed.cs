﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cell.Common.Constants;
using Cell.Common.Extensions;
using Cell.Model.Entities.SecurityGroupEntity;
using Cell.Model.Entities.SecurityPermissionEntity;
using Cell.Model.Entities.SecurityUserEntity;
using Cell.Model.Entities.SettingAdvancedEntity;
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
            var groups = await InitGroup();
            var account = await InitAccount();
            var advanced = await InitAdvancedSetting();
            if (account != null && advanced != null)
            {
                var systemRole = groups.FirstOrDefault(t => t.Code == "SYSTEM.ROLE" && t.Name == "System");
                foreach (var securityGroup in groups)
                {
                    await InitPermission(securityGroup.Id, securityGroup.Name, ConfigurationKeys.SecurityGroupTableName, systemRole);
                }
                foreach (var settingAdvanced in advanced)
                {
                    await InitPermission(settingAdvanced.Id, settingAdvanced.Name,
                        ConfigurationKeys.SettingAdvancedTableName, systemRole);
                }
                await InitPermission(account.Id, account.Account, ConfigurationKeys.SecurityUserTableName, systemRole);
                await _context.SaveChangesAsync();
            }
        }

        private static string CreatePath(string jsonFile)
        {
            return "Setup/" + jsonFile;
        }

        private async Task<SecurityUser> InitAccount()
        {
            if (await _context.SecurityUsers.AnyAsync()) return null;
            var input = File.ReadAllText(CreatePath("setting-user-data.json"));
            var user = JsonConvert.DeserializeObject<SecurityUser>(input);
            _context.SecurityUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        private async Task<List<SecurityGroup>> InitGroup()
        {
            if (await _context.SecurityGroups.AnyAsync()) return null;
            var input = File.ReadAllText(CreatePath("setting-group-data.json"));
            var settingGroups = JsonConvert.DeserializeObject<List<SecurityGroup>>(input);
            _context.SecurityGroups.AddRange(settingGroups);
            await _context.SaveChangesAsync();
            return settingGroups;
        }

        private async Task<List<SettingAdvanced>> InitAdvancedSetting()
        {
            if (await _context.SettingAdvanceds.AnyAsync()) return null;
            var input = File.ReadAllText(CreatePath("setting-advanced-data.json"));
            var settingAdvanced = JsonConvert.DeserializeObject<List<SettingAdvanced>>(input);
            _context.SettingAdvanceds.AddRange(settingAdvanced);
            await _context.SaveChangesAsync();
            return settingAdvanced;
        }

        private async Task InitPermission(Guid objectId, string objectName, string authorizedType, SecurityGroup systemRole)
        {
            if (!await _context.SecurityPermissions.AnyAsync())
            {
                if (systemRole == null) return;
                var securityPermissions = new List<SecurityPermission>
                {
                    new SecurityPermission
                    {
                        AuthorizedId = systemRole.Id,
                        AuthorizedType = authorizedType,
                        ObjectId = objectId,
                        ObjectName = objectName,
                        TableName = ConfigurationKeys.SecurityGroup
                    },
                    new SecurityPermission
                    {
                        AuthorizedId = Guid.Parse("60886CC0-5566-4B48-8B2A-E9818CFCB5D8"),
                        AuthorizedType = authorizedType,
                        ObjectId = objectId,
                        ObjectName = objectName,
                        TableName = ConfigurationKeys.SecurityUser
                    }
                };
                _context.SecurityPermissions.AddRange(securityPermissions);
            }
        }
    }
}