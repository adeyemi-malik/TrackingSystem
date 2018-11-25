using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mints.Models.ApiModels
{
    public class SaveLocationViewModel
    {
        [Required]
        public string Latitude { get; set; }

        [Required]
        public string Longitude { get; set; }

        [Required]
        public string TrackerId { get; set; }
    }

    public class FilterLocationViewModel
    {
        public int? Page { get; set; }

        public DateTime? For { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        [Required]
        public string Id { get; set; }
    }

    public class LocationsRequestModel : BaseModel
    {
        public LocationsRequestModel()
        {
            Locations = new List<LocationViewModel>();
        }

        public IEnumerable<LocationViewModel> Locations { get; set; }
    }

    public class LocationViewModel
    {
        public int Id { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Tracker { get; set; }

        public string Animal { get; set; }

        public DateTime Date { get; set; }
    }
}
