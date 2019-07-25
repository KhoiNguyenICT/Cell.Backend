using System.Collections.Generic;

namespace Cell.Model.Models.SettingTable
{
    public class SettingTableCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string BasedTable { get; set; }
        public List<SettingTableSettingsModel> Settings { get; set; }
    }
}