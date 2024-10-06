using Mde.Project.Core.Enums;

namespace Mde.Project.Core.Entities
{
	public class Product
	{
		public Product(Guid id, string name, string description, Unit unit, 
			decimal price, string imageUrl)
		{
			Id = id;
			Name = name;
			Description = description;
			Unit = unit;
			Price = price;
			ImageUrl = imageUrl;
		}

		public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
		public Unit Unit { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }


    }
}
