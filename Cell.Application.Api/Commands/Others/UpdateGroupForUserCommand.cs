using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Commands.Others
{
    public class UpdateGroupForUserCommand
    {
        public Guid UserId { get; set; }
        public List<SettingUserSettingGroupItemCommand> Departments { get; set; }
        public List<SettingUserSettingGroupItemCommand> Roles { get; set; }
        public Guid DefaultDepartment { get; set; }
        public SettingUserSettingGroupItemCommand DefaultDepartmentData { get; set; }
        public Guid DefaultRole { get; set; }
        public SettingUserSettingGroupItemCommand DefaultRoleData { get; set; }
    }
}