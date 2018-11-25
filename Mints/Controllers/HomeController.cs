using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mints.BLayer.Repositories;
using Mints.Models;
using Mints.Models.ApiModels;

namespace Mints.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILocationRepository _locationRepository;

        public HomeController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public async Task<IActionResult> Index()
        {
            var locations = await _locationRepository.GetLocations("malik@yahoo.com", "AnimalA", null, null, null, 0, 1000000);
            var model = locations.Select(l => new LocationViewModel
            {
                Animal = l.Animal.Tag,
                Tracker = l.Tracker.Tag,
                Latitude = l.Latitude,
                Longitude = l.Longitude,
                Date  = l.DateCreated,
                Id = l.Id
            });
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
