namespace Mde.Project.Core.Services.Models.RequestModels
{
    public class FarmUpdateRequestModel
    {
		public string Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string ImageUrl { get; set; }
	}
}
