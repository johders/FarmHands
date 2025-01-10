using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Core.Services;
using Mde.Project.Mobile.Constants;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Mde.Project.Tests.Services
{
    public class OpenStreetServiceTests
    {
        private Mock<IHttpClientFactory> CreateMockHttpClientFactory(HttpMessageHandler handler)
        {
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(AppConstants.StreetApiUrl)
            };
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(AppConstants.UserAgentHeader);

            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(AppConstants.AddressClientName)).Returns(httpClient);
            return mockFactory;
        }

        [Theory]
        [InlineData("123 Sesame Street, Springfield", "40.123", "-75.123")]
        [InlineData("124 Tegridy Farm Lane, South Park", "40.124", "-75.124")]
        public async Task GetCoordinatesFromAddressAsync_WithValidAddress_ReturnsResults(string validAddress, string expectedLat, string expectedLon)
        {
            // Arrange
            var expectedApiResponse = new List<OpenStreetResult>
            {
                new OpenStreetResult { Name = validAddress, Lat = expectedLat, Lon = expectedLon }
            };

            var serializedApiResponse = JsonSerializer.Serialize(expectedApiResponse);

            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(serializedApiResponse, Encoding.UTF8, "application/json")
                });

            var mockFactory = CreateMockHttpClientFactory(mockHandler.Object);
            var service = new OpenStreetService(mockFactory.Object);

            // Act
            var result = await service.GetCoordinatesFromAddressAsync(validAddress);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Data);
            Assert.Single(result.Data);
            Assert.Contains(result.Data, r => r.Name == validAddress && r.Lat == expectedLat && r.Lon == expectedLon);
        }

        [Fact]
        public async Task GetCoordinatesFromAddressAsync_WithNoResults_ReturnsError()
        {
            // Arrange
            string unknownAddress = "undisclosed location";
            var emptyResponse = new List<OpenStreetResult>();
            var serializedResponse = JsonSerializer.Serialize(emptyResponse);

            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(serializedResponse, Encoding.UTF8, "application/json")
                });

            var mockFactory = CreateMockHttpClientFactory(mockHandler.Object);
            var service = new OpenStreetService(mockFactory.Object);

            // Act
            var result = await service.GetCoordinatesFromAddressAsync(unknownAddress);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Errors);
            Assert.Contains("No locations found for address input.", result.Errors);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetCoordinatesFromAddressAsync_WithInvalidAddressInput_ReturnsApiError()
        {
            // Arrange
            string invalidAddress = "incorrect input";
            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest
                });

            var mockFactory = CreateMockHttpClientFactory(mockHandler.Object);
            var service = new OpenStreetService(mockFactory.Object);

            // Act
            var result = await service.GetCoordinatesFromAddressAsync(invalidAddress);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Errors);
            Assert.Contains("Error fetching API data", result.Errors);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetCoordinatesFromAddressAsync_ExceptionThrown_ReturnsError()
        {
            // Arrange
            string validAddress = "address input";
            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new Exception("Something went wrong"));

            var mockFactory = CreateMockHttpClientFactory(mockHandler.Object);
            var service = new OpenStreetService(mockFactory.Object);

            // Act
            var result = await service.GetCoordinatesFromAddressAsync(validAddress);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Errors);
            Assert.Contains("Something went wrong", result.Errors);
            Assert.Null(result.Data);
        }
    }
}
