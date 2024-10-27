using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.User;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class UserFavoriteProductsViewModel : ObservableObject
    {
        private readonly IFavoriteProductService _favoriteProductService;

		public UserFavoriteProductsViewModel(IFavoriteProductService favoriteProductService)
		{
			_favoriteProductService = favoriteProductService;
		}

		private ObservableCollection<Product> favoriteProducts;
		public ObservableCollection<Product> FavoriteProducts
		{
			get { return favoriteProducts; }
			set 
			{ 
				SetProperty(ref favoriteProducts, value); 
			}
		}

		public ICommand RefreshFavoriteProductsListCommand => new Command(async () =>
		{
			var result = await _favoriteProductService.GetAllFavoriteProductssAsync();
			var favoriteProducts = result.Data;

			FavoriteProducts = new ObservableCollection<Product>(favoriteProducts);
		});

		public ICommand ViewProductDetailsCommand => new Command<Product>(async (product) =>
		{
			var navigationParameter = new Dictionary<string, object>()
			{
				{ nameof(UserProductDetailsViewModel.SelectedProduct), product }
			};

			await Shell.Current.GoToAsync(nameof(UserProductDetailPage), true, navigationParameter);
		});
	}
}
