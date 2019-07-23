namespace Cell.Application.Api.Models.SettingAction
{
    public class SettingActionUpdateModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContainerType { get; set; }
        public SettingActionSettingsModel Settings { get; set; }
    }
}