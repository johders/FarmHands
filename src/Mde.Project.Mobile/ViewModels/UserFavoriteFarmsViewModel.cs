using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class UserFavoriteFarmsViewModel : ObservableObject
    {
		private readonly IFavoriteFarmService _favoriteFarmService;
		private readonly IFarmService _farmService;

        public UserFavoriteFarmsViewModel(IFavoriteFarmService favoriteFarmService)
        {
            _favoriteFarmService = favoriteFarmService;
        }

        private ObservableCollection<Farm> favoriteFarms;

		public ObservableCollection<Farm> FavoriteFarms
        {
			get { return favoriteFarms; }
			set 
			{ 
				SetProperty(ref favoriteFarms, value); 
			}
		}

        public ICommand RefreshFavoriteFarmsListCommand => new Command(async () =>
        {
            var result = await _favoriteFarmService.GetAllFavoriteFarmsAsync();
            var favoriteFarms = result.Data;

            FavoriteFarms = new ObservableCollection<Farm>(favoriteFarms);
        });

    }
}
