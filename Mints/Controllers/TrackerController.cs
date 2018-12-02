using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mints.BLayer.Repositories;

namespace Mints.Controllers
{
    [Route("api/v1/tracker/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TrackerController : ControllerBase
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly ILocationRepository _locationRepository;

        public TrackerController(IFarmerRepository farmerRepository, IAnimalRepository animalRepository, ILocationRepository locationRepository)
        {
            _farmerRepository = farmerRepository;
            _animalRepository = animalRepository;
            _locationRepository = locationRepository;
        }
    }
}