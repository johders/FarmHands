using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class UserPreferencesViewModel
    {
        public UserPreferencesViewModel(IUserPreferencesService userPreferencesService) 
        {
            DietaryOptions = new ObservableCollection<DietaryOption>(userPreferencesService.GetDietaryOptions());
            CuisineOptions = new ObservableCollection<CuisineOption>(userPreferencesService.GetCuisineOptions());
        }
        public ObservableCollection<DietaryOption> DietaryOptions { get; set; }
        public ObservableCollection<CuisineOption> CuisineOptions { get; set; }
        public ObservableCollection<CuisineOption> SelectedCuisines { get; set; } = new();

		public ICommand SwitchToFarmerViewCommand => new Command(() =>
		{
			App.Current.MainPage = new AppShellFarmer();
		});

        public void UpdateSelectedCuisines()
        {
            SelectedCuisines.Clear();
            foreach(var cuisine in CuisineOptions.Where(c => c.IsSelected))
            {
                SelectedCuisines.Add(cuisine);
            }
        }
	}
}
