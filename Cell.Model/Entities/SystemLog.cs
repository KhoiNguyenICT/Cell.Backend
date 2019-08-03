using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Model.Entities.SystemLogEntity
{
    [Table("T_SYSTEM_LOG")]
    public class SystemLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public long Id { get; set; }

        [Column("APPLICATION")]
        public string Application { get; set; }

        [Column("LOGGED")]
        public string Logged { get; set; }

        [Column("LEVEL")]
        public string Level { get; set; }

        [Column("MESSAGE")]
        public string Message { get; set; }

        [Column("LOGGER")]
        public string Logger { get; set; }

        [Column("CALL_SITE")]
        public string Callsite { get; set; }

        [Column("EXCEPTION")]
        public string Exception { get; set; }
    }
}