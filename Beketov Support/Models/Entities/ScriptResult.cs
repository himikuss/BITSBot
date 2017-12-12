using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beketov_Support.Models.Entities
{
    public class ScriptResult
    {
        [Key]
        [Index]
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public int NextMessage { get; set; }

        
        public int? MessageId { get; set; }
        [ForeignKey("MessageId")]
        public Message Message { get; set; }

        public ICollection<History> History { get; set; }

        public ScriptResult() => History = new List<History>();
    }
}