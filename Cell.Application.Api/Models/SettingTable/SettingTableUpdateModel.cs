namespace Cell.Application.Api.Models.SettingTable
{
    public class SettingTableUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SettingTableSettingsModel Settings { get; set; }
    }
}