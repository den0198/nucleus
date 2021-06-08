using System;
using System.ComponentModel.DataAnnotations;
using Models.Bases;

namespace Models.EntitiesDatabase
{
    public class UserDetailsEntity : UserBase
    {
        public Guid Id { get; set; }

        [Required]
        public string AccountId { get; set; }

        public virtual AccountEntity Account { get; set; }
        
    }
}