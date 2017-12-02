using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beketov_Support.Models.Entities
{
    public class ScriptParamValue
    {
        [Key]
        [Index]
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }

        [Required]
        public int? ScriptParamId { get; set; }
        [ForeignKey("ScriptParamId")]
        public ScriptParam ScriptParam { get; set; }

        [Required]
        public int MessageId { get; set; }
        [ForeignKey("MessageId")]
        public Message Message { get; set; }
    }
}