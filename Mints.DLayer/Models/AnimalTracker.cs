using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mints.DLayer.Models
{
    public class AnimalTracker : BaseEntity
    {
        public AnimalTracker()
        {
            Locations = new HashSet<Location>();
        }

        [Required]
        public int AnimalId { get; set; }

        [Required]
        public int TrackerId { get; set; }

        public Animal Animal { get; set; }

        public Tracker Tracker { get; set; }

        [Range(0, 1)]
        public int Status { get; set; }

        public ICollection<Location> Locations { get; set; }

    }
}
