namespace Mde.Project.Core.Entities
{
	public class Farm
	{
		public Farm(Guid id, string name, string description, double latitude, double longitude, IEnumerable<Product> products, string imageUrl)
		{
			Id = id;
			Name = name;
			Description = description;
			Latitude = latitude;
			Longitude = longitude;
			Products = products;
			ImageUrl = imageUrl;
		}

		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string ImageUrl { get; set; }
    }
}
