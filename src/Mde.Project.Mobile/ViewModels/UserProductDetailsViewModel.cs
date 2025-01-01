using CommunityToolkit.Mvvm.ComponentModel;
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
		private readonly IFavoriteProductService _favoriteProductsService;
		private readonly IImageConversionService _imageConversionService;

        public UserProductDetailsViewModel(IOfferService offerService, IFavoriteProductService favoriteProductsService, IImageConversionService imageConversionService)
        {
            _offerService = offerService;
            _favoriteProductsService = favoriteProductsService;
            _imageConversionService = imageConversionService;
        }

        private ProductViewModel selectedProduct;
		public ProductViewModel SelectedProduct
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
		public ICommand CheckIfProductIsFavoriteCommand => new Command(async () =>
		{
			if (SelectedProduct != null)
			{
                var uid = await SecureStorage.GetAsync("userId");
				var result = await _favoriteProductsService.IsUserFavoritedAsync(uid, SelectedProduct.Id);
				IsFavorite = result.IsSuccess;
			}
		});

		public ICommand ToggleFavoriteCommand => new Command(async () =>
		{
            var uid = await SecureStorage.GetAsync("userId");

            if (IsFavorite)
			{
                await _favoriteProductsService.DeleteAsync(uid, SelectedProduct.Id);
            }
            else
			{
                await _favoriteProductsService.CreateAsync(uid, SelectedProduct.Id);

            }
            IsFavorite = !IsFavorite;
		});

		private async Task LoadOffersForSelectedProduct()
		{
            IsLoading = true;

            if (SelectedProduct is not null)
            {
                var result = await _offerService.GetAllOffersByProductIdAsync(SelectedProduct.Id);
                var offers = result.Data;
                var offersViewModels = offers.Select(o => new OfferViewModel(o, _imageConversionService));
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
