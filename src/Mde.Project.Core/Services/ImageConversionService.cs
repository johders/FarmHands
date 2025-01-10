using Mde.Project.Core.Services.Interfaces;

namespace Mde.Project.Core.Services
{
    public class ImageConversionService : IImageConversionService
    {
        public byte[] ConvertBase64ToByteArray(string base64String)
        {
            try
            {
                var base64Data = base64String.Contains(",")
                    ? base64String.Substring(base64String.IndexOf(",") + 1)
                    : base64String;

                return Convert.FromBase64String(base64Data);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Stream ConvertBase64ToStream(string base64String)
        {
            var byteArray = ConvertBase64ToByteArray(base64String);
            return byteArray != null ? new MemoryStream(byteArray) : null;
        }

        public string ConvertStreamToBase64(Stream imageStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                imageStream.CopyTo(memoryStream);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }
}
