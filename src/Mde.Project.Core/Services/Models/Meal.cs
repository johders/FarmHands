using System.Text.Json.Serialization;

namespace Mde.Project.Core.Services.Models
{
    public class Meal
    {
        [JsonPropertyName("strMeal")]
        public string Name { get; set; }

        [JsonPropertyName("strMealThumb")]

        public string ImageString { get; set; }

        [JsonPropertyName("idMeal")]
        public string Id { get; set; }

        public string RecipePageUrl => $"https://www.themealdb.com/meal/{Id}";
    }
}
