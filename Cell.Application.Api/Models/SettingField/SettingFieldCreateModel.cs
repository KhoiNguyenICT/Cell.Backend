using System;

namespace Cell.Application.Api.Models.SettingField
{
    public class SettingFieldCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int AllowFilter { get; set; }
        public int AllowSummary { get; set; }
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