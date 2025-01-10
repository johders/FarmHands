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

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
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
            IsLoading = true;

            var uid = await SecureStorage.GetAsync("userId");
            var result = await _favoriteFarmService.GetAllFavoriteFarmsByUserAsync(uid);
			var favoriteFarms = result.Data.Select(ff => new FarmViewModel(ff, _farmService, _imageConversionService));

			FavoriteFarms = new ObservableCollection<FarmViewModel>(favoriteFarms);

            IsLoading = false;
        });

		public ICommand ViewFarmDetailsCommand => new Command<FarmViewModel>(async (farmViewModel) =>
		{
			var navigationParameter = new Dictionary<string, object>()
			{
				{ nameof(UserFarmDetailsViewModel.SelectedFarm), farmViewModel }
			};

			await Shell.Current.GoToAsync(nameof(UserFarmDetailPage), true, navigationParameter);
		});

	}
}
