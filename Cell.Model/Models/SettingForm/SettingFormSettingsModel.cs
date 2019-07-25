using System;
using System.Collections.Generic;

namespace Cell.Model.Models.SettingForm
{
    public class SettingFormSettingsModel
    {
        public int LayoutTemplate { get; set; }
        public SettingConfigurationRegionModel Regions { get; set; }
    }

    public class SettingConfigurationRegionModel
    {
        public List<RegionSettingFormModel> Form { get; set; }
        public List<RegionSettingActionModel> Action { get; set; }
    }

    public class RegionSettingFormModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class RegionSettingActionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}