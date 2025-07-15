using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    [Route("Cities")]
    public class CitiesController : Controller
    {
        private readonly ApiService _apiService;

        public CitiesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet("{id}/Streets")]
        public async Task<IActionResult> Streets(int id)
        {
            var streets = await _apiService.GetStreetsAsync(id);
            ViewBag.CityId = id;
            return View(streets);
        }
    }
}