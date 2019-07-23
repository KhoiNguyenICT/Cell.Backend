namespace Cell.Application.Api.Models.SettingReport
{
    public class SettingReportUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SettingReportSettingsModel Settings { get; set; }
    }
}