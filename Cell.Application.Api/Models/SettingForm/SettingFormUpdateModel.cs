using System;

namespace Cell.Application.Api.Models.SettingForm
{
    public class SettingFormUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid LayoutId { get; set; }
        public SettingFormSettingsModel Settings { get; set; }
    }
}