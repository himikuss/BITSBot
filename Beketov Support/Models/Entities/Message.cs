using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beketov_Support.Models.Entities
{
    public class Message
    {
        [Key]
        [Index]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public MessageType Type { get; set; }
        public MessageKeyboardType KType { get; set; }
        
        public int? ScriptId { get; set; }
        [ForeignKey("ScriptId")]
        public Script Scripts { get; set; }

        public ICollection<ScriptParamValue> ScriptParams { get; set; }
        public ICollection<ScriptResult> ScriptResults { get; set; }
        public ICollection<Button> Buttons { get; set; }
        public ICollection<Company> Companies { get; set; }
                
        public Message()
        {
            ScriptParams = new List<ScriptParamValue>();
            ScriptResults = new List<ScriptResult>();
            Buttons = new List<Button>();
            Companies = new List<Company>();
        }
    }
}