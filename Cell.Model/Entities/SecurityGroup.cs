using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Model.Entities.SecurityGroupEntity
{
    [Table("T_SECURITY_GROUP")]
    public class SecurityGroup : TreeEntity
    {
        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("STATUS")]
        public Guid Status { get; set; }

        [NotMapped]
        public List<SecurityGroup> Children { get; set; }
    }
}