using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService _api;

        public HomeController(ApiService api) => _api = api;

        public async Task<IActionResult> Index()
        {
            var cities = await _api.GetCitiesAsync();
            return View(cities);
        }
    }
}