using System;

namespace Cell.Application.Api.Commands
{
    public class SettingActionInstanceCommand : Command
    {
        public string ContainerType { get; set; }

        public Guid ActionId { get; set; }

        public int OrdinalPosition { get; set; }

        public Guid Parent { get; set; }

        public string Settings { get; set; }

        public Guid TableId { get; set; }

        public string TableName { get; set; }
    }
}