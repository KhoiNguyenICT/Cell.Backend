using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Commands
{
    public class SettingFieldCommand : Command
    {
        public bool AllowFilter { get; set; }

        public bool AllowSummary { get; set; }

        public string Caption { get; set; }

        public string DataType { get; set; }

        public int OrdinalPosition { get; set; }

        public string PlaceHolder { get; set; }

        public SettingFieldSettingsConfigurationCommand Settings { get; set; }

        public string StorageType { get; set; }

        public Guid TableId { get; set; }

        public string TableName { get; set; }
    }

    public class SettingFieldSettingsConfigurationCommand
    {
        public int DataSize { get; set; }
        public SettingFieldSettingFormConfigurationCommand Form { get; set; }
        public SettingFieldSettingViewConfigurationCommand View { get; set; }
    }

    public class SettingFieldSettingFormConfigurationCommand
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

    public class SettingFieldSettingViewConfigurationCommand
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