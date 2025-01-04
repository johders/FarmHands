using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.User;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    [QueryProperty(nameof(SelectedFarm), nameof(SelectedFarm))]
    public class UserFarmDetailsViewModel : ObservableObject
    {
        private readonly IOfferService _offerService;
        private readonly IFavoriteFarmService _favoriteFarmService;
        private readonly IImageConversionService _imageConversionService;

        public UserFarmDetailsViewModel(IOfferService offerService, IFavoriteFarmService favoriteFarmService, IImageConversionService imageConversionService)
        {
            _offerService = offerService;
            _favoriteFarmService = favoriteFarmService;
            _imageConversionService = imageConversionService;
        }

        private FarmViewModel selectedFarm;
        public FarmViewModel SelectedFarm
        {
            get { return selectedFarm; }
            set 
            {
               if(SetProperty(ref selectedFarm, value))
                {
                    _ = LoadOffersForSelectedFarmAsync();
                }
            }
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
        }

        private ObservableCollection<OfferViewModel> offers;
        public ObservableCollection<OfferViewModel> Offers
        {
            get { return offers; }
            set
            {
                SetProperty(ref offers, value);
            }
        }

        private bool isFavorite;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set 
            { 
                SetProperty(ref isFavorite, value);
            }
        }

		public ICommand CheckIfFarmIsFavoriteCommand => new Command(async () =>
		{
			if (SelectedFarm != null)
			{
                var uid = await SecureStorage.GetAsync("userId");
                var result = await _favoriteFarmService.IsUserFavoritedAsync(uid, SelectedFarm.Id);
				IsFavorite = result.IsSuccess;
			}
		});

		public ICommand ToggleFavoriteCommand => new Command(async () =>
		{
			if (IsFavorite)
			{
                var uid = await SecureStorage.GetAsync("userId");
                await _favoriteFarmService.DeleteAsync(uid, SelectedFarm.Id);
            }
			else
			{
                var uid = await SecureStorage.GetAsync("userId");
                await _favoriteFarmService.CreateAsync(uid, SelectedFarm.Id);
            }
			IsFavorite = !IsFavorite;
		});

		private async Task LoadOffersForSelectedFarmAsync()
		{
            IsLoading = true;

			if (SelectedFarm is not null)
			{
				var result = await _offerService.GetAllOffersByFarmIdAsync(SelectedFarm.Id);
				var offers = result.Data.Where(o => o.IsAvailable); ;

                var offersViewModels = offers.Select(o => new OfferViewModel(o, _imageConversionService, _offerService));

				Offers = new ObservableCollection<OfferViewModel>(offersViewModels);
			}

            IsLoading = false;
		}

		public ICommand ViewOfferDetailsCommand => new Command<OfferViewModel>(async (offer) =>
		{

			var navigationParameter = new Dictionary<string, object>()
			{
				{ nameof(UserOfferDetailsViewModel.SelectedOffer), offer }
			};

			await Shell.Current.GoToAsync(nameof(UserOfferDetailPage), true, navigationParameter);
		});
	}
}
