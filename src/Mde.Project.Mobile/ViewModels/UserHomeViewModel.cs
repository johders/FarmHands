using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services;
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
        private readonly FarmService _testService;
        private readonly ProductService _prodTesterService;

        public UserHomeViewModel(IFarmService farmService, IProductService productService, FarmService testService, ProductService prodTesterService)
        {
            _farmService = farmService;
            _productService = productService;

            _testService = testService;
            _prodTesterService = prodTesterService;
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

        private ObservableCollection<ProductViewModel> products;
        public ObservableCollection<ProductViewModel> Products
        {
            get { return products; }
            set
            {
                SetProperty(ref products, value);
            }
        }

        public ICommand RefreshFarmListCommand => new Command(async () =>
		{
            //var result = await _farmService.GetAllAsync();
            
            var result = await _testService.GetAllAsync();

            var farms = result.Data;
			Farms = new ObservableCollection<Farm>(farms);
		});

        public ICommand RefreshProductListCommand => new Command(async () =>
        {
            //var result = await _productService.GetAllAsync();
            var result = await _prodTesterService.GetAllAsync();
            var products = result.Data.Select(product => new ProductViewModel(product, _productService));
            Products = new ObservableCollection<ProductViewModel>(products.Where(p => p.OfferCount > 0).OrderByDescending(p => p.OfferCount));
		});

        public ICommand ViewFarmDetailsCommand => new Command<Farm>(async (farm) =>
        {
            var navigationParameter = new Dictionary<string, object>()
            {
                { nameof(UserFarmDetailsViewModel.SelectedFarm), farm }
            };

             await Shell.Current.GoToAsync(nameof(UserFarmDetailPage), true, navigationParameter);
        });

        public ICommand ViewProductDetailsCommand => new Command<ProductViewModel>(async (productViewModel) =>
        {
            //var result = await _productService.GetByIdAsync(productViewModel.Id);
            var result = await _prodTesterService.GetByIdAsync(productViewModel.Id);
            var product = result.Data;

            if (!result.IsSuccess) 
            {
                Shell.Current.DisplayAlert("Oops", $"{String.Join(", ", result.Errors)}", "OK");
                return;
            }

            var navigationParameter = new Dictionary<string, object>()
            {
                { nameof(UserProductDetailsViewModel.SelectedProduct), product }
            };

            await Shell.Current.GoToAsync(nameof(UserProductDetailPage), true, navigationParameter);
        });


    }
}
