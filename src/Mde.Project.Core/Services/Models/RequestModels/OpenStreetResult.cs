using System.Text.Json.Serialization;

namespace Mde.Project.Core.Services.Models.RequestModels
{
    public class OpenStreetResult
    {
        [JsonPropertyName("lat")]
        public string Lat { get; set; }

        [JsonPropertyName("lon")]
        public string Lon { get; set; }

        [JsonPropertyName("display_name")]
        public string Name { get; set; }

        public double Latitude => double.TryParse(Lat, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var lat) ? lat : 0;
        public double Longitude => double.TryParse(Lon, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var lon) ? lon : 0;
    }
}
