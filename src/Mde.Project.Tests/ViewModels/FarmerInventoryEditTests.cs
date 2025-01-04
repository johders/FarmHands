using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Mobile.ViewModels;
using Moq;

namespace Mde.Project.Tests.ViewModels
{
    public class FarmerInventoryEditTests
    {
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<IOfferService> _offerServiceMock;
        private readonly Mock<IFarmService> _farmServiceMock;
        private readonly Mock<IFarmerService> _farmerServiceMock;
        private readonly Mock<IImageConversionService> _imageConversionServiceMock;
        private readonly FarmerInventoryEditViewModel _viewModel;

        public FarmerInventoryEditTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _offerServiceMock = new Mock<IOfferService>();
            _farmServiceMock = new Mock<IFarmService>();
            _farmerServiceMock = new Mock<IFarmerService>();
            _imageConversionServiceMock = new Mock<IImageConversionService>();

            _viewModel = new FarmerInventoryEditViewModel(
                    _productServiceMock.Object,
                    _offerServiceMock.Object,
                    _farmServiceMock.Object,
                    _farmerServiceMock.Object,
                    _imageConversionServiceMock.Object);
        }

        [Fact]
        public void LoadUnitOptions_WithViewmodelInstance_InitializesUnitOptionsCorrectly()
        {
            // Arrange
            var viewModel = new FarmerInventoryEditViewModel(
                Mock.Of<IProductService>(), Mock.Of<IOfferService>(), Mock.Of<IFarmService>(),
                Mock.Of<IFarmerService>(), Mock.Of<IImageConversionService>());

            // Act
            var unitOptions = viewModel.UnitOptions;

            // Assert
            Assert.NotEmpty(unitOptions);
            Assert.Equal(Enum.GetValues(typeof(Unit)).Length, unitOptions.Count);
        }

        [Fact]
        public async Task InitializeAsync_WhenProductsAvailable_PopulatesProducts()
        {
            // Arrange
            var mockProducts = new List<Product>
            {
                new Product { Id = "1", Name = "Product1", ImageUrl = "image" },
                new Product { Id = "2", Name = "Product2", ImageUrl = "image" },
            };

            var mockProductService = new Mock<IProductService>();
            mockProductService.Setup(service => service.GetAllAsync())
                .ReturnsAsync(new ResultModel<IEnumerable<Product>> { Data = mockProducts });

            var viewModel = new FarmerInventoryEditViewModel(
                mockProductService.Object, Mock.Of<IOfferService>(), Mock.Of<IFarmService>(),
                Mock.Of<IFarmerService>(), Mock.Of<IImageConversionService>());

            // Act
            await viewModel.InitializeAsync();

            // Assert
            Assert.NotEmpty(viewModel.Products);
            Assert.Equal(2, viewModel.Products.Count);
            Assert.Equal("Product1", viewModel.Products[0].Name);
        }

        [Fact]
        public void SelectedOffer_WhenSet_UpdatesDependentProperties()
        {
            // Arrange
            var validOffer = new Offer
            {
                Variant = "Mock Variant",
                Product = new Product { Id = "1", Name = "Mock Product" },
                Description = "Mock Description",
                Price = 100.50m,
                Unit = Unit.Kilogram,
                OfferImageUrl = "http://example.com/image.jpg"
            };

            var offerViewModel = new OfferViewModel(validOffer, _imageConversionServiceMock.Object, _offerServiceMock.Object);

            // Act
            _viewModel.SelectedOffer = offerViewModel;

            // Assert
            Assert.Equal("Edit offer", _viewModel.PageTitle);
            Assert.Equal("Mock Variant", _viewModel.Variant);
            Assert.Equal("Mock Product", _viewModel.SelectedProduct.Name);
            Assert.Equal("Mock Description", _viewModel.Description);
            Assert.Equal(100.50m, _viewModel.Price);
            Assert.Equal(Unit.Kilogram, _viewModel.SelectedUnit);
            Assert.Equal("http://example.com/image.jpg", _viewModel.ImageUrl);
        }
    }
}
