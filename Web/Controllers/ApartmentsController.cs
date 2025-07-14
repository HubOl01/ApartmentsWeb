using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly ApiService _api;

        public ApartmentsController(ApiService api) => _api = api;

        public async Task<IActionResult> Index(int houseId, float? minArea, float? maxArea)
        {
            ViewBag.HouseId = houseId;
            ViewBag.MinArea = minArea;
            ViewBag.MaxArea = maxArea;

            var apartments = await _api.GetApartmentsByHouseAsync(houseId, minArea, maxArea);
            return View(apartments);
        }
    }
}
