using Cell.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Domain.Aggregates.SettingFormAggregate
{
    [Table("T_SETTING_FORM")]
    public class SettingForm : Entity, IAggregateRoot
    {
        [Column("LAYOUT_ID")]
        public Guid LayoutId { get; private set; }

        [Column("SETTINGS")]
        public string Settings { get; private set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; private set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; private set; }

        public SettingForm(
            string name,
            string description,
            Guid layoutId,
            string settings,
            Guid tableId,
            string tableName)
        {
            Name = name;
            Description = description;
            LayoutId = layoutId;
            Settings = settings;
            TableId = tableId;
            TableName = tableName;
        }
    }
}