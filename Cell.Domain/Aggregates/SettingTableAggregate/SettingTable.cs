using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SettingTableAggregate
{
    [Table("T_SETTING_TABLE")]
    public class SettingTable : Entity, IAggregateRoot
    {
        [Column("BASED_TABLE")]
        [StringLength(200)]
        public string BasedTable { get; private set; }

        [Column("SETTINGS")]
        public string Settings { get; private set; }

        public SettingTable(
            string name,
            string description,
            string code,
            string basedTable,
            string settings)
        {
            Name = name;
            Description = description;
            Code = code;
            BasedTable = basedTable;
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