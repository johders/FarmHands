using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.User;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	[QueryProperty(nameof(SelectedProduct), nameof(SelectedProduct))]
    public class UserProductDetailsViewModel : ObservableObject
    {
		private readonly IOfferService _offerService;

        public UserProductDetailsViewModel(IOfferService offerService)
        {
            _offerService = offerService;
        }

        private Product selectedProduct;

		public Product SelectedProduct
        {
			get { return selectedProduct; }
			set
			{
				if(SetProperty(ref selectedProduct, value))
                {
                    _ = LoadOffersForSelectedProduct();
                }
			}
		}

        private ObservableCollection<Offer> offers;

        public ObservableCollection<Offer> Offers
        {
            get { return offers; }
            set
            {
                SetProperty(ref offers, value);
            }
        }

        private async Task LoadOffersForSelectedProduct()
		{
            if(SelectedProduct is not null)
            {
                var result = await _offerService.GetAllOffersByProductIdAsync(SelectedProduct.Id);
                var offers = result.Data;
                Offers = new ObservableCollection<Offer>(offers);
            }
		}

		public ICommand ViewOfferDetailsCommand => new Command<Offer>(async (offer) =>
		{

			var navigationParameter = new Dictionary<string, object>()
			{
				{ nameof(UserOfferDetailsViewModel.SelectedOffer), offer }
			};

			await Shell.Current.GoToAsync(nameof(UserOfferDetailPage), true, navigationParameter);
		});
	}
}
