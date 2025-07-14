using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class CitiesController : Controller
    {
        private readonly ApiService _api;

        public CitiesController(ApiService api) => _api = api;

        public async Task<IActionResult> Index()
        {
            var cities = await _api.GetCitiesAsync();
            Console.WriteLine("Cities loaded: " + cities?.Count);
            return View(cities);
        }

        //// GET: Cities/Details/5
        //public async Task<IActionResult> Details(int id)
        //{
        //    var streets = await _api.GetStreetsByCityAsync(id);
        //    ViewBag.CityId = id;
        //    return View(streets);
        //}
    }
}
