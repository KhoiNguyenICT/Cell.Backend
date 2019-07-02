using Cell.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Domain.Aggregates.SettingFilterAggregate
{
    [Table("T_SETTING_FILTER")]
    public class SettingFilter : Entity, IAggregateRoot
    {
        [Column("SETTINGS")]
        public string Settings { get; private set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; private set; }

        [Column("TABLE_ID_TEXT")]
        public string TableIdText { get; private set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; private set; }

        public SettingFilter(
            string name,
            string description,
            string settings,
            Guid tableId,
            string tableName)
        {
            Name = name;
            Description = description;
            Settings = settings;
            TableId = tableId;
            TableName = tableName;
        }

        public void Update(
            string name,
            string description,
            string settings)
        {
            Name = name;
            Description = description;
            Settings = settings;
        }
    }
}