using CommunityToolkit.Mvvm.ComponentModel;
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
        private readonly IImageConversionService _imageConversionService;

        public UserHomeViewModel(IFarmService farmService, IProductService productService, IImageConversionService imageConversionService)
        {
            _farmService = farmService;
            _productService = productService;
            _imageConversionService = imageConversionService;
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            private set => SetProperty(ref isLoading, value);
        }

        private bool isLoadingFarms;
        public bool IsLoadingFarms
        {
            get => isLoadingFarms;
            set
            {
                SetProperty(ref isLoadingFarms, value);
                UpdateIsLoading();
            }
        }

        private bool isLoadingProducts;
        public bool IsLoadingProducts
        {
            get => isLoadingProducts;
            set
            {
                SetProperty(ref isLoadingProducts, value);
                UpdateIsLoading();
            }
        }
        private void UpdateIsLoading()
        {
            IsLoading = IsLoadingFarms && IsLoadingProducts;
        }

        private ObservableCollection<FarmViewModel> farms;
        public ObservableCollection<FarmViewModel> Farms
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
            IsLoadingFarms = true;
            var result = await _farmService.GetAllAsync();

            var farms = result.Data.Where(f => f.ProfileComplete).Select(farm => new FarmViewModel(farm, _farmService, _imageConversionService));

            Farms = new ObservableCollection<FarmViewModel>(farms);
            IsLoadingFarms = false;
        });

        public ICommand RefreshProductListCommand => new Command(async () =>
        {
            IsLoadingProducts = true;
            var result = await _productService.GetAllAsync();

            var productViewModels = result.Data
                .Select(product => new ProductViewModel(product, _productService, _imageConversionService))
                .ToList();

            await Task.WhenAll(productViewModels.Select(vm => vm.LoadOfferCountAsync()));

            Products = new ObservableCollection<ProductViewModel>(productViewModels
                .Where(p => p.OfferCount > 0)
                .OrderByDescending(p => p.OfferCount));
            IsLoadingProducts = false;
        });

        public ICommand ViewFarmDetailsCommand => new Command<FarmViewModel>(async (farm) =>
        {
            var navigationParameter = new Dictionary<string, object>()
            {
                { nameof(UserFarmDetailsViewModel.SelectedFarm), farm }
            };

            await Shell.Current.GoToAsync(nameof(UserFarmDetailPage), true, navigationParameter);
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
