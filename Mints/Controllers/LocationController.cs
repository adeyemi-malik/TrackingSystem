using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mints.BLayer.Models.Location;
using Mints.BLayer.Models.Tracker;
using Mints.BLayer.Repositories;
using Mints.Models.ApiModels;

namespace Mints.Controllers
{
    [Route("api/v1/location/[action]")]
    [ApiController]
    public class LocationController : ControllerBase
    {      
        private readonly IFarmerRepository _farmerRepository;
        private readonly ILocationRepository _locationRepository;

        public LocationController(IFarmerRepository farmerRepository, ILocationRepository locationRepository)
        {           
            _farmerRepository = farmerRepository;
            _locationRepository = locationRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]SaveLocationViewModel model)
        {
            var location = new Location {
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Tracker = new Tracker { Tag = model.TrackerId}
            };

            await _locationRepository.SaveLocation(location);
            return Ok();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(201, Type = typeof(LocationsRequestModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> Locations([FromQuery]FilterLocationViewModel model)
        {
            try
            {
                int page = model.Page ?? 1;
                var take = 30;
                var skip = take * (page - 1);
                var username = User.Identity.Name;
                var transactions = await _locationRepository.GetLocations(username, model.Id, model.From, model.To, model.For, skip, take);
                var locationModel = transactions.Select(t => new LocationViewModel
                {
                    Id = t.Id,
                    Longitude = t.Longitude,
                    Latitude = t.Latitude,
                    Date = t.DateCreated,
                    Animal = t.Animal.Tag,
                    Tracker = t.Tracker.Tag
                });
                return Ok(new LocationsRequestModel { Status = "success", Message = "locations successfully retrieved", Locations = locationModel });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel { Status = "error", Message = ex.Message });
            }          
        }
    }
}
