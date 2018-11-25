using Mints.DLayer.Models.Base;
using BAnimal = Mints.BLayer.Models.Animal.Animal;
using BTracker = Mints.BLayer.Models.Tracker.Tracker;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mints.BLayer.Models.Location
{
    public class Location: IEntity
    {
        public int Id { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public BAnimal Animal { get; set; }

        public BTracker Tracker { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
