using System.Collections.Generic;

namespace Cell.Application.Api.Commands
{
    public class SettingTableCommand : Command
    {
        public string BasedTable { get; set; }

        public int CountFieldItems { get; set; }

        public int CountActionItems { get; set; }

        public List<SettingTableSettingsConfigurationCommand> Settings { get; set; }
    }

    public class SettingTableSettingsConfigurationCommand
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}