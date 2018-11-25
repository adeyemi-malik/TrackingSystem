using System;
using System.Collections.Generic;
using System.Text;
using BLocation = Mints.BLayer.Models.Location.Location;
using DLocation = Mints.DLayer.Models.Location;

namespace Mints.BLayer.Helpers
{
    public static class LocationHelper
    {
        public static BLocation ToLocation(this DLocation dataModel) => new BLocation
        {
            Id = dataModel.Id,
            Longitude = dataModel.Longitude,
            Latitude = dataModel.Latitude,
            Animal = dataModel.AnimalTracker.Animal.ToAnimal(),
            Tracker = dataModel.AnimalTracker.Tracker.ToTracker(),
            DateCreated = dataModel.DateCreated
        };



        public static DLocation ToDbLocation(this BLocation location) => new DLocation
        {
            Id = location.Id,
            Longitude = location.Longitude,
            Latitude = location.Latitude
        };
    }
}
