using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mints.DLayer.Models
{
    public class Role: BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        [Required]
        [StringLength(maximumLength: 250)]
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
