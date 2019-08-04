using Cell.Common.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Model.Entities.SettingAdvancedEntity
{
    [Table("T_SETTING_ADVANCED")]
    public class SettingAdvanced : TreeEntity
    {
        [Column("SETTING_VALUE")]
        public string SettingValue { get; set; }

        [NotMapped]
        public List<SettingAdvanced> Children { get; set; }
    }
}