using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Models.SettingAdvanced
{
    public class SettingAdvancedModel
    {
        public int IndexLeft { get; set; }

        public int IndexRight { get; set; }

        public int IsLeaf { get; set; }

        public int NodeLevel { get; set; }

        public Guid Parent { get; set; }

        public virtual SettingAdvancedModel ParentNode { get; set; }

        public string PathCode { get; set; }

        public string PathId { get; set; }

        public string SettingValue { get; set; }

        public virtual List<SettingAdvancedModel> Children { get; set; }
    }
}