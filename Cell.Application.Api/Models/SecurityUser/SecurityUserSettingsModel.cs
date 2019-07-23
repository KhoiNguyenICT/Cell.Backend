using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Models.SecurityUser
{
    public class SecurityUserSettingsModel
    {
        public SettingUserSettingsInfoModel Information { get; set; }
        public List<SettingUserSettingGroupItemModel> Departments { get; set; }
        public SettingUserSettingGroupItemModel DefaultDepartmentData { get; set; }
        public List<SettingUserSettingGroupItemModel> Roles { get; set; }
        public SettingUserSettingGroupItemModel DefaultRoleData { get; set; }
    }

    public class SettingUserSettingsInfoModel
    {
        public string ProfileImage { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Address { get; set; }
    }

    public class SettingUserSettingGroupItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}