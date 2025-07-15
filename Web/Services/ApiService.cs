using Web.Models;

namespace Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("http://localhost:5114/api/");
        }

        public async Task<List<City>> GetCitiesAsync()
        {
            await Task.Delay(100);
            try
            {
                var response = await _http.GetAsync("cities");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<City>>();
                    return data ?? new List<City>();
                }
                else
                {
                    Console.WriteLine("Ошибка API: " + response.StatusCode);
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"API недоступен — {ex.Message}");
            }

            return new List<City>();
        }

        public async Task<List<Street>> GetStreetsAsync(int cityId)
        {
            return await _http.GetFromJsonAsync<List<Street>>($"cities/{cityId}/streets") ?? new();
        }

        public async Task<List<House>> GetHousesByStreetAsync(int streetId)
        {
            return await _http.GetFromJsonAsync<List<House>>($"streets/{streetId}/houses") ?? new();
        }

        public async Task<List<Apartment>> GetApartmentsByHouseAsync(int houseId, float? minArea = null,
            float? maxArea = null)
        {
            var query = new List<string> { $"houseId={houseId}" };
            if (minArea != null) query.Add($"minArea={minArea}");
            if (maxArea != null) query.Add($"maxArea={maxArea}");

            var url = $"apartments?" + string.Join("&", query);

            return await _http.GetFromJsonAsync<List<Apartment>>(url) ?? new();
        }
    }
}