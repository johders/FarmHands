using Google.Api;
using Mde.Project.Core.Services;
using Mde.Project.Core.Services.Interfaces;
using Moq;

namespace Mde.Project.Tests.Services
{
    public class ImageConversionServiceTests
    {
        [Fact]
        public void ConvertBase64ToByteArray_WithValidBase64String_ReturnsByteArray()
        {
            // Arrange
            string validBase64String = "RmFybUhhbmRzIEJhc2U2NCBUZXN0";
            var imageService = new ImageConversionService();

            // Act
            var result = imageService.ConvertBase64ToByteArray(validBase64String);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("FarmHands Base64 Test", System.Text.Encoding.UTF8.GetString(result));
        }

        [Fact]
        public void ConvertBase64ToByteArray_WithInvalidBase64String_ReturnsNull()
        {
            // Arrange
            string invalidBase64String = "I_Am-An_Invalid-Base64_string";
            var imageService = new ImageConversionService();

            // Act
            var result = imageService.ConvertBase64ToByteArray(invalidBase64String);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ConvertBase64ToStream_WithValidBase64String_ReturnsStream()
        {
            // Arrange
            string validBase64String = "RmFybUhhbmRzIEJhc2U2NCBUZXN0";
            var imageService = new ImageConversionService();

            // Act
            var result = imageService.ConvertBase64ToStream(validBase64String);

            // Assert
            Assert.NotNull(result);
            using (var reader = new StreamReader(result))
            {
                var text = reader.ReadToEnd();
                Assert.Equal("FarmHands Base64 Test", text);
            }
        }

        [Fact]
        public void ConvertBase64ToStream_WithInvalidBase64String_ReturnsNull()
        {
            // Arrange
            string invalidBase64String = "I_Am-An_Invalid-Base64_string";
            var imageService = new ImageConversionService();

            // Act
            var result = imageService.ConvertBase64ToStream(invalidBase64String);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ConvertStreamToBase64_WithValidStream_ReturnsBase64String()
        {
            // Arrange
            string expectedString = "FarmHands Base64 Test";
            using var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(expectedString));
            var imageService = new ImageConversionService();

            // Act
            var result = imageService.ConvertStreamToBase64(stream);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("RmFybUhhbmRzIEJhc2U2NCBUZXN0", result);
        }

        [Fact]
        public void ConvertStreamToBase64_WithEmptyStream_ReturnsEmptyBase64String()
        {
            // Arrange
            using var stream = new MemoryStream();
            var imageService = new ImageConversionService();

            // Act
            var result = imageService.ConvertStreamToBase64(stream);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("", result);
        }
    }
}
