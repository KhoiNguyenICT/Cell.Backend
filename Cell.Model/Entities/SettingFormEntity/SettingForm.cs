using Cell.Common.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Model.Entities.SettingFormEntity
{
    [Table("T_SETTING_FORM")]
    public class SettingForm : Entity
    {
        [Column("LAYOUT_ID")]
        public Guid LayoutId { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; set; }
    }
}