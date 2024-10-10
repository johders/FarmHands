using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using System.Collections.ObjectModel;

namespace Mde.Project.Mobile.ViewModels
{
    public class HomePageViewModel
    {
		public ObservableCollection<Farm> Farms { get; set; }
		public ObservableCollection<Product> Products { get; set; }

        public HomePageViewModel()
        {
			Farms = new ObservableCollection<Farm>(Seeder.SeedFarms());
            Products = new ObservableCollection<Product>(Seeder.SeedProducts());
		}
    }
}
