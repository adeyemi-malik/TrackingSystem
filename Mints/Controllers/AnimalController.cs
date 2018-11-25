using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mints.BLayer.Models.Animal;
using Mints.BLayer.Repositories;
using Mints.Models.ApiModels;

namespace Mints.Controllers
{
    [Route("api/v1/animal/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly ILocationRepository _locationRepository;

        public AnimalController(IAnimalRepository animalRepository, IFarmerRepository farmerRepository, ILocationRepository locationRepository)
        {
            _animalRepository = animalRepository;
            _farmerRepository = farmerRepository;
            _locationRepository = locationRepository;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(AnimalsRequestModel))]
        [ProducesResponseType(400, Type = typeof(ErrorModel))]
        public async Task<IActionResult> RegisterAnimal([FromBody] RegisterAnimalViewModel model)
        {
            try
            {
                var animal = new Animal
                {
                    Tag = model.Tag,
                    Status = model.Status == true? 1: 0,
                };

               await _animalRepository.AddAnimals(animal);                
                return CreatedAtAction(nameof(Animal), new {id = animal.Tag }, new RegisterAnimalRequestModel {
                    Status = "success",
                    Message = "animal registration successful",
                    Tag = animal.Tag
                });
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
                var animals = await _animalRepository.GetAnimals();
                var animalModel = animals.Select(a => new AnimalViewModel
                {
                    Id = a.Id,
                    Tag = a.Tag,
                    Status = a.Status == 1 ? true : false,
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
                var animal = await _animalRepository.GetAnimal(id);
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
                var count = await _animalRepository.TotalAnimals();


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