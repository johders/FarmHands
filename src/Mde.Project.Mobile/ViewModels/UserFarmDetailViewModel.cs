using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;

namespace Mde.Project.Mobile.ViewModels
{
    public class UserFarmDetailViewModel
    {

        public UserFarmDetailViewModel()
        {
            FarmSelected = Seeder.SeedFarms().First();
        }

        public Farm FarmSelected { get; set; }


    }
}
