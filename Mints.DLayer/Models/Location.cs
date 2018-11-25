using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mints.DLayer.Models
{
    public class Location: BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 250)]
        public string Latitude { get; set; }

        [Required]
        [StringLength(maximumLength: 250)]
        public string Longitude { get; set; }

        [Required]
        public int AnimalTrackerId { get; set; }

        public AnimalTracker AnimalTracker { get; set; }

    }
}
