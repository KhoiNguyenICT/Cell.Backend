using System.Collections.Generic;

namespace Cell.Model.Models.SettingField
{
    public class SettingFieldSettingsModel
    {
        public int DataSize { get; set; }
        public SettingFieldSettingFormModel Form { get; set; }
        public SettingFieldSettingViewModel View { get; set; }
    }

    public class SettingFieldSettingFormModel
    {
        public string DefaultValue { get; set; }
        public string FormElementType { get; set; }
        public string FormComboType { get; set; }
        public string DataSourceType { get; set; }
        public string DataSourceTable { get; set; }
        public string DataSourceTemplate { get; set; }
        public string DataSourceFilter { get; set; }
        public string SubmitFormat { get; set; }
        public string DisplayFormat { get; set; }
        public string SubmitText { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public string Formula { get; set; }
        public string Custom { get; set; }
        public string Api { get; set; }
    }

    public class SettingFieldSettingViewModel
    {
        public string Font { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public string Description { get; set; }
        public string BackgroundColor { get; set; }
        public string HighlightColor { get; set; }
        public string FontAlign { get; set; }
        public int FontWidth { get; set; }
        public List<string> FontStyle { get; set; }
    }
}