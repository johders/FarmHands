using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.Farmer;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	public class FarmerDashboardViewModel : ObservableObject
    {
        private readonly IOfferService _offerService;

		public FarmerDashboardViewModel(IOfferService offerService)
		{
			_offerService = offerService;
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
				var result = await _offerService.GetAllAsync();
				var offerViewModels = result.Data.Select(offer => new OfferViewModel(offer));
				Offers = new ObservableCollection<OfferViewModel>(offerViewModels);
			});

		public ICommand GoToInventoryManagerCommand => new Command(async () =>
		{
			await Shell.Current.GoToAsync(nameof(FarmerInventoryListPage), true);
		});
	}
}
