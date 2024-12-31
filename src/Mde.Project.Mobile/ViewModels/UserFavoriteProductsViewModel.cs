using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.User;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class UserFavoriteProductsViewModel : ObservableObject
    {
        private readonly IFavoriteProductService _favoriteProductService;
		private readonly IProductService _productService;
		private readonly IImageConversionService _imageConversionService;

        public UserFavoriteProductsViewModel(IFavoriteProductService favoriteProductService, IProductService productService, IImageConversionService imageConversionService)
        {
            _favoriteProductService = favoriteProductService;
            _productService = productService;
            _imageConversionService = imageConversionService;
        }

        private ObservableCollection<ProductViewModel> favoriteProducts;
		public ObservableCollection<ProductViewModel> FavoriteProducts
		{
			get { return favoriteProducts; }
			set 
			{ 
				SetProperty(ref favoriteProducts, value); 
			}
		}

		public ICommand RefreshFavoriteProductsListCommand => new Command(async () =>
		{
            var uid = await SecureStorage.GetAsync("userId");
            var result = await _favoriteProductService.GetAllFavoriteProductsByUserAsync(uid);

			var favoriteProducts = result.Data.Select(fp => new ProductViewModel(fp, _productService, _imageConversionService)).ToList();

            await Task.WhenAll(favoriteProducts.Select(vm => vm.LoadOfferCountAsync()));

            FavoriteProducts = new ObservableCollection<ProductViewModel>(favoriteProducts);
		});

		public ICommand ViewProductDetailsCommand => new Command<ProductViewModel>(async (productViewModel) =>
		{
			var navigationParameter = new Dictionary<string, object>()
			{
				{ nameof(UserProductDetailsViewModel.SelectedProduct), productViewModel }
			};

			await Shell.Current.GoToAsync(nameof(UserProductDetailPage), true, navigationParameter);
		});
	}
}
