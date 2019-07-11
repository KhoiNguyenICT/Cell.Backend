using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Commands
{
    public class SettingUserCommand : Command
    {
        public string Account { get; set; }

        public Guid DefaultDepartment { get; set; }

        public Guid DefaultRole { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public SettingUserSettingsCommand Settings { get; set; }

        public Guid Status { get; set; }
    }

    public class SettingUserSettingsCommand
    {
        public SettingUserSettingsInfoCommand Information { get; set; }
        public List<SettingUserSettingGroupItemCommand> Departments { get; set; }
        public SettingUserSettingGroupItemCommand DefaultDepartmentData { get; set; }
        public List<SettingUserSettingGroupItemCommand> Roles { get; set; }
        public SettingUserSettingGroupItemCommand DefaultRoleData { get; set; }
    }

    public class SettingUserSettingsInfoCommand
    {
        public string ProfileImage { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
    }

    public class SettingUserSettingGroupItemCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}