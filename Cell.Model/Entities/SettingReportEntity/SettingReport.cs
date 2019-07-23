using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cell.Common.SeedWork;

namespace Cell.Model.Entities.SettingReportEntity
{
    [Table("T_SETTING_REPORT")]
    public class SettingReport : Entity
    {
        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; set; }

        [Column("TABLE_ID_TEXT")]
        public string TableIdText { get; set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; set; }
    }
}