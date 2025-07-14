using Web.Models;

namespace Web.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ApiClient");
        }

        public async Task<List<City>> GetCitiesAsync()
        {
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
                    Console.WriteLine($"Ошибка API: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение при запросе: {ex.Message}");
            }

            return new List<City>(); // fallback
        }


        public async Task<List<Street>> GetStreetsAsync(int cityId)
        {
            return await _http.GetFromJsonAsync<List<Street>>($"cities/{cityId}/streets") ?? new();
        }

        public async Task<List<House>> GetHousesByCityAsync(int cityId)
        {
            return await _http.GetFromJsonAsync<List<House>>($"cities/{cityId}/houses") ?? new();
        }

        public async Task<List<House>> GetHousesByStreetAsync(int streetId)
        {
            return await _http.GetFromJsonAsync<List<House>>($"streets/{streetId}/houses") ?? new();
        }

        public async Task<List<Apartment>> GetApartmentsByHouseAsync(int houseId, float? minArea = null, float? maxArea = null)
        {
            var url = $"houses/{houseId}/apartments";

            if (minArea != null || maxArea != null)
            {
                var query = new List<string>();
                if (minArea != null) query.Add($"minArea={minArea}");
                if (maxArea != null) query.Add($"maxArea={maxArea}");
                url += "?" + string.Join("&", query);
            }

            return await _http.GetFromJsonAsync<List<Apartment>>(url) ?? new();
        }
    }


}
