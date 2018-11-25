using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mints.DLayer.Models
{
    public class FarmerAnimal: BaseEntity
    {
        [Required]
        public int AnimalId { get; set; }

        [Required]
        public int FarmerId { get; set; }

        public Animal Animal { get; set; }

        public Farmer Farmer { get; set; }
    }
}
