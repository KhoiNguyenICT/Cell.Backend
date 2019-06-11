using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Commands
{
    public class SettingFormCommand : Command
    {
        public Guid LayoutId { get; set; }

        public SettingConfigurationCommand Settings { get; set; }

        public Guid TableId { get; set; }

        public string TableName { get; set; }
    }

    public class SettingConfigurationCommand
    {
        public int LayoutTemplate { get; set; }
        public SettingConfigurationRegionCommand Regions { get; set; }
    }

    public class SettingConfigurationRegionCommand
    {
        public List<RegionSettingForm> Form { get; set; }
        public List<RegionSettingAction> Action { get; set; }
    }

    public class RegionSettingForm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class RegionSettingAction
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}