using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using System.Text.Json;

namespace Mde.Project.Core.Services
{
    public class MealDbService : IMealDbService
    {
        private readonly HttpClient _httpClient;

        public MealDbService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MealDb");
        }

        public async Task<ResultModel<List<Meal>>> GetMealsByIngredientAsync(string ingredient)
        {
            var result = new ResultModel<List<Meal>>();

            try
            {
                var relativeUrl = $"filter.php?i={Uri.EscapeDataString(ingredient)}";

                var response = await _httpClient.GetAsync(relativeUrl);

                if (!response.IsSuccessStatusCode)
                {
                    result.Errors.Add("Error fetching API data");
                    return result;
                }

                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<MealsList>(content);

                if (apiResponse?.Meals != null)
                {
                    result.Data = apiResponse.Meals;
                }
                else
                {
                    result.Errors.Add("No meals found for the given ingredient.");
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
            }

            return result;
        }
    }
}
