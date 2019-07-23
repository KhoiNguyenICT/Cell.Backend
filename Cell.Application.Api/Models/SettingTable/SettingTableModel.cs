using System.Collections.Generic;
using Cell.Common.SeedWork;

namespace Cell.Application.Api.Models.SettingTable
{
    public class SettingTableModel : BaseModel
    {
        public string BasedTable { get; set; }

        public int CountFieldItems { get; set; }

        public int CountActionItems { get; set; }

        public List<SettingTableSettingsModel> Settings { get; set; }
    }
}