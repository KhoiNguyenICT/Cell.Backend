using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Models.SettingFeature
{
    public class SettingFeatureSettingsModel
    {
        public string MenuType { get; set; }
        public List<SettingFeatureSettingsInitParamModel> InitParams { get; set; }
        public ContainerSettingsModel ContainerSettings { get; set; }
    }

    public class ContainerSettingsModel
    {
        public string Table { get; set; }
        public string ComponentType { get; set; }
        public ComponentItemModel ComponentItem { get; set; }
    }

    public class ComponentItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SettingFeatureSettingsInitParamModel> InitParams { get; set; }
    }

    public class SettingFeatureSettingsInitParamModel
    {
        public string Name { get; set; }
        public string Param { get; set; }
        public string Source { get; set; }
        public string DataType { get; set; }
        public string DefaultValue { get; set; }
    }
}