using Cell.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Domain.Aggregates.SettingViewAggregate
{
    [Table("T_SETTING_VIEW")]
    public class SettingView : Entity, IAggregateRoot
    {
        [Column("TABLE_ID")]
        public Guid TableId { get; private set; }

        [StringLength(200)]
        [Column("TABLE_NAME")]
        public string TableName { get; private set; }

        [Column("SETTINGS")]
        public string Settings { get; private set; }

        public SettingView(
            string code,
            string name,
            string description, 
            Guid tableId,
            string tableName,
            string settings)
        {
            Code = code;
            Name = name;
            Description = description;
            TableId = tableId;
            TableName = tableName;
            Settings = settings;
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