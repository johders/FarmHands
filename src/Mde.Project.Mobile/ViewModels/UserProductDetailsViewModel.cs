using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
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
        private readonly IMealDbService _mealDbService;

        public UserProductDetailsViewModel(IOfferService offerService, IFavoriteProductService favoriteProductsService, IImageConversionService imageConversionService, IMealDbService mealDbService)
        {
            _offerService = offerService;
            _favoriteProductsService = favoriteProductsService;
            _imageConversionService = imageConversionService;
            _mealDbService = mealDbService;
        }

        private ProductViewModel selectedProduct;
		public ProductViewModel SelectedProduct
        {
			get { return selectedProduct; }
			set
			{
				if(SetProperty(ref selectedProduct, value))
                {
                    _ = LoadDataForSelectedProduct();
                }
			}
		}

        private ObservableCollection<Meal> recipes;
        public ObservableCollection<Meal> Recipes
        {
            get => recipes;
            set => SetProperty(ref recipes, value);
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
            if (SelectedProduct is not null)
            {
                var result = await _offerService.GetAllOffersByProductIdAsync(SelectedProduct.Id);
                var offers = result.Data.Where(o => o.IsAvailable);
                var offersViewModels = offers.Select(o => new OfferViewModel(o, _imageConversionService));
                Offers = new ObservableCollection<OfferViewModel>(offersViewModels);
            }
        }

        private async Task LoadRecipesForSelectedProduct()
        {
            if (SelectedProduct is not null)
            {
                var result = await _mealDbService.GetMealsByIngredientAsync(SelectedProduct.Name);
                if (result.Data != null && result.Data.Count > 0)
                {
                    Recipes = new ObservableCollection<Meal>(result.Data);
                }
                else
                {
                    Recipes = new ObservableCollection<Meal>();
                }
            }
        }

        private async Task LoadDataForSelectedProduct()
        {
            IsLoading = true;

            if (SelectedProduct is not null)
            {
                var offersTask = LoadOffersForSelectedProduct();
                var recipesTask = LoadRecipesForSelectedProduct();

                await Task.WhenAll(offersTask, recipesTask);
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

        public ICommand OpenRecipeCommand => new Command<Meal>(async (meal) =>
        {
            if (meal != null && !string.IsNullOrEmpty(meal.RecipePageUrl))
            {
                try
                {
                    await Browser.OpenAsync(meal.RecipePageUrl, BrowserLaunchMode.SystemPreferred);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to open URL: {ex.Message}");
                }
            }
        });
    }
}
