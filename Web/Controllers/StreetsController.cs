using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    [Route("Cities/{cityId}/Streets")]
    public class StreetsController : Controller
    {
        private readonly ApiService _apiService;

        public StreetsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet("{streetId}/Houses")]
        public async Task<IActionResult> Houses(int cityId, int streetId)
        {
            var houses = await _apiService.GetHousesByStreetAsync(streetId);
            ViewBag.CityId = cityId;
            ViewBag.StreetId = streetId;
            return View(houses);
        }
    }
}