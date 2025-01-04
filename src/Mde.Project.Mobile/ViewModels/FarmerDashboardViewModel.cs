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

        public FarmerDashboardViewModel(IOfferService offerService, IFarmerService farmerService, IImageConversionService imageConversionService, ISecureStorageService secureStorageService)
        {
            _offerService = offerService;
            _farmerService = farmerService;
            _imageConversionService = imageConversionService;
            _secureStorageService = secureStorageService;
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

		public ICommand GoToInventoryManagerCommand => new Command(async () =>
		{
			await Shell.Current.GoToAsync(nameof(FarmerInventoryListPage), true);
		});


	}
}
