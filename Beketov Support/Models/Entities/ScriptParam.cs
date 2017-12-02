using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beketov_Support.Models.Entities
{
    public class ScriptParam
    {
        [Key]
        [Index]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public SParamType Type { get; set; }

        [Required]
        public int? ScriptId { get; set; }
        [ForeignKey("ScriptId")]
        public Script Script { get; set; }

        public ICollection<ScriptParamValue> ScriptValues { get; set; }

        public ScriptParam()
        {
            ScriptValues = new List<ScriptParamValue>();
        }
    }
}