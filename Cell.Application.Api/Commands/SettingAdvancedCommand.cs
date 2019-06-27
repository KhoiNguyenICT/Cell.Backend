using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Commands
{
    public class SettingAdvancedCommand : Command
    {
        public int IndexLeft { get; set; }

        public int IndexRight { get; set; }

        public int IsLeaf { get; set; }

        public int NodeLevel { get; set; }

        public Guid Parent { get; set; }

        public virtual SettingAdvancedCommand ParentNode { get; set; }

        public string PathCode { get; set; }

        public string PathId { get; set; }

        public string SettingValue { get; set; }

        public virtual List<SettingAdvancedCommand> Children { get; set; }
    }
}