using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;

namespace Mde.Project.Mobile.ViewModels
{
    [QueryProperty(nameof(SelectedFarm), nameof(SelectedFarm))]
    public class UserFarmDetailsViewModel : ObservableObject
    {
        private Farm selectedFarm;

        public Farm SelectedFarm
        {
            get { return selectedFarm; }
            set 
            {
                SetProperty(ref selectedFarm, value);
            }
        }
    }
}
