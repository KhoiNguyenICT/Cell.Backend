using System.Collections.Generic;

namespace Cell.Application.Api.Models.SettingReport
{
    public class SettingReportSettingsModel
    {
        public List<SettingReportSettingAxisItemModel> Axis { get; set; }
        public List<SettingReportSettingFieldItemModel> Fields { get; set; }
        public List<SettingReportSettingCategoryItemModel> Categories { get; set; }
        public List<SettingReportSettingValueItemModel> Values { get; set; }
        public List<SettingReportSettingSortItemModel> Sorts { get; set; }
        public int MaximumItemsShowUp { get; set; }
    }

    public class SettingReportSettingAxisItemModel
    {
        public string Name { get; set; }
        public string Side { get; set; }
        public string Format { get; set; }
    }

    public class SettingReportSettingFieldItemModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SettingReportSettingCategoryItemModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SettingReportSettingSeriesItemModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SettingReportSettingValueItemModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class SettingReportSettingSortItemModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}