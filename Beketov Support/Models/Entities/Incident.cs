using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beketov_Support.Models.Entities
{
    public class Incident
    {
        [Key]
        [Index]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<History> History { get; set; }

        public Incident() => History = new List<History>();
    }
}