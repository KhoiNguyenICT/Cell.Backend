using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Model.Entities.SettingActionEntity
{
    [Table("T_SETTING_ACTION")]
    public class SettingAction : Entity
    {
        [Column("CONTAINER_TYPE")]
        [StringLength(50)]
        public string ContainerType { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; set; }
    }
}