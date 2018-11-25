using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mints.DLayer.Models
{
    public class UserRole : BaseEntity
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public int UserId { get; set; }

        public Role Role { get; set; }

        public User User { get; set; }
    }
}
