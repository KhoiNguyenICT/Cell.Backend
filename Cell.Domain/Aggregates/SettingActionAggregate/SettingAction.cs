using Cell.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Domain.Aggregates.SettingActionAggregate
{
    [Table("T_SETTING_ACTION")]
    public class SettingAction : Entity, IAggregateRoot
    {
        [Column("CONTAINER_TYPE")]
        [StringLength(50)]
        public string ContainerType { get; private set; }

        [Column("SETTINGS")]
        public string Settings { get; private set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; private set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; private set; }

        public SettingAction(
            string code,
            string name,
            string description,
            string containerType,
            string settings,
            Guid tableId,
            string tableName)
        {
            Code = code;
            Name = name;
            Description = description;
            ContainerType = containerType;
            Settings = settings;
            TableId = tableId;
            TableName = tableName;
        }

        public void Update(
            string code,
            string name,
            string description,
            string containerType,
            string settings)
        {
            Code = code;
            Name = name;
            Description = description;
            ContainerType = containerType;
            Settings = settings;
        }
    }
}