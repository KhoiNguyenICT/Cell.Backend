using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Commands
{
    public class SettingGroupCommand : Command
    {
        public int IndexLeft { get; set; }

        public int IndexRight { get; set; }

        public int IsLeaf { get; set; }

        public int NodeLevel { get; set; }

        public Guid Parent { get; set; }

        public string PathCode { get; set; }

        public string PathId { get; set; }

        public string Settings { get; set; }

        public Guid Status { get; set; }

        public List<SettingGroupCommand> Children { get; set; }
    }
}