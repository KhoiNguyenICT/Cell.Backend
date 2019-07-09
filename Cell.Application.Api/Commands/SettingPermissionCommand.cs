using System;

namespace Cell.Application.Api.Commands
{
    public class SettingPermissionCommand : Command
    {
        public Guid AuthorizedId { get; set; }

        public string AuthorizedType { get; set; }

        public Guid ObjectId { get; set; }

        public string ObjectName { get; set; }

        public string Settings { get; set; }

        public string TableName { get; set; }
    }
}