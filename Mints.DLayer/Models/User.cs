using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mints.DLayer.Models
{
    public class User: BaseEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
        }

        [Required]
        [StringLength(maximumLength: 250)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(maximumLength: 250)]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [StringLength(maximumLength: 750)]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(maximumLength: 500)]
        public string HashSalt { get; set; }

        public Farmer Farmer { get; set; }
      
        public ICollection<UserRole> UserRoles { get; set; }

    }
}
