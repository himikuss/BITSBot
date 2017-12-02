using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beketov_Support.Models.Entities
{
    public class Button
    {
        [Key]
        [Index]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string Text { get; set; }
        public string Description { get; set; }
        [Required]
        public int NextMessage { get; set; }

        [Required]
        public int? MessageId { get; set; }

        [ForeignKey("MessageId")]
        public Message Message { get; set; }
    }
}