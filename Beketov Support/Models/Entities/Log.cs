using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beketov_Support.Models.Entities
{
    public class Log
    {
        [Key]
        [Index]
        public int Id { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }
        [Required]
        public string TXT { get; set; }
        [Required]
        public LogType Type { get; set; }
        [Required]
        public LogLevel Lvl { get; set; }
        public string Source { get; set; }
        
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}