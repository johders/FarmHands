using Mde.Project.Core.Services;
using Mde.Project.Mobile.Constants;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Mde.Project.Tests.Services
{
    public class MealDbServiceTests
    {
        private Mock<IHttpClientFactory> CreateMockHttpClientFactory(HttpMessageHandler handler)
        {
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(AppConstants.MealApiUrl)
            };
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            var mockFactory = new Mock<IHttpClientFactory>();
            mockFactory.Setup(_ => _.CreateClient(AppConstants.MealClientName))
               .Returns(httpClient);
            return mockFactory;
        }

        [Fact]
        public async Task GetMealsByIngredientAsync_WithValidIngredient_ReturnsMeals()
        {
            // Arrange
            var expectedApiResponse = new
            {
                meals = new[]
                {
                    new { strMeal = "Chicken Curry", idMeal = "12345" },
                    new { strMeal = "Chicken Alfredo", idMeal = "67890" }
                }
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
            var service = new MealDbService(mockFactory.Object);

            // Act
            var result = await service.GetMealsByIngredientAsync("chicken");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Errors);
            Assert.NotNull(result.Data);
            Assert.Equal(2, result.Data.Count);
            Assert.Contains(result.Data, meal => meal.Name == "Chicken Curry");
            Assert.Contains(result.Data, meal => meal.Name == "Chicken Alfredo");
        }

        [Fact]
        public async Task GetMealsByIngredientAsync_WithEmptyResult_ReturnsErrorMessage()
        {
            // Arrange
            var emptyApiResponse = new { meals = (object[])null };
            var serializedEmptyApiResponse = JsonSerializer.Serialize(emptyApiResponse);
            string unknownIngredient = "something-weird";
            string expectedErrorMessage = "No meals found for the given ingredient.";

            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(serializedEmptyApiResponse, Encoding.UTF8, "application/json")
                });

            var mockFactory = CreateMockHttpClientFactory(mockHandler.Object);
            var service = new MealDbService(mockFactory.Object);

            // Act
            var result = await service.GetMealsByIngredientAsync(unknownIngredient);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Errors);
            Assert.Null(result.Data);
            Assert.Contains(expectedErrorMessage, result.Errors);
        }
    }
}
