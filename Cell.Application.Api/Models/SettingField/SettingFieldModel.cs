using System;

namespace Cell.Application.Api.Models.SettingField
{
    public class SettingFieldModel
    {
        public bool AllowFilter { get; set; }

        public bool AllowSummary { get; set; }

        public string Caption { get; set; }

        public string DataType { get; set; }

        public int OrdinalPosition { get; set; }

        public string PlaceHolder { get; set; }

        public SettingFieldSettingsModel Settings { get; set; }

        public string StorageType { get; set; }

        public Guid TableId { get; set; }

        public string TableName { get; set; }
    }
}