using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Services;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.Farmer;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	public class FarmerDashboardViewModel : ObservableObject
    {
        private readonly IOfferService _offerService;
		private readonly IFarmerService _farmerService;

        public FarmerDashboardViewModel(IOfferService offerService, IFarmerService farmerService)
        {
            _offerService = offerService;
            _farmerService = farmerService;
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

		private bool isAvailable;
		public bool IsAvailable
		{
			get { return isAvailable; }
			set { SetProperty(ref isAvailable, value); }
		}

		public ICommand RefreshOffersListCommand =>
			new Command(async () =>
			{
				var uid = await SecureStorage.GetAsync("userId");
				var farmIdResult = await _farmerService.GetFarmIdByFarmerAsync(uid);

				if (!farmIdResult.IsSuccess)
				{
                    await Shell.Current.DisplayAlert("Oops", $"Something went wrong: {string.Join(", ", farmIdResult.Errors)}", "OK");
                    return;
				}
                var result = await _offerService.GetAllOffersByFarmIdAsync(farmIdResult.Data);

				var offerViewModels = result.Data.Select(offer => new OfferViewModel(offer));
				Offers = new ObservableCollection<OfferViewModel>(offerViewModels);
			});

		public ICommand GoToInventoryManagerCommand => new Command(async () =>
		{
			await Shell.Current.GoToAsync(nameof(FarmerInventoryListPage), true);
		});
	}
}
