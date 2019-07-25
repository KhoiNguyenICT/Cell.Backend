using System;
using Cell.Common.SeedWork;

namespace Cell.Model.Models.SettingActionInstance
{
    public class SettingActionInstanceModel: BaseModel
    {
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