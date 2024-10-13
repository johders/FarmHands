using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using System.Collections.ObjectModel;

namespace Mde.Project.Mobile.ViewModels
{
    public class FarmDashboardViewModel
    {
        public FarmDashboardViewModel()
        {
            Offers = new ObservableCollection<Offer>(Seeder.SeedFarmOffers());
        }
        public ObservableCollection<Offer> Offers { get; set; }
    }
}
