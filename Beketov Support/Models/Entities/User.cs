﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beketov_Support.Models.Entities
{
    public class User
    {
        [Key]
        [Index]
        public int Id { get; set; }
        public int TelegramId { get; set; }
        [MaxLength(16)]
        public string Phone { get; set; }
        //[Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WebUserName { get; set; }
        public string WebPassword { get; set; }
        public string EMail { get; set; }
        public string DomainUserName { get; set; }
        public string ComputerIP { get; set; }
        public string ComputerName { get; set; }
        [Required]
        public UserRole Role { get; set; }
        
        [Required]
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public ICollection<Log> Logs { get; set; }
        public User() => Logs = new List<Log>();
    }
}