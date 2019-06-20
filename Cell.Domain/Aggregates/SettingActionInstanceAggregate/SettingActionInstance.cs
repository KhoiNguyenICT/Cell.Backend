using Cell.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Domain.Aggregates.SettingActionInstanceAggregate
{
    [Table("T_SETTING_ACTION_INSTANCE")]
    public class SettingActionInstance : Entity, IAggregateRoot
    {
        [Column("CONTAINER_TYPE")]
        [StringLength(50)]
        public string ContainerType { get; private set; }

        [Column("ACTION_ID")]
        public Guid ActionId { get; private set; }

        [Column("ORDINAL_POSITION")]
        public int OrdinalPosition { get; private set; }

        [Column("PARENT")]
        public Guid Parent { get; private set; }

        [Column("PARENT_TEXT")]
        public string ParentText { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; private set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; private set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; private set; }

        public SettingActionInstance() { }

        public SettingActionInstance(
            string name,
            string description,
            string containerType,
            Guid actionId,
            int ordinalPosition,
            Guid parent,
            string parentText,
            string settings,
            Guid tableId,
            string tableName)
        {
            Name = name;
            Description = description;
            ContainerType = containerType;
            ActionId = actionId;
            OrdinalPosition = ordinalPosition;
            Parent = parent;
            ParentText = parentText;
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