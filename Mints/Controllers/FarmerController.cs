using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExamCardPin.Models.ApiModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mints.BLayer.Repositories;
using Mints.Models.ApiModels;

namespace Mints.Controllers
{
    [Route("api/v1/farmer/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class FarmerController : ControllerBase
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly ILocationRepository _locationRepository;

        public FarmerController(IFarmerRepository farmerRepository, IAnimalRepository animalRepository, ILocationRepository locationRepository)
        {
            _farmerRepository = farmerRepository;
            _animalRepository = animalRepository;
            _locationRepository = locationRepository;
        }

        [HttpGet]
        [ProducesResponseType(201, Type = typeof(FarmerProfileRequestModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> Profile()
        {
            try
            {
                var username = User.Identity.Name;
                var farmer = await _farmerRepository.FindByNameAsync(username);
                var farmerModel = new FarmerViewModel
                {
                    Id = farmer.Id,
                    FirstName = farmer.FirstName,
                    LastName = farmer.LastName,
                    DateCreated = farmer.DateCreated,
                    Email = farmer.Email,
                    Address = farmer.Address,
                    Phone = farmer.Phone,
                    TotalAnimals = await _animalRepository.TotalAnimals(username)
                };
               
                return Ok(new FarmerProfileRequestModel { Status = "success", Message = "farmer profile successfully retrieved", Profile = farmerModel });
            }
            catch (Exception ex)
            {
                var response = new ErrorModel
                {
                    Status = "error",
                    Message = $"{ex.Message}",
                };
                return BadRequest(response);
            }
        }

        [HttpGet]
        [ProducesResponseType(201, Type = typeof(AnimalsRequestModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> Animals()
        {
            try
            {
                var username = User.Identity.Name;
                var animals = await _animalRepository.GetAnimals(username);
                var animalModel = animals.Select(a => new AnimalViewModel
                {
                    Id = a.Id,
                    Tag = a.Tag,
                    Status = a.Status == 1? true : false,
                    DateCreated = a.DateCreated,
                    Picture = a.Picture
                });

                return Ok(new AnimalsRequestModel { Status = "success", Message = "animals records successfully retrieved", Animals = animalModel });
            }
            catch (Exception ex)
            {
                var response = new ErrorModel
                {
                    Status = "error",
                    Message = $"{ex.Message}",
                };
                return BadRequest(response);
            }
        }

        [HttpGet]
        [ProducesResponseType(201, Type = typeof(AnimalRequestModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> Animal(string id)
        {
            try
            {
                var username = User.Identity.Name;
                var animal = await _animalRepository.GetAnimal(username, id);
                var animalModel = new AnimalViewModel
                {
                    Id = animal.Id,
                    Tag = animal.Tag,
                    Status = animal.Status == 1 ? true : false,
                    DateCreated = animal.DateCreated,
                    Picture = animal.Picture
                };

                return Ok(new AnimalRequestModel { Status = "success", Message = "animals records successfully retrieved", Animal = animalModel });
            }
            catch (Exception ex)
            {
                var response = new ErrorModel
                {
                    Status = "error",
                    Message = $"{ex.Message}",
                };
                return BadRequest(response);
            }
        }

        [HttpGet]
        [ProducesResponseType(201, Type = typeof(AnimalCountRequestModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> TotalAnimals()
        {
            try
            {
                var username = User.Identity.Name;
                var count = await _animalRepository.TotalAnimals(username);
                

                return Ok(new AnimalCountRequestModel { Status = "success", Message = "animals count successfully retrieved", Count = count });
            }
            catch (Exception ex)
            {
                var response = new ErrorModel
                {
                    Status = "error",
                    Message = $"{ex.Message}",
                };
                return BadRequest(response);
            }
        }

    }
}
