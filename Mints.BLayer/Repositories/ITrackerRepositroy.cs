using System;
using System.Collections.Generic;
using System.Text;
using BTracker = Mints.BLayer.Models.Tracker.Tracker;
using DTracker = Mints.DLayer.Models.Tracker;

namespace Mints.BLayer.Repositories
{
    public interface ITrackerRepositroy : IGenericRepository<DTracker>
    {
    }
}
