using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;
using System.Collections.ObjectModel;

namespace Mde.Project.Mobile.ViewModels
{
	public class ProductViewModel
	{
		public ProductViewModel()
		{
			//Products = Seeder.SeedProducts();
		}

		public ObservableCollection<Product> Products { get; set; }
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Unit Unit { get; set; }
		public decimal Price { get; set; }
		public string ImageUrl { get; set; }


	}
}
