using System.Text.Json.Serialization;

namespace Mde.Project.Core.Services.Models
{
    public class MealsList
    {
        [JsonPropertyName("meals")]
        public List<Meal> Meals { get; set; }
    }
}
