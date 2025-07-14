using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    public class StreetsController : Controller
    {
        private readonly ApiService _api;

        public StreetsController(ApiService api) => _api = api;

        public async Task<IActionResult> Index(int cityId)
        {
            ViewBag.CityId = cityId;
            var streets = await _api.GetStreetsAsync(cityId);
            return View(streets);
        }
    }
}
