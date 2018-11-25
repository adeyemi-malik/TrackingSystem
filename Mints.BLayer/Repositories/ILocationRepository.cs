using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLocation = Mints.BLayer.Models.Location.Location;
using DLocation = Mints.DLayer.Models.Location;

namespace Mints.BLayer.Repositories
{
    public interface ILocationRepository: IGenericRepository<DLocation>
    {
        Task SaveLocation(BLocation location);

        Task<IEnumerable<BLocation>> GetLocations(string user, string animal, DateTime? dateFrom, DateTime? dateTo, DateTime? dateFor, int skip, int take);

        Task<IQueryable<BLocation>> GetLocation(string animal, string tracker);


    }
}
