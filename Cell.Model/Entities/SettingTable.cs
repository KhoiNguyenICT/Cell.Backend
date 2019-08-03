using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Model.Entities.SettingTableEntity
{
    [Table("T_SETTING_TABLE")]
    public class SettingTable : Entity
    {
        [Column("BASED_TABLE")]
        [StringLength(200)]
        public string BasedTable { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }
    }
}