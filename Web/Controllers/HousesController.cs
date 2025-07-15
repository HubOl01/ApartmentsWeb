using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    [Route("Cities/{cityId}/Streets/{streetId}/Houses")]
    public class HousesController : Controller
    {
        private readonly ApiService _apiService;

        public HousesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet("{houseId}/Apartments")]
        public async Task<IActionResult> Apartments(int cityId, int streetId, int houseId, float? minArea,
            float? maxArea)
        {
            ViewBag.CityId = cityId;
            ViewBag.StreetId = streetId;
            ViewBag.HouseId = houseId;
            ViewBag.MinArea = minArea;
            ViewBag.MaxArea = maxArea;

            var apartments = await _apiService.GetApartmentsByHouseAsync(houseId, minArea, maxArea);
            return View(apartments);
        }
    }
}