using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cell.Common.SeedWork;

namespace Cell.Model.Entities.SettingActionInstanceEntity
{
    [Table("T_SETTING_ACTION_INSTANCE")]
    public class SettingActionInstance : Entity
    {
        [Column("CONTAINER_TYPE")]
        [StringLength(50)]
        public string ContainerType { get; set; }

        [Column("ACTION_ID")]
        public Guid ActionId { get; set; }

        [Column("ORDINAL_POSITION")]
        public int OrdinalPosition { get; set; }

        [Column("PARENT")]
        public Guid Parent { get; set; }

        [Column("PARENT_TEXT")]
        public string ParentText { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; set; }
    }
}