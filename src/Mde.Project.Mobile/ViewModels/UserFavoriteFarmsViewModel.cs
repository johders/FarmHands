using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.User;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class UserFavoriteFarmsViewModel : ObservableObject
	{
		private readonly IFavoriteFarmService _favoriteFarmService;
		private readonly IFarmService _farmService;
		private readonly IImageConversionService _imageConversionService;

        public UserFavoriteFarmsViewModel(IFavoriteFarmService favoriteFarmService, IFarmService farmService, IImageConversionService imageConversionService)
        {
            _favoriteFarmService = favoriteFarmService;
            _farmService = farmService;
            _imageConversionService = imageConversionService;
        }

        private ObservableCollection<FarmViewModel> favoriteFarms;
		public ObservableCollection<FarmViewModel> FavoriteFarms
		{
			get { return favoriteFarms; }
			set
			{
				SetProperty(ref favoriteFarms, value);
			}
		}

		public ICommand RefreshFavoriteFarmsListCommand => new Command(async () =>
		{
			var uid = await SecureStorage.GetAsync("userId");
            var result = await _favoriteFarmService.GetAllFavoriteFarmsByUserAsync(uid);
			var favoriteFarms = result.Data.Select(ff => new FarmViewModel(ff, _farmService, _imageConversionService));

			FavoriteFarms = new ObservableCollection<FarmViewModel>(favoriteFarms);
		});

		public ICommand ViewFarmDetailsCommand => new Command<FarmViewModel>(async (farmViewModel) =>
		{
			var result = await _farmService.GetByIdAsync(farmViewModel.Id);
			var farm = result.Data;

			if (!result.IsSuccess)
			{
				Shell.Current.DisplayAlert("Oops", $"{String.Join(", ", result.Errors)}", "OK");
				return;
			}

			var navigationParameter = new Dictionary<string, object>()
			{
				{ nameof(UserFarmDetailsViewModel.SelectedFarm), farm }
			};

			await Shell.Current.GoToAsync(nameof(UserFarmDetailPage), true, navigationParameter);
		});

	}
}
