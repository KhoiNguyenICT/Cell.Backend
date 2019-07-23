using System;

namespace Cell.Application.Api.Models.SettingAdvanced
{
    public class SettingAdvancedCreateModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public Guid Parent { get; set; }
    }
}