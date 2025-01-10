using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.Farmer;
using Mde.Project.Mobile.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class FarmerDashboardViewModel : ObservableObject
    {
        private readonly IOfferService _offerService;
		private readonly IFarmerService _farmerService;
		private readonly IImageConversionService _imageConversionService;
        private readonly ISecureStorageService _secureStorageService;
        private readonly IFarmService _farmService;

        public FarmerDashboardViewModel(IOfferService offerService, IFarmerService farmerService, IImageConversionService imageConversionService, ISecureStorageService secureStorageService, IFarmService farmService)
        {
            _offerService = offerService;
            _farmerService = farmerService;
            _imageConversionService = imageConversionService;
            _secureStorageService = secureStorageService;
            _farmService = farmService;
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

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
        }

        public ICommand RefreshOffersListCommand =>
			new Command(async () =>
			{
                IsLoading = true;

                var uid = await _secureStorageService.GetAsync("userId");
                var farmIdResult = await _farmerService.GetFarmIdByFarmerAsync(uid);

				if (!farmIdResult.IsSuccess)
				{
                    await Shell.Current.DisplayAlert("Oops", $"Something went wrong: {string.Join(", ", farmIdResult.Errors)}", "OK");
                    return;
				}
                var result = await _offerService.GetAllOffersByFarmIdAsync(farmIdResult.Data);

				var offerViewModels = result.Data.Select(offer => new OfferViewModel(offer, _imageConversionService, _offerService));
				Offers = new ObservableCollection<OfferViewModel>(offerViewModels);

                IsLoading = false;
            });

        public ICommand CheckIfUserProfileComplete =>
            new Command(async () =>
            {
                var uid = await SecureStorage.GetAsync("userId");
                var farmerResult = await _farmerService.GetFarmIdByFarmerAsync(uid);

                var result = await _farmService.GetByIdAsync(farmerResult.Data);

                if (result.IsSuccess)
                {
                    IsProfileComplete = result.Data.ProfileComplete;
                }

                if (!IsProfileComplete)
                {
                    await Shell.Current
                .DisplayAlert("Profile incomplete", "Please note that your farm will only be visible to consumers when your profile is complete. When you are ready, go to the settings page to complete your profile", "OK");
                }
            });

        private bool isProfileComplete;
        public bool IsProfileComplete
        {
            get => isProfileComplete;
            set => SetProperty(ref isProfileComplete, value);
        }

        public ICommand GoToInventoryManagerCommand => new Command(async () =>
		{
			await Shell.Current.GoToAsync(nameof(FarmerInventoryListPage), true);
		});

	}
}
