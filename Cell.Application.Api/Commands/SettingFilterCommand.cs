using System;

namespace Cell.Application.Api.Commands
{
    public class SettingFilterCommand : Command
    {
        public string Settings { get; set; }

        public Guid TableId { get; set; }

        public string TableIdText { get; set; }

        public string TableName { get; set; }
    }
}