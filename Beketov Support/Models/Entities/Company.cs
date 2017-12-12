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
        public string DomainName { get; set; }
        [MaxLength(15)]
        public string InternetIP { get; set; }
        
        //[Required]
        public int? FirstMessage { get; set; }
        [ForeignKey("FirstMessage")]
        public Message Message { get; set; }
        
        public ICollection<User> Users { get; set; }
        public Company() => Users = new List<User>();
    }
}