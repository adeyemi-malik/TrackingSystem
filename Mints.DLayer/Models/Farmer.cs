using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mints.DLayer.Models
{
    public class Farmer: BaseEntity
    {
        public Farmer()
        {
            FarmerAnimals = new HashSet<FarmerAnimal>();
        }

        [Required]
        [EmailAddress]
        [StringLength(maximumLength: 250)]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 250)]
        public string Phone { get; set; }      

        [Required]
        [StringLength(maximumLength: 250)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: 250)]
        public string LastName { get; set; }

        [Required]
        [StringLength(maximumLength: 800)]
        public string Address { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<FarmerAnimal> FarmerAnimals { get; set; }

    }
}
