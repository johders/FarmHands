using Mde.Project.Core.Data;
using Mde.Project.Core.Entities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class UserPreferencesViewModel
    {
        public UserPreferencesViewModel() 
        {
            DietaryOptions = new ObservableCollection<DietaryOption>(Seeder.SeedDietaryOptions());
            CuisineOptions = new ObservableCollection<CuisineOption>(Seeder.SeedCuisineOptions());
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
