using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beketov_Support.Models.Entities
{
    public class Company
    {
        [Key]
        [Index]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int TaxId { get; set; }
        
        public ICollection<User> Users { get; set; }
        public Company() => Users = new List<User>();
    }
}