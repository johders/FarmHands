using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Mobile.Services;
using Mde.Project.Mobile.ViewModels;
using Moq;

namespace Mde.Project.Tests.ViewModels
{
    public class FarmerDashboardTests
    {
        private readonly Mock<IOfferService> _mockOfferService;
        private readonly Mock<IFarmerService> _mockFarmerService;
        private readonly Mock<IImageConversionService> _mockImageConversionService;
        private readonly Mock<IFarmService> _farmService;
        private readonly FakeSecureStorageService _fakeSecureStorageService;
        private readonly FarmerDashboardViewModel _viewModel;

        public FarmerDashboardTests()
        {
            _mockOfferService = new Mock<IOfferService>();
            _mockFarmerService = new Mock<IFarmerService>();
            _mockImageConversionService = new Mock<IImageConversionService>();
            _farmService = new Mock<IFarmService>();
            _fakeSecureStorageService = new FakeSecureStorageService();

            _viewModel = new FarmerDashboardViewModel(
                _mockOfferService.Object,
                _mockFarmerService.Object,
                _mockImageConversionService.Object,
                _fakeSecureStorageService,
                _farmService.Object
            );
            
        }

        [Fact]
        public async Task RefreshOffersListCommand_WithValidFarmId_PopulatesOffers()
        {
            // Arrange
            var mockFarmId = "mockFarmId";
            var mockUserId = "mockUserId";
            var mockOffers = new List<Offer>
            {
                new Offer { Id = "1", Variant = "Offer1", OfferImageUrl = "imageUrl" },
                new Offer { Id = "2", Variant = "Offer2", OfferImageUrl = "imageUrl" }
            };

            _mockFarmerService
                .Setup(service => service.GetFarmIdByFarmerAsync(mockUserId))
                .ReturnsAsync(new ResultModel<string> { Data = mockFarmId });

            _mockOfferService
               .Setup(service => service.GetAllOffersByFarmIdAsync(mockFarmId))
               .ReturnsAsync(new ResultModel<IEnumerable<Offer>> { Data = mockOffers.AsEnumerable() });

            await _fakeSecureStorageService.SetAsync("userId", mockUserId);

            // Act
            await Task.Run(() => _viewModel.RefreshOffersListCommand.Execute(null));

            // Assert
            Assert.False(_viewModel.IsLoading);
            Assert.NotEmpty(_viewModel.Offers);
            Assert.Equal(2, _viewModel.Offers.Count);
            Assert.Equal("Offer1", _viewModel.Offers[0].VariantName);
            Assert.Equal("Offer2", _viewModel.Offers[1].VariantName);
        }

        [Fact]
        public async Task RefreshOffersListCommand_WhenNoOffers_Available_LeavesOffersEmpty()
        {
            // Arrange
            var mockFarmId = "mockFarmId";
            var mockUserId = "mockUserId";

            _mockFarmerService
                .Setup(service => service.GetFarmIdByFarmerAsync(mockUserId))
                .ReturnsAsync(new ResultModel<string> { Data = mockFarmId });

            _mockOfferService
                .Setup(service => service.GetAllOffersByFarmIdAsync(mockFarmId))
                .ReturnsAsync(new ResultModel<IEnumerable<Offer>> { Data = Enumerable.Empty<Offer>() });

            await _fakeSecureStorageService.SetAsync("userId", mockUserId);

            // Act
            await Task.Run(() => _viewModel.RefreshOffersListCommand.Execute(null));

            // Assert
            Assert.False(_viewModel.IsLoading);
            Assert.Empty(_viewModel.Offers);
        }
    }
}
