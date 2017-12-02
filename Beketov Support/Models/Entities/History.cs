using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beketov_Support.Models.Entities
{
    public class History
    {
        [Key]
        [Index]
        public int Id { get; set; }
        [Required]
        public MessageType Type { get; set; }
        [Required]
        public MessageDirection Direction { get; set; }
        public string Text { get; set; }

        public int ButtonId { get; set; }
        [ForeignKey("ButtonId")]
        public Button Button { get; set; }

        public int ResultId { get; set; }
        [ForeignKey("ResultId")]
        public ScriptResult Result { get; set; }

        [Required]
        public int IncidentId { get; set; }
        [ForeignKey("IncidentId")]
        public Incident Incident { get; set; }
    }
}