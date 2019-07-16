using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Application.Api.Commands
{
    public class SettingActionCommand : Command
    {
        [StringLength(50)]
        public string ContainerType { get; set; }

        public SettingActionSettingConfigurationCommand Settings { get; set; }

        public Guid TableId { get; set; }

        public string TableName { get; set; }
    }

    public class SettingActionSettingConfigurationCommand
    {
        public string Icon { get; set; }
        public string Style { get; set; }
        public string InitSource { get; set; }
        public List<SettingActionSettingStepConfigurationCommand> Steps { get; set; }
    }

    public class SettingActionSettingStepConfigurationCommand
    {
        public string StepType { get; set; }
        public string Description { get; set; }
        public List<SettingActionSettingStepParameterConfigurationCommand> Parameters { get; set; }
    }

    public class SettingActionSettingStepParameterConfigurationCommand
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Table { get; set; }
        public string ComponentType { get; set; }
        public string Component { get; set; }
        public string AdvanceParameter { get; set; }
        public string Feature { get; set; }
        public string Link { get; set; }
        public string CustomApi { get; set; }
    }
}