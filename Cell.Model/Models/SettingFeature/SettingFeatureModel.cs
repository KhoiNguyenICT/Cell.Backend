using Cell.Common.SeedWork;
using System;
using System.Collections.Generic;

namespace Cell.Model.Models.SettingFeature
{
    public class SettingFeatureModel : BaseModel
    {
        public string Icon { get; set; }

        public int IndexLeft { get; set; }

        public int IndexRight { get; set; }

        public int IsLeaf { get; set; }

        public int NodeLevel { get; set; }

        public Guid Parent { get; set; }

        public string PathCode { get; set; }

        public string PathId { get; set; }

        public SettingFeatureSettingsModel Settings { get; set; }

        public List<SettingFeatureModel> Children { get; set; }
    }
}