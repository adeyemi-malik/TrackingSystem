using System;
using System.Collections.Generic;
using System.Text;
using BTracker = Mints.BLayer.Models.Tracker.Tracker;
using DTracker = Mints.DLayer.Models.Tracker;
namespace Mints.BLayer.Helpers
{
    public static class TrackerHelper
    {
        public static BTracker ToTracker(this DTracker dataModel) => new BTracker
        {
            Id = dataModel.Id,
            Tag = dataModel.Tag,
            Status = dataModel.Status,
            DateCreated = dataModel.DateCreated
        };



        public static DTracker ToDbTracker(this BTracker tracker) => new DTracker
        {
            Id = tracker.Id,
            Tag = tracker.Tag,
            Status = tracker.Status
        };
    }
}
