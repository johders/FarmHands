using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using System.Collections.ObjectModel;

namespace Mde.Project.Mobile.ViewModels
{
    public class FarmViewModel
    {
        public FarmViewModel()
        {
            Farms = new ObservableCollection<Farm>(Seeder.SeedFarms());
        }
        public ObservableCollection<Farm> Farms { get; set; }
    }
}
