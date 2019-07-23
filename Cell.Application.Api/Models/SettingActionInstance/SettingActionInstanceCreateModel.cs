using System;
using System.ComponentModel;

namespace Cell.Application.Api.Models.SettingActionInstance
{
    public class SettingActionInstanceCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContainerType { get; set; }
        public Guid ActionId { get; set; }
        public int OrdinalPosition { get; set; }
        public Guid Parent { get; set; }
        public string ParentText { get; set; }
        public string Settings { get; set; }
        public Guid TableId { get; set; }
        public string TableName { get; set; }
    }
}