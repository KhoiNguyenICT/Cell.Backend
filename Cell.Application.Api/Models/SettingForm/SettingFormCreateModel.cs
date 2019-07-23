using System;

namespace Cell.Application.Api.Models.SettingForm
{
    public class SettingFormCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid LayoutId { get; set; }
        public Guid TableId { get; set; }
        public string TableName { get; set; }
        public SettingFormSettingsModel Settings { get; set; }
    }
}