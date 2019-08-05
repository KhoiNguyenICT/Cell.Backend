using System;

namespace Cell.Model.Models.SettingTable
{
    public class SettingTableUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SettingTableSettingsModel Settings { get; set; }
    }
}