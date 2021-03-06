﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mints.DLayer.Models
{
    public class Tracker: BaseEntity
    {
        public Tracker()
        {
            AnimalTrackers = new HashSet<AnimalTracker>();
        }

        [Required]
        [StringLength(maximumLength: 250)]
        public string Tag { get; set; }

        [Range(0, 1)]
        public int Status { get; set; }

        public ICollection<AnimalTracker> AnimalTrackers { get; set; }

    }
}
