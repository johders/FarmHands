using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;

namespace Mde.Project.Mobile.ViewModels
{
    public class UserItemDetailsViewModel
    {
        public UserItemDetailsViewModel()
        {
            SelectedProduct = Seeder.SeedProducts().First();
        }
        public Product SelectedProduct { get; set; }
    }
}
