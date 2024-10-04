namespace Mde.Project.Core.Entities
{
	public class Farm
	{
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
