using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beketov_Support.Models.Entities
{
    public class Script
    {
        [Key]
        [Index]
        public int Id { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public SReturnType ReturnType { get; set; }
        public string Description { get; set; }

        public ICollection<Message> Messages { get; set; }
        public ICollection<ScriptParam> ScriptParam { get; set; }

        public Script()
        {
            Messages = new List<Message>();
            ScriptParam = new List<ScriptParam>();
        }
    }
}