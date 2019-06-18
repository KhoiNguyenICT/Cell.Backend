using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Application.Api.Commands
{
    public class SettingFeatureCommand : Command
    {
        public string Icon { get; set; }

        public int IndexLeft { get; set; }

        public int IndexRight { get; set; }

        public int IsLeaf { get; set; }

        public int NodeLevel { get; set; }

        public Guid Parent { get; set; }

        public string PathCode { get; set; }

        public string PathId { get; set; }

        public string Settings { get; set; }

        [NotMapped]
        public List<SettingFeatureCommand> Children { get; set; }
    }
}