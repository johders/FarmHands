using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Mobile.ViewModels;
using Moq;

namespace Mde.Project.Tests.ViewModels
{
    public class UserHomeTests
    {
        private readonly Mock<IFarmService> _farmServiceMock;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IImageConversionService> _imageConversionServiceMock;
        private readonly UserHomeViewModel _viewModel;

        public UserHomeTests()
        {
            _farmServiceMock = new Mock<IFarmService>();
            _productServiceMock = new Mock<IProductService>();
            _imageConversionServiceMock = new Mock<IImageConversionService>();

            _viewModel = new UserHomeViewModel(
                _farmServiceMock.Object,
                _productServiceMock.Object,
                _imageConversionServiceMock.Object);
        }

        [Fact]
        public async Task RefreshFarmListCommand_WithValidData_UpdatesFarms()
        {
            // Arrange
            var farms = new List<Farm> 
            {
            new Farm { Id = "1", Name = "Farm A", ProfileComplete = true, ImageUrl = "test" },
            new Farm { Id = "2", Name = "Farm B", ProfileComplete = true, ImageUrl = "test" }
            };

            _farmServiceMock.Setup(f => f.GetAllAsync())
                .ReturnsAsync(new ResultModel<IEnumerable<Farm>> { Data = farms });

            // Act
            await Task.Run(() => _viewModel.RefreshFarmListCommand.Execute(null));

            // Assert
            Assert.NotEmpty(_viewModel.Farms);
            Assert.Equal(2, _viewModel.Farms.Count);
        }

        [Fact]
        public async Task RefreshProductListCommand_WithValidData_UpdatesProductsAndOfferCount()
        {
            // Arrange
            int offerCountFirst = 3;
            int offerCountSecond = 5;

            var products = new List<Product>
            {
                new Product { Id = "1", Name = "Product A", ImageUrl = "test"},
                new Product { Id = "2", Name = "Product B", ImageUrl = "test"}
            };

            _productServiceMock.Setup(p => p.GetAllAsync())
                .ReturnsAsync(new ResultModel<IEnumerable<Product>> { Data = products });

            _productServiceMock.Setup(p => p.GetOfferCountAsync("1"))
                                            .ReturnsAsync(offerCountFirst);
            _productServiceMock.Setup(p => p.GetOfferCountAsync("2"))
                                            .ReturnsAsync(offerCountSecond);

            var productViewModels = products.Select(product => new ProductViewModel(product, _productServiceMock.Object, _imageConversionServiceMock.Object)).ToList();
            
            // Act
            await Task.Run(() => _viewModel.RefreshProductListCommand.Execute(null));
            await Task.WhenAll(productViewModels.Select(vm => vm.LoadOfferCountAsync()));

            // Assert
            Assert.NotEmpty(_viewModel.Products); 
            Assert.Equal(2, _viewModel.Products.Count);
            Assert.Equal(5, _viewModel.Products.First().OfferCount);
            Assert.Equal(3, _viewModel.Products.Last().OfferCount);
        }
    }

}
