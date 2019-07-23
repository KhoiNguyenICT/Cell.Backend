using System.Collections.Generic;

namespace Cell.Application.Api.Models.SettingAction
{
    public class SettingActionSettingsModel
    {
        public string Icon { get; set; }
        public string Style { get; set; }
        public string InitSource { get; set; }
        public List<SettingActionSettingStepModel> Steps { get; set; }
    }

    public class SettingActionSettingStepModel
    {
        public string StepType { get; set; }
        public string Description { get; set; }
        public List<SettingActionSettingStepParameterModel> Parameters { get; set; }
    }

    public class SettingActionSettingStepParameterModel
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