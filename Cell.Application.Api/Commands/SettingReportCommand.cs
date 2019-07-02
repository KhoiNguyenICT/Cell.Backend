using System;

namespace Cell.Application.Api.Commands
{
    public class SettingReportCommand : Command
    {
        public string Settings { get; private set; }

        public Guid TableId { get; private set; }

        public string TableIdText { get; private set; }

        public string TableName { get; private set; }
    }
}