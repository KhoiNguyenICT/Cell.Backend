using Cell.Common.SeedWork;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Model.Entities.SettingFeatureEntity
{
    [Table("T_SETTING_FEATURE")]
    public class SettingFeature : Entity
    {
        [Column("ICON")]
        [StringLength(50)]
        public string Icon { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [NotMapped]
        public List<SettingFeature> Children { get; set; }
    }
}