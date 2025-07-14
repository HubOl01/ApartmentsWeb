    using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    public class HousesController : Controller
    {
        private readonly ApiService _api;

        public HousesController(ApiService api) => _api = api;

        public async Task<IActionResult> IndexByCity(int cityId)
        {
            ViewBag.CityId = cityId;
            var houses = await _api.GetHousesByCityAsync(cityId);
            return View("Index", houses);
        }

        public async Task<IActionResult> IndexByStreet(int streetId)
        {
            ViewBag.StreetId = streetId;
            var houses = await _api.GetHousesByStreetAsync(streetId);
            return View("Index", houses);
        }
    }
}
