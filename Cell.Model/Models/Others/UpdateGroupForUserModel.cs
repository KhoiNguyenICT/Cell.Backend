using System;
using System.Collections.Generic;
using Cell.Model.Models.SecurityUser;

namespace Cell.Model.Models.Others
{
    public class UpdateGroupForUserModel
    {
        public Guid UserId { get; set; }
        public List<SettingUserSettingGroupItemModel> Departments { get; set; }
        public List<SettingUserSettingGroupItemModel> Roles { get; set; }
        public Guid DefaultDepartment { get; set; }
        public SettingUserSettingGroupItemModel DefaultDepartmentData { get; set; }
        public Guid DefaultRole { get; set; }
        public SettingUserSettingGroupItemModel DefaultRoleData { get; set; }
    }
}