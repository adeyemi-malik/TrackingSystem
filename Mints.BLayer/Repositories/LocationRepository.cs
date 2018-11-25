using Microsoft.EntityFrameworkCore;
using Mints.BLayer.Exceptions;
using Mints.BLayer.Extensions;
using Mints.BLayer.Helpers;
using Mints.DLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLocation = Mints.BLayer.Models.Location.Location;
using DLocation = Mints.DLayer.Models.Location;

namespace Mints.BLayer.Repositories
{
    public class LocationRepository: GenericRepository<DLocation>, ILocationRepository
    {
        public LocationRepository(Context dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task SaveLocation(BLocation location)
        {
            var animalTracker = await _dbContext.AnimalTrackers
                .Include(a => a.Tracker)
                .Include(a => a.Animal)
                .Where(a => a.Tracker.Tag.Equals(location.Tracker.Tag, StringComparison.OrdinalIgnoreCase) && a.Status == 1)
                .FirstOrDefaultAsync();

            var entity = new DLocation
            {
                AnimalTracker = animalTracker,
                Longitude = location.Longitude,
                Latitude = location.Latitude
            };
            await InsertAsync(entity);
            location.Id = entity.Id;
            location.Animal = animalTracker.Animal.ToAnimal();
            location.Tracker = animalTracker.Tracker.ToTracker();
        }

        public async Task<IEnumerable<BLocation>> GetLocations(string user, string animal, DateTime? dateFrom, DateTime? dateTo, DateTime? dateFor, int skip, int take)
        {
            var farmer = await _dbContext.Farmers
                .Include(u => u.User)
                .SingleOrDefaultAsync(m => m.User.Email.Equals(user, StringComparison.OrdinalIgnoreCase));
            if (farmer == null)
            {
                throw new UserReferenceNotFoundException("The user reference does not exist for a valid user");
            }
            var locations = _dbContext.FarmerAnimals
                .Include(f => f.Animal)
                .ThenInclude(a => a.AnimalTrackers)
                .ThenInclude(a => a.Locations)
                .Include(f => f.Animal)
                .ThenInclude(a => a.AnimalTrackers)
                .ThenInclude(a => a.Tracker)
                .SingleOrDefault(a => a.Animal.Tag.Equals(animal, StringComparison.OrdinalIgnoreCase) && a.FarmerId == farmer.Id)
                .Animal
                .AnimalTrackers
                .SelectMany(a => a.Locations)
                .FilterByDateFrom(dateFrom)
                .FilterByDateTo(dateTo)
                .FilterBySpecificDate(dateFor)               
                .OrderByDescending(t => t.DateCreated)
                .Skip(skip)
                .Take(take)
                .Select(l => l.ToLocation());
            return locations;

            /*var locations = _dbContext.AnimalTrackers
               .Where(a => a.Animal.Tag.Equals(animal, StringComparison.OrdinalIgnoreCase))
               .SelectMany(a => a.Locations)
               .FilterByDateFrom(dateFrom)
               .FilterByDateTo(dateTo)
               .FilterBySpecificDate(dateFor)
               .Include(l => l.AnimalTracker)
               .ThenInclude(a => a.Tracker)
               .Include(l => l.AnimalTracker)
               .ThenInclude(a => a.Animal)
               .OrderByDescending(t => t.DateCreated)
               .Skip(skip)
               .Take(take)
               .Select(l => l.ToLocation());  */          
        }

        public Task<IQueryable<BLocation>> GetLocation(string animal, string tracker)
        {
            var locations = _dbContext.AnimalTrackers
               .Include(a => a.Tracker)
               .Include(a => a.Animal)
               .Where(a => a.Animal.Tag.Equals(animal, StringComparison.OrdinalIgnoreCase) && a.Tracker.Tag.Equals(tracker, StringComparison.OrdinalIgnoreCase))
               .SelectMany(a => a.Locations).Select(l => l.ToLocation());

            return Task.FromResult(locations);
        }
    }
}
