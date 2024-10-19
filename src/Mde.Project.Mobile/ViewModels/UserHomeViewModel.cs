using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.User;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class UserHomeViewModel : ObservableObject
    {
		private readonly IFarmService _farmService;
        private readonly IProductService _productService;

        public UserHomeViewModel(IFarmService farmService, IProductService productService)
        {
            _farmService = farmService;
            _productService = productService;
        }

        private ObservableCollection<Farm> farms;

		public ObservableCollection<Farm> Farms
		{
			get { return farms; }
			set 
			{ 
				SetProperty(ref farms, value);
			}
		}

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                SetProperty(ref products, value);
            }
        }

        public ICommand RefreshFarmListCommand =>
			new Command(async () =>
			{
				var result = await _farmService.GetAllAsync();
				var farms = result.Data;
				Farms = new ObservableCollection<Farm>(farms);
			});

        public ICommand RefreshProductListCommand =>
            new Command(async () =>
            {
                var result = await _productService.GetAllAsync();
                var products = result.Data;
                Products = new ObservableCollection<Product>(products);
            });

        public ICommand ViewFarmDetailsCommand =>
            new Command<Farm>(async (farm) =>
            {
                var navigationParameter = new Dictionary<string, object>()
                {
                    { nameof(UserFarmDetailsViewModel.SelectedFarm), farm }
                };

                await Shell.Current.GoToAsync(nameof(UserFarmDetailPage), true, navigationParameter);

            });

        public ICommand ViewProductDetailsCommand =>
            new Command<Product>(async (product) =>
            {
                var navigationParameter = new Dictionary<string, object>()
                {
                    { nameof(UserProductDetailsViewModel.SelectedProduct), product }
                };

                await Shell.Current.GoToAsync(nameof(UserProductDetailPage), true, navigationParameter);

            });


    }
}
