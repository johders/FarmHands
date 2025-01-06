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


        // Full data lists
        private ObservableCollection<FarmViewModel> allFarms;
        private ObservableCollection<ProductViewModel> allProducts;

        // Filtered lists bound to UI
        private ObservableCollection<FarmViewModel> filteredFarms;
        public ObservableCollection<FarmViewModel> FilteredFarms
        {
            get => filteredFarms;
            set => SetProperty(ref filteredFarms, value);
        }

        private ObservableCollection<ProductViewModel> filteredProducts;
        public ObservableCollection<ProductViewModel> FilteredProducts
        {
            get => filteredProducts;
            set => SetProperty(ref filteredProducts, value);
        }

        // Search query property
        private string searchQuery;
        public string SearchQuery
        {
            get => searchQuery;
            set
            {
                if (SetProperty(ref searchQuery, value))
                {
                    PerformSearch();
                }
            }
        }

        public ICommand LoadDataCommand => new Command(async () =>
        {
            IsLoading = true;
            var farmResult = await _farmService.GetAllAsync();
            var farmViewModels = farmResult.Data
                .Where(f => f.ProfileComplete)
                .Select(farm => new FarmViewModel(farm, _farmService, _imageConversionService));

            allFarms = new ObservableCollection<FarmViewModel>(farmViewModels);
            FilteredFarms = new ObservableCollection<FarmViewModel>(allFarms);

            var productResult = await _productService.GetAllAsync();
            var productViewModels = productResult.Data
                .Select(product => new ProductViewModel(product, _productService, _imageConversionService))
                .ToList();

            await Task.WhenAll(productViewModels.Select(vm => vm.LoadOfferCountAsync()));

            allProducts = new ObservableCollection<ProductViewModel>(productViewModels
                .Where(p => p.OfferCount > 0)
                .OrderByDescending(p => p.OfferCount));
            FilteredProducts = new ObservableCollection<ProductViewModel>(allProducts);
            IsLoading = false;
        });

        private void PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery))
            {
                FilteredFarms = new ObservableCollection<FarmViewModel>(allFarms);
                FilteredProducts = new ObservableCollection<ProductViewModel>(allProducts);
                return;
            }

            var query = SearchQuery.ToLower();

            FilteredFarms = new ObservableCollection<FarmViewModel>(allFarms
                .Where(farm => farm.Name.ToLower().Contains(query) ||
                               farm.Description.ToLower().Contains(query)));

            FilteredProducts = new ObservableCollection<ProductViewModel>(allProducts
                .Where(product => product.Name.ToLower().Contains(query)));
        }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set => SetProperty(ref isLoading, value);
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
