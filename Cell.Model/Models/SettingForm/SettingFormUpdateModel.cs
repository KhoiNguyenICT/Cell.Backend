using System;

namespace Cell.Model.Models.SettingForm
{
    public class SettingFormUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid LayoutId { get; set; }
        public SettingFormSettingsModel Settings { get; set; }
    }
}