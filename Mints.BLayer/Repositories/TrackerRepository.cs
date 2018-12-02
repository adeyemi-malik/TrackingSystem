using Microsoft.EntityFrameworkCore;
using Mints.BLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BTracker = Mints.BLayer.Models.Tracker.Tracker;
using DTracker = Mints.DLayer.Models.Tracker;

namespace Mints.BLayer.Repositories
{
    public class TrackerRepository: GenericRepository<DTracker>, ITrackerRepositroy
    {
        public async Task<int> TotalTrackers()
        {
            var count = await Query().CountAsync();
            return count;
        }

        public async Task<bool> AddAnimals(BTracker tracker)
        {
            var dTracker = tracker.ToDbTracker();
            await InsertAsync(dTracker);
            tracker.Id = dTracker.Id;
            return true;
        }
    }
}
